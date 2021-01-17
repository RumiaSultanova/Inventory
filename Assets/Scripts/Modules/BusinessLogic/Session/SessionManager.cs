using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.Inventory;
using Modules.BusinessLogic.Player;
using UnityEngine;

namespace Modules.BusinessLogic.Session
{
    public class SessionManager : MonoBehaviour
    {
        public InputManager InputManager { get; private set; }
        public DragManager DragManager { get; private set; }
        public SnapManager SnapManager { get; private set; }
        public InventoryManager InventoryManager { get; private set; }
        
        private void Awake()
        {
            InputManager = gameObject.AddComponent<InputManager>();

            DragManager = new DragManager(InputManager);
            SnapManager = new SnapManager(DragManager);
            InventoryManager = new InventoryManager(InputManager, SnapManager);
        }
    }
}
