using System.Collections.Generic;
using Modules.BusinessLogic.Inventory.Item;
using Modules.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform content;

        private List<(bool, ItemUI)> _items = new List<(bool, ItemUI)>();
        
        private const int MaxCount = 3;
        
        private void Awake()
        {
            for (int i = 0; i < MaxCount; i++)
            {
                var itemUI = Addressables.InstantiateAsync(AssetNames.ItemUI).Result;
                itemUI.transform.SetParent(content);
                _items.Add((false, itemUI.GetComponent<ItemUI>()));
            }
        }

        public void Add(Item item)
        {
            foreach (var itemUI in _items)
            {
                
            }
        }
    }
}
