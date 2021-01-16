using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class DragManager
    {
        private readonly Camera _cam;
        private readonly LayerMask _itemLayer;
        private const float GapFromGround = 1;

        private Plane _plane;
        private Item _item;
    
        public delegate void OnItem(Item item);
        public event OnItem ItemReleased;
        
        public DragManager(InputManager inputManager)
        {
            inputManager.TouchEnter += InputManagerOnTouchEnter;
            inputManager.TouchMoved += InputManagerOnTouchMoved;
            inputManager.TouchExit += InputManagerOnTouchExit;
        
            _cam = Camera.main;
            _itemLayer = LayerMask.NameToLayer("Item");
        }

        private void InputManagerOnTouchEnter(Vector2 screenPoint)
        {
            if (Physics.Raycast(_cam.ScreenPointToRay(screenPoint), out var hit, _itemLayer) &&
                (_item = hit.collider.GetComponent<Item>()))
            {
                _item = hit.collider.GetComponent<Item>();
                _item.DisablePhysics();
                
                _plane = new Plane(Vector3.up, Vector3.up * GapFromGround);
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
                ItemReleased?.Invoke(_item);
                _item = null;
            }
        }

        private void Move(Vector2 screenPoint)
        {
            var ray = _cam.ScreenPointToRay(screenPoint);
            if(_plane.Raycast(ray, out var distance))
            {
                _item.transform.position = ray.GetPoint(distance);
            }
        }
    }
}
