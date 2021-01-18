using Modules.BusinessLogic.Core;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory.Item;
using Modules.BusinessLogic.Session;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class DragManager : Manager
    {
        private const float GapFromGround = 1;

        private Plane _plane;
        private Item _item;

        private InputManager _inputManager;
        
        public delegate void OnItem(Item item, Vector2 screenPoint);
        public event OnItem ItemReleased;
        
        public override void Inject(SessionManager session)
        {
            (_inputManager = session.InputManager).TouchEnter += InputManagerOnTouchEnter;
        }

        /// <summary>
        /// Check if mouse pressed on object with Item component to start dragging it
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void InputManagerOnTouchEnter(Vector2 screenPoint)
        {
            if (_inputManager.CheckItemTouched(screenPoint, out _item))
            {
                _item.DisablePhysics();
                Subscribe();       
                StartMove();
                Move(screenPoint);
            }
        }

        /// <summary>
        /// Define plane above ground where item will be dragged
        /// </summary>
        public void StartMove()
        {
            _plane = new Plane(Vector3.up, Vector3.up * GapFromGround);
        }
        
        /// <summary>
        /// Raycast to defined plane to place item on its surface 
        /// </summary>
        /// <param name="item">Item to drag</param>
        /// <param name="screenPoint">Point in screen dimension</param>
        public void Move(Item item, Vector2 screenPoint)
        {
            var ray = _inputManager.cam.ScreenPointToRay(screenPoint);
            if(_plane.Raycast(ray, out var distance))
            {
                item.transform.position = ray.GetPoint(distance);
            }
        }

        /// <summary>
        /// Raycast to defined plane to place item on its surface 
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        public void Move(Vector2 screenPoint)
        {
            Move(_item, screenPoint);
        }

        /// <summary>
        /// Subscribe to pressing mouse movement and stop pressing events 
        /// </summary>
        public void Subscribe()
        {
            _inputManager.TouchMoved += InputManagerOnTouchMoved;
            _inputManager.TouchExit += InputManagerOnTouchExit;
        }

        /// <summary>
        /// Move item to place in virtual world under screen point
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void InputManagerOnTouchMoved(Vector2 screenPoint)
        {
            Move(screenPoint);
        }

        /// <summary>
        ///  Stop dragging item, enable physics to drop it and unsubscribe from events
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void InputManagerOnTouchExit(Vector2 screenPoint)
        {
            Unsubscribe();
            _item.EnablePhysics();
            ItemReleased?.Invoke(_item, screenPoint);
        }

        /// <summary>
        /// Unsubscribe from pressing mouse movement and from stop pressing events
        /// </summary>
        private void Unsubscribe()
        {
            _inputManager.TouchMoved -= InputManagerOnTouchMoved;
            _inputManager.TouchExit -= InputManagerOnTouchExit;
        }
    }
}