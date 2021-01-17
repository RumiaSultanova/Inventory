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

        public delegate void OnInput(Item item);
        public event OnInput PointerExit;

        private void Awake()
        {
            SetupGrid();
        }

        /// <summary>
        /// Add items UI to inventory UI and subscribe to item selection
        /// </summary>
        private async void SetupGrid()
        {
            for (var i = 0; i < MaxCount; i++)
            {
                var itemUI = (await Addressables.InstantiateAsync(AssetNames.ItemUI).Task).GetComponent<ItemUI>();
                itemUI.transform.SetParent(content, false);
                itemUI.PointerExit += item => PointerExit?.Invoke(item);
                _cells.Add((false, itemUI));
            }
        }

        /// <summary>
        /// Add item to inventory and fill cell data fields
        /// </summary>
        /// <param name="item">Added item</param>
        public void AddItem(Item item)
        {
            for (var i = 0; i < _cells.Count; i++)
            {
                var cell = _cells[i];
                if (cell.Item1) { continue; }
                
                cell.Item1 = true;
                cell.Item2.AddItem(item);
                _cells[i] = cell;
                break;
            }
        }

        /// <summary>
        /// Remove item from inventory and reset its data fields
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void SelectItem(Item item)
        {
            for (var i = 0; i < _cells.Count; i++)
            {
                var cell = _cells[i];
                if (cell.Item2.Item == item)
                {
                    cell.Item2.Reset();
                    cell.Item1 = false;
                    _cells[i] = cell;
                    return;
                }
            }

        }

        /// <summary>
        /// Activate inventory UI
        /// </summary>
        public void Activate()
        {
            canvas.enabled = true;
        }

        /// <summary>
        /// Deactivate inventory UI
        /// </summary>
        public void Deactivate()
        {
            canvas.enabled = false;
        }
    }
}