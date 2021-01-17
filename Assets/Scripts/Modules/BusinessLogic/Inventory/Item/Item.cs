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

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }

        public void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
    }
}
