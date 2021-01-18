using Modules.BusinessLogic.Inventory.Item;
using Modules.UI.Inventory;
using UnityEngine;

namespace Modules.BusinessLogic.Session
{
    /// <summary>
    /// Defined for certain level to manage level 3D and UI content
    /// </summary>
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject bag;
        [SerializeField] private Item[] items;
        [SerializeField] private InventoryUI inventoryUI;

        public GameObject Bag => bag;
        public Item[] Items => items;
        public InventoryUI InventoryUI => inventoryUI;
    }
}