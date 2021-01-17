using Modules.BusinessLogic.Session;

namespace Modules.BusinessLogic.Core
{
    public abstract class Manager
    {
        public abstract void Inject(SessionManager session);
    }
}
