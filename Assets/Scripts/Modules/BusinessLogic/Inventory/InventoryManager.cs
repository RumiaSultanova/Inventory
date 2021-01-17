using System.Collections.Generic;
using Modules.BusinessLogic.Player;
using Modules.UI.Inventory;
using UnityEngine;

namespace Modules.BusinessLogic.Inventory
{
    public class InventoryManager
    {
        private readonly List<Item.Item> _items = new List<Item.Item>();
        private readonly InventoryUI _inventoryUI;

        public InventoryManager(SnapManager snapManager)
        {
            snapManager.ItemSnapped += SnapManagerOnItemSnapped;
            
            _inventoryUI = Object.FindObjectOfType<InventoryUI>();
        }

        private void SnapManagerOnItemSnapped(Item.Item item)
        {
            _inventoryUI.Add(item);
            _items.Add(item);
        }
    }
}
