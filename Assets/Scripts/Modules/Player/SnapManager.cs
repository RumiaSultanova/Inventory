using System.Collections;
using Modules.Inventory.Item;
using UnityEngine;

namespace Modules.Player
{
    public class SnapManager
    {
        private const float RadiusToSnap = .5f;
        private const float MaxDistance = .01f;
        
        public SnapManager(DragManager dragManager)
        {
            dragManager.ItemReleased += DragManagerOnItemReleased;
        }

        private void DragManagerOnItemReleased(ItemManager itemManager)
        {
            var item = itemManager.transform;
            var snapzone = itemManager.Snapzone;
            if (Vector3.Distance(item.position, item.position) <= RadiusToSnap)
            {
                itemManager.DisablePhysics();
                itemManager.StartCoroutine(Snap(item, snapzone.position));
            }
        }

        private IEnumerator Snap(Transform item, Vector3 target)
        {
            while (Vector3.Distance(item.position, target) > MaxDistance)
            {
                item.position = Vector3.Lerp(item.position, target, Time.deltaTime);
                yield return null;
            }
        }
    }
}
