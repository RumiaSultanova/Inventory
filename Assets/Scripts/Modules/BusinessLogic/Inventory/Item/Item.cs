using UnityEngine;

namespace Modules.BusinessLogic.Inventory.Item
{
    [RequireComponent(typeof(ItemData))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private Transform snapzone;
        public Transform Snapzone => snapzone;

        public ItemData ItemData { get; private set; }
    
        private Rigidbody _rigidbody;
        private Collider _collider;
        
        private void Awake()
        {
            ItemData = gameObject.GetComponent<ItemData>();

            _rigidbody = GetComponentInChildren<Rigidbody>();
            _collider = GetComponentInChildren<Collider>();
        }

        /// <summary>
        /// Enable physics to allow object interact with virtual world
        /// </summary>
        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }

        /// <summary>
        /// Disable physics to allow manual interaction with object
        /// </summary>
        public void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
    }
}
