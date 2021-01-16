using UnityEngine;

namespace Modules.Inventory.Item
{
    [RequireComponent(typeof(Item))]
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private Transform snapzone;
        public Transform Snapzone => snapzone;

        public Item Item { get; private set; }
    
        private Rigidbody _rigidbody;
        private Collider _collider;
        
        private void Awake()
        {
            Item = gameObject.GetComponent<Item>();

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
