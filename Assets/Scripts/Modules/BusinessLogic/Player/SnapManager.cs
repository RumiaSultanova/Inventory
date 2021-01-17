using System.Collections;
using Modules.BusinessLogic.Inventory.Item;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class SnapManager
    {
        private const float RadiusToSnap = .5f;
        private const float MaxDistance = .01f;
        private const float Speed = 2f;

        public delegate void OnItem(Item item);
        public event OnItem ItemSnapped;
        
        public SnapManager(DragManager dragManager)
        {
            dragManager.ItemReleased += DragManagerOnItemReleased;
        }

        private void DragManagerOnItemReleased(Item item)
        {
            var vector = item.transform.position - item.Snapzone.position;
            vector.y = 0;
            if (vector.magnitude > RadiusToSnap) { return; }
            
            item.DisablePhysics();
            item.StartCoroutine(Snap(item, item.Snapzone.position));
        }

        private IEnumerator Snap(Item item, Vector3 target)
        {
            while (Vector3.Distance(item.transform.position, target) > MaxDistance)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, target, Speed * Time.deltaTime);
                yield return null;
            }
            ItemSnapped?.Invoke(item);
        }
    }
}
