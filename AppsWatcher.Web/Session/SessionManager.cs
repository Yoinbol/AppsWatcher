using System.Web;
using AppsWatcher.Common.Models;

namespace AppsWatcher.Web.Session
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionManager : ISessionManager
    {
        /// <summary>
        /// 
        /// </summary>
        public User CurrentUser
        {
            get
            {
                var currentUser = HttpContext.Current.Session["CurrentUser"];

                if (currentUser != null)
                    return (User)currentUser;

                return null;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}
