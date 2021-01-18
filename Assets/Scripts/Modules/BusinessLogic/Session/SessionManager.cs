using System.Collections.Generic;
using Modules.BusinessLogic.Core;
using Modules.BusinessLogic.CustomInput;
using Modules.BusinessLogic.DataProvider;
using Modules.BusinessLogic.Inventory;
using Modules.BusinessLogic.Player;
using UnityEngine;

namespace Modules.BusinessLogic.Session
{
    /// <summary>
    /// Defined for certain level to manage services and theirs interactions
    /// </summary>
    public class SessionManager : MonoBehaviour
    {
        [SerializeField] private Level level;
        public Level Level => level;
        public InputManager InputManager { get; private set; }
        public DragManager DragManager { get; private set; }
        public SnapManager SnapManager { get; private set; }
        public InventoryManager InventoryManager { get; private set; }
        public DataProviderManager DataProviderManager { get; private set; }

        private readonly List<Manager> _managers = new List<Manager>();
        
        /// <summary>
        /// Add managers to scene
        /// </summary>
        private void Awake()
        {
            InputManager = gameObject.AddComponent<InputManager>();

            _managers.Add(DragManager = new DragManager());
            _managers.Add(SnapManager = new SnapManager());
            _managers.Add(InventoryManager = new InventoryManager());
            _managers.Add(DataProviderManager = new DataProviderManager());
        }

        /// <summary>
        /// Initialize current scene managers
        /// </summary>
        private void Start()
        {
            foreach (var manager in _managers)
            {
                manager.Inject(this);
            }
        }
    }
}
