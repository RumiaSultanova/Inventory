using System.Collections.Generic;
using Modules.BusinessLogic.Inventory.Item;
using Modules.UI.Inventory;
using Modules.Utils;
using UnityEngine.AddressableAssets;

namespace Modules.BusinessLogic.Player
{
    public class InventoryManager
    {
        private List<Item> _items = new List<Item>();
        private InventoryUI _inventoryUI;

        public InventoryManager(SnapManager snapManager)
        {
            snapManager.ItemSnapped += SnapManagerOnItemSnapped;
            
            _inventoryUI = Addressables.InstantiateAsync(AssetNames.InventoryUI).Result.GetComponent<InventoryUI>();
        }

        private void SnapManagerOnItemSnapped(Item item)
        {
            _items.Add(item);
            _inventoryUI.Add(item);
        }
    }
}
