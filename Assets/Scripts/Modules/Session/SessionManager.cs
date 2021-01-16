using Modules.CustomInput;
using Modules.Player;
using UnityEngine;

namespace Modules.Session
{
    public class SessionManager : MonoBehaviour
    {
        public InputManager InputManager { get; private set; }
        public DragManager DragManager { get; private set; }
        
        private void Awake()
        {
            InputManager = gameObject.AddComponent<InputManager>();

            DragManager = new DragManager(InputManager);
        }
    }
}
