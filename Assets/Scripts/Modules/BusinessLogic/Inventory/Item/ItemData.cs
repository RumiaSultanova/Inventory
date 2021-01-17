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
        [SerializeField] private Sprite sprite;
        
        public int ID => id;
        public ItemType ItemType => type;
        public string ItemName => itemName;
        public float Width => width;
        public Sprite Sprite => sprite;

    }
}
