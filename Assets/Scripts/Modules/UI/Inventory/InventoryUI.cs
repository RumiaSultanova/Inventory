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
        [SerializeField] private Canvas canvas;
        
        private readonly List<(bool, ItemUI)> _cells = new List<(bool, ItemUI)>();
        
        private const int MaxCount = 3;

        public bool IsActive => canvas.enabled;
        
        private void Awake()
        {
            SetupGrid();
        }

        private async void SetupGrid()
        {
            for (var i = 0; i < MaxCount; i++)
            {
                var itemUI =  await Addressables.InstantiateAsync(AssetNames.ItemUI).Task;
                itemUI.transform.SetParent(content, false);
                _cells.Add((false, itemUI.GetComponent<ItemUI>()));
            }
        }

        public void Add(Item item)
        {
            for (var i = 0; i < _cells.Count; i++)
            {
                var cell = _cells[i];
                if (cell.Item1) { continue; }
                
                cell.Item1 = true;
                cell.Item2.SetItem(item);
                _cells[i] = cell;
                break;
            }
        }

        public void Activate()
        {
            canvas.enabled = true;
        }

        public void Deactivate()
        {
            canvas.enabled = false;
        }
    }
}