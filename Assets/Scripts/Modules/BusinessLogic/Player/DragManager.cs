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

        public void StartMove()
        {
            _plane = new Plane(Vector3.up, Vector3.up * GapFromGround);
        }
        
        public void Move(Item item, Vector2 screenPoint)
        {
            var ray = _inputManager.cam.ScreenPointToRay(screenPoint);
            if(_plane.Raycast(ray, out var distance))
            {
                item.transform.position = ray.GetPoint(distance);
            }
        }

        public void Move(Vector2 screenPoint)
        {
            Move(_item, screenPoint);
        }

        public void Subscribe()
        {
            _inputManager.TouchMoved += InputManagerOnTouchMoved;
            _inputManager.TouchExit += InputManagerOnTouchExit;
        }

        private void InputManagerOnTouchMoved(Vector2 screenPoint)
        {
            Move(screenPoint);
        }


        private void InputManagerOnTouchExit(Vector2 screenPoint)
        {
            Unsubscribe();
            _item.EnablePhysics();
            ItemReleased?.Invoke(_item, screenPoint);
        }

        private void Unsubscribe()
        {
            _inputManager.TouchMoved -= InputManagerOnTouchMoved;
            _inputManager.TouchExit -= InputManagerOnTouchExit;
        }
    }
}