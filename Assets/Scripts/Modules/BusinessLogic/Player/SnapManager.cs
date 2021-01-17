using System.Collections;
using Modules.BusinessLogic.Core;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory.Item;
using Modules.BusinessLogic.Session;
using UnityEngine;

namespace Modules.BusinessLogic.Player
{
    public class SnapManager : Manager
    {
        private const float MaxDistance = .01f;
        private const int MaxDistancePixels = 5;
        private const float Speed = 2f;
        
        public delegate void OnItem(Item item);
        public event OnItem ItemSnapped;
        public event OnItem ItemSnapFinished;

        private InputManager _inputManager;
        private DragManager _dragManager;
        
        public override void Inject(SessionManager session)
        {
            _inputManager = session.InputManager;
            (_dragManager = session.DragManager).ItemReleased += DragManagerOnItemReleased;
            session.InventoryManager.Selected += InventoryManagerOnSelected;
        }

        /// <summary>
        /// Check if item released on bag to smoothly snap it to bag and add it to inventory
        /// </summary>
        /// <param name="item">Released item</param>
        /// <param name="screenPoint">Point in screen dimension</param>
        private void DragManagerOnItemReleased(Item item, Vector2 screenPoint)
        {
            if (_inputManager.CheckBagTouched(screenPoint))
            {
                ItemSnapped?.Invoke(item);
                item.DisablePhysics();
                item.StartCoroutine(SnapToTransform(item, item.Snapzone));
            }
        }

        /// <summary>
        /// Unsnap item from bag and smoothly move it to mouse, then drop (because need to long tab to drag item) 
        /// </summary>
        /// <param name="item">Last touched item</param>
        private void InventoryManagerOnSelected(Item item)
        {
            item.StopAllCoroutines();
            item.StartCoroutine(SnapToMouse(item));
        }
        
        /// <summary>
        /// Smooth movement to snap zone transform
        /// </summary>
        /// <param name="item">Snapping item</param>
        /// <param name="target">Snap zone transform</param>
        /// <returns></returns>
        private IEnumerator SnapToTransform(Item item, Transform target)
        {
            while (Vector3.Distance(item.transform.position, target.position) > MaxDistance)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, target.position, Speed * Time.deltaTime);
                yield return null;
            }
            ItemSnapFinished?.Invoke(item);
            yield return null;
        }

        /// <summary>
        /// Smooth movement to mouse position
        /// </summary>
        /// <param name="item">Snapping item</param>
        /// <returns></returns>
        private IEnumerator SnapToMouse(Item item)
        {
            _dragManager.StartMove();
            var screenPoint = _inputManager.cam.WorldToScreenPoint(item.transform.position);
            screenPoint.z = 0;
            while (Vector3.Distance(screenPoint, Input.mousePosition) > MaxDistancePixels)
            {
                var targetPosition = Vector3.Lerp(screenPoint, Input.mousePosition, Speed * Time.deltaTime);
                _dragManager.Move(item, targetPosition);
                screenPoint = _inputManager.cam.WorldToScreenPoint(item.transform.position);
                screenPoint.z = 0;
                yield return null;
            }
            item.EnablePhysics();
            yield return null;
        }
    }
}
