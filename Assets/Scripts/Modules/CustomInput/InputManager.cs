using System;
using UnityEngine;

namespace Modules.CustomInput
{
    public class InputManager : MonoBehaviour
    {
        public delegate void OnInput(Vector2 touch);
        public event OnInput TouchEnter;
        public event OnInput TouchStay;
        public event OnInput TouchMoved;
        public event OnInput TouchExit;

        private const float MoveDelta = .05f;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchEnter?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                TouchStay?.Invoke(Input.mousePosition);
                if (Math.Abs(Input.GetAxis("Mouse X")) < MoveDelta && Math.Abs(Input.GetAxis("Mouse Y")) < MoveDelta) { return; }
                TouchMoved?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                TouchExit?.Invoke(Input.mousePosition);
            }
        }
    }
}
