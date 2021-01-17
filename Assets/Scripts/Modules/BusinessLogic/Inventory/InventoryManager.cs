using Modules.BusinessLogic.Core;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Session;
using Modules.UI.Inventory;
using UnityEngine;

namespace Modules.BusinessLogic.Inventory
{
    public class InventoryManager : Manager
    {
        private InventoryUI _inventoryUI;

        private InputManager _inputManager;

        public delegate void OnInput(Item.Item item);
        public event OnInput Selected;
        
        public override void Inject(SessionManager session)
        {
            _inventoryUI = session.Level.InventoryUI;
            _inventoryUI.PointerExit += InventoryOnPointerExit;
            session.SnapManager.ItemSnapped += SnapManagerOnItemAdded;
            (_inputManager = session.InputManager).TouchEnter += InputManagerOnTouchEnter;
        }

        private void SnapManagerOnItemAdded(Item.Item item)
        {
            _inventoryUI.AddItem(item);
        }
        
        private void InventoryOnPointerExit(Item.Item item)
        {
            if (!_inputManager.IsTouching)
            {
                _inventoryUI.SelectItem(item);
                Selected?.Invoke(item);
            }
        }
        
        private void InputManagerOnTouchEnter(Vector2 screenPoint)
        {
            if (_inputManager.CheckBagTouched(screenPoint))
            {
                _inventoryUI.Activate();
                _inputManager.TouchMoved += InputManagerOnTouchMoved;
                _inputManager.TouchExit += DeactivateInventory;
            }
        }
        
        private void InputManagerOnTouchMoved(Vector2 screenPoint)
        {
            if (!(_inputManager.CheckBagTouched(screenPoint) || _inputManager.CheckUITouched()))
            {
                DeactivateInventory(screenPoint);
            }
        }
        
        private void DeactivateInventory(Vector2 screenPoint)
        {
            _inventoryUI.Deactivate();
            _inputManager.TouchExit -= DeactivateInventory;
            _inputManager.TouchMoved -= InputManagerOnTouchMoved;
        }
    }
}
