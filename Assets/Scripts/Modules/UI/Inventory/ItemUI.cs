using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Image iconImage;

        private Item _item;

        private void Awake()
        {
            Deactivate();
        }

        public void SetItem(Item item)
        {
            nameText.text = item.ItemData.ItemName;
            iconImage.sprite = item.ItemData.Sprite;
        }

        public void Reset()
        {
            Deactivate();
        }

        private void Deactivate()
        {
            nameText.text = "";
            iconImage.sprite = null;
        }
    }
}
