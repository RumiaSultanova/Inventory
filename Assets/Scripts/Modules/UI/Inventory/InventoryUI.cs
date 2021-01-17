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
                }
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