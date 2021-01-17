using System;
using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.BusinessLogic.CustomInput
{
    public class InputManager : MonoBehaviour
    {
        public Camera cam;

        public bool IsTouching => _isTouching;
        private bool _isTouching;

        public delegate void OnInput(Vector2 screenPoint);
        public event OnInput TouchEnter;
        public event OnInput TouchStay;
        public event OnInput TouchMoved;
        public event OnInput TouchExit;

        private const float MoveDelta = .05f;
        
        private int _itemLayer;
        private int _bagLayer;

        private void Awake()
        {
            cam = Camera.main;
            _itemLayer = LayerMask.GetMask("Item");
            _bagLayer = LayerMask.GetMask("Bag");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                TouchEnter?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                TouchStay?.Invoke(Input.mousePosition);
                if (Math.Abs(Input.GetAxis("Mouse X")) < MoveDelta && Math.Abs(Input.GetAxis("Mouse Y")) < MoveDelta) { return; }
                TouchMoved?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                TouchExit?.Invoke(Input.mousePosition);
            }
        }

        public bool CheckItemTouched(Vector2 screenPoint, out Item item)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(screenPoint), out var hit, 100f, _itemLayer))
            {
                item = hit.collider.GetComponent<Item>();
                return true;
            }

            item = null;
            return false;
        }

        public bool CheckBagTouched(Vector2 screenPoint)
        {
            return Physics.Raycast(cam.ScreenPointToRay(screenPoint), out _, 100f, _bagLayer);
        }

        public bool CheckUITouched()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
