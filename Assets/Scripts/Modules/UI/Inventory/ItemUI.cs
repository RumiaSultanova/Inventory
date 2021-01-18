using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.UI.Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Image iconImage;

        [SerializeField] private Button button;
        private EventTrigger _trigger;

        public Item Item { get; private set; }

        public delegate void OnInput(Item item);
        public event OnInput PointerExit;
        
        private void Awake()
        {
            SetupTrigger();
            Reset();
        }

        /// <summary>
        /// Initialize listener of stop pressing mouse on item's UI
        /// </summary>
        private void SetupTrigger()
        {
            _trigger = gameObject.AddComponent<EventTrigger>();

            var pointerExit = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
            pointerExit.callback.AddListener(item => PointerExit?.Invoke(Item));
            _trigger.triggers.Add(pointerExit);
        }

        /// <summary>
        /// Add new item to UI, make interactable and fill name text field and icon image
        /// </summary>
        /// <param name="item">Added item</param>
        public void AddItem(Item item)
        {
            Item = item;
            
            nameText.text = Item.ItemData.ItemName;
            iconImage.sprite = Item.ItemData.Sprite;
           
            button.interactable = true;
            _trigger.enabled = true;
        }

        /// <summary>
        /// Remove item data from UI and make non-interactable 
        /// </summary>
        public void Reset()
        {
            nameText.text = "";
            iconImage.sprite = null;

            button.interactable = false;
            _trigger.enabled = false;
        }
    }
}
