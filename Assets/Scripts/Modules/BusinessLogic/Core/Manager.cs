using Modules.BusinessLogic.Session;

namespace Modules.BusinessLogic.Core
{
    public abstract class Manager
    {
        /// <summary>
        /// Inject session into every manager to init dependencies 
        /// </summary>
        /// <param name="session">Stores every active manager in session</param>
        public abstract void Inject(SessionManager session);
    }
}
