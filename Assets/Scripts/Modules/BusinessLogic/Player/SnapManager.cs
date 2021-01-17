using System.Collections;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class SnapManager
    {
        private const float MaxDistance = .01f;
        private const float Speed = 2f;
        
        public delegate void OnItem(Item item);
        public event OnItem ItemSnapped;

        private readonly InputManager _inputManager;
        
        public SnapManager(DragManager dragManager)
        {
            dragManager.ItemReleased += DragManagerOnItemReleased;

            _inputManager = dragManager.InputManager;
        }

        private void DragManagerOnItemReleased(Item item, Vector2 screenPoint)
        {
            if (_inputManager.CheckBagTouched(screenPoint))
            {
                ItemSnapped?.Invoke(item);
                item.DisablePhysics();
                item.StartCoroutine(Snap(item, item.Snapzone.position));
            }
        }

        private IEnumerator Snap(Item item, Vector3 target)
        {
            while (Vector3.Distance(item.transform.position, target) > MaxDistance)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, target, Speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
