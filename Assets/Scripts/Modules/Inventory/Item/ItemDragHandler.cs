using UnityEngine;

namespace Modules.Inventory.Item
{
    public class ItemDragHandler : MonoBehaviour
    {
        private Camera _cam;
        private Rigidbody _rigidbody;
        private Collider _collider;

        private float gapFromGround = 1;
        private Plane _plane;
    
        private void Awake()
        {
            _cam = Camera.main;
           
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _collider = GetComponentInChildren<Collider>();
        }
    
        private void OnMouseDown()
        {
            DisablePhysics();
            
            _plane = new Plane(Vector3.up, Vector3.up * gapFromGround);
        }

        private void OnMouseDrag()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if(_plane.Raycast(ray, out var distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }
    
        private void OnMouseUp()
        {
            EnablePhysics();
        }
    
        private void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }
    
        private void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
    }
}
