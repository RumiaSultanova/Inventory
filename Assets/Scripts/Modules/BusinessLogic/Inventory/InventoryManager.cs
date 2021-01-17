using System.Collections.Generic;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Player;
using Modules.UI.Inventory;
using UnityEngine;

namespace Modules.BusinessLogic.Inventory
{
    public class InventoryManager
    {
        private readonly List<Item.Item> _items = new List<Item.Item>();
        private readonly InventoryUI _inventoryUI;

        private readonly InputManager _inputManager;
        
        public InventoryManager(InputManager inputManager, SnapManager snapManager)
        {
            (_inputManager = inputManager).TouchEnter += InputManagerOnTouchEnter;
            snapManager.ItemSnapped += SnapManagerOnItemSnapped;

            _inventoryUI = Object.FindObjectOfType<InventoryUI>();
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
        
        private void SnapManagerOnItemSnapped(Item.Item item)
        {
            _inventoryUI.Add(item);
            _items.Add(item);
        }
        
        private void DeactivateInventory(Vector2 screenPoint)
        {
            _inventoryUI.Deactivate();
            _inputManager.TouchExit -= DeactivateInventory;
            _inputManager.TouchMoved -= InputManagerOnTouchMoved;
        }
    }
}
