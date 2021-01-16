using Modules.CustomInput;
using Modules.Inventory.Item;
using UnityEngine;

namespace Modules.Player
{
    public class DragManager
    {
        private readonly Camera _cam;
        private readonly LayerMask _itemLayer;
        private const float GapFromGround = 1;

        private Plane _plane;
        private ItemManager _item;
    
        public DragManager(InputManager inputManager)
        {
            inputManager.TouchEnter += InputManagerOnTouchEnter;
            inputManager.TouchMoved += InputManagerOnTouchMoved;
            inputManager.TouchExit += InputManagerOnTouchExit;
        
            _cam = Camera.main;
            _itemLayer = LayerMask.NameToLayer("Item");
        }

        private void InputManagerOnTouchEnter(Vector2 touch)
        {
            if (Physics.Raycast(_cam.ScreenPointToRay(touch), out var hit, _itemLayer))
            {
                _item = hit.collider.GetComponent<ItemManager>();
                _item.DisablePhysics();
                
                _plane = new Plane(Vector3.up, Vector3.up * GapFromGround);
            }
        }
    
        private void InputManagerOnTouchMoved(Vector2 touch)
        {
            if (_item)
            {
                Move(touch);
            }
        }
    
        private void InputManagerOnTouchExit(Vector2 touch)
        {
            if (_item)
            {
                _item.EnablePhysics();
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
