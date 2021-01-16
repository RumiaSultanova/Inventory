using Model.Item;
using UnityEngine;

namespace Modules.Inventory.Item
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private ItemType type;
        [SerializeField] private string itemName;
        [SerializeField] private float width;
    }
}
