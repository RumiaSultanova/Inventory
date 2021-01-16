using UnityEngine;

namespace Modules.Utils
{
    public class LookToCamera : MonoBehaviour
    {
        private const float MaxAngle = .1f;

        private Camera _camera;

        private Canvas _canvas;
        
        private void Awake()
        {
            _camera = Camera.main;
            _canvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            if (!_canvas.enabled) { return; }
            
            var vector = transform.position - _camera.transform.position;
            
            if (vector.magnitude < MaxAngle) { return; }

            vector.x = 0f;
            var direction = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime);
        }
    }
}