using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject disabledState;

        [SerializeField] private Text nameText;
        [SerializeField] private Image iconImage;

        private void Awake()
        {
            Deactivate();
        }

        public void SetItem(string itemName, Sprite icon)
        {
            nameText.text = itemName;
            iconImage.sprite = icon;
            Activate();
        }

        public void Reset()
        {
            Deactivate();
        }
        
        private void Activate()
        {
            SwitchState(true);
        }

        private void Deactivate()
        {
            SwitchState(false);
        }

        private void SwitchState(bool state)
        {
            activeState.SetActive(state);
            disabledState.SetActive(!state);
        }
    }
}
