using System.Web.Mvc;
using AppsWatcher.Common.Core;
using AppsWatcher.Web.Session;

namespace AppsWatcher.WebHost.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected ISessionManager SessionManager
        {
            get
            {
                return ComponentsContainer.Instance.Resolve<ISessionManager>();
            }
        }
    }
}
