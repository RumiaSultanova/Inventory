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
        public event OnInput Added;
        public event OnInput Selected;
        
        public override void Inject(SessionManager session)
        {
            _inventoryUI = session.Level.InventoryUI;
            _inventoryUI.PointerExit += InventoryOnPointerExit;
            session.SnapManager.ItemSnapped += SnapManagerOnItemAdded;
            (_inputManager = session.InputManager).TouchEnter += InputManagerOnTouchEnter;
        }

        /// <summary>
        /// Add item to inventory when item snapping to bag
        /// </summary>
        /// <param name="item">Snapped item</param>
        private void SnapManagerOnItemAdded(Item.Item item)
        {
            _inventoryUI.AddItem(item);
            Added?.Invoke(item);
        }
        
        /// <summary>
        /// Activate inventory if mouse pressed on bag
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void InputManagerOnTouchEnter(Vector2 screenPoint)
        {
            if (_inputManager.CheckBagTouched(screenPoint))
            {
                ActivateInventory();
            }
        }
        
        /// <summary>
        /// Deactivate inventory if mouse not on bag or its UI
        /// </summary>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void InputManagerOnTouchMoved(Vector2 screenPoint)
        {
            if (!(_inputManager.CheckBagTouched(screenPoint) || _inputManager.CheckUITouched()))
            {
                DeactivateInventory(screenPoint);
            }
        }
        
        /// <summary>
        /// Check if mouse stop pressing on item UI to select
        /// </summary>
        /// <param name="item">Last touched item</param>
        private void InventoryOnPointerExit(Item.Item item)
        {
            if (!_inputManager.IsTouching)
            {
                _inventoryUI.SelectItem(item);
                Selected?.Invoke(item);
            }
        }

        /// <summary>
        /// Activate inventory UI and subscribe to deactivate if mouse stop pressing or moved from bag or its UI
        /// </summary>
        private void ActivateInventory()
        {
            _inventoryUI.Activate();
            _inputManager.TouchMoved += InputManagerOnTouchMoved;
            _inputManager.TouchExit += DeactivateInventory;
        }
        
        /// <summary>
        /// Deactivates inventory UI and unsubscribe from events
        /// </summary>
        /// <param name="screenPoint"></param>
        private void DeactivateInventory(Vector2 screenPoint)
        {
            _inventoryUI.Deactivate();
            _inputManager.TouchExit -= DeactivateInventory;
            _inputManager.TouchMoved -= InputManagerOnTouchMoved;
        }
    }
}
