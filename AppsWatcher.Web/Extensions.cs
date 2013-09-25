using System.Web.Mvc;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Web.Session;

namespace AppsWatcher.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static User GetCurrentUser(this HtmlHelper helper)
        {
            return ComponentsContainer.Instance.Resolve<ISessionManager>().CurrentUser;
        }
    }
}
