using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class DragManager
    {
        private const float GapFromGround = 1;

        private Plane _plane;
        private Item _item;

        public readonly InputManager InputManager;
        
        public delegate void OnItem(Item item, Vector2 screenPoint);
        public event OnItem ItemReleased;
        
        public DragManager(InputManager inputManager)
        {
            (InputManager = inputManager).TouchEnter += InputManagerOnTouchEnter;
            InputManager.TouchMoved += InputManagerOnTouchMoved;
            InputManager.TouchExit += InputManagerOnTouchExit;
        }

        private void InputManagerOnTouchEnter(Vector2 screenPoint)
        {
            if (InputManager.CheckItemTouched(screenPoint, out _item))
            {
                _item.DisablePhysics();
                
                _plane = new Plane(Vector3.up, Vector3.up * GapFromGround);
                Move(screenPoint);
            }
        }
    
        private void InputManagerOnTouchMoved(Vector2 screenPoint)
        {
            if (_item)
            {
                Move(screenPoint);
            }
        }
    
        private void InputManagerOnTouchExit(Vector2 screenPoint)
        {
            if (_item)
            {
                _item.EnablePhysics();
                ItemReleased?.Invoke(_item, screenPoint);
                _item = null;
            }
        }

        private void Move(Vector2 screenPoint)
        {
            var ray = InputManager.cam.ScreenPointToRay(screenPoint);
            if(_plane.Raycast(ray, out var distance))
            {
                _item.transform.position = ray.GetPoint(distance);
            }
        }
    }
}
