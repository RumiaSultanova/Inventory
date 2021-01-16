using Model.Item;
using UnityEngine;

namespace Modules.BusinessLogic.Inventory.Item
{
    public class ItemData : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private ItemType type;
        [SerializeField] private string itemName;
        [SerializeField] private float width;
    }
}
