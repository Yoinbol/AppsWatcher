using AppsWatcher.Common.Models;

namespace AppsWatcher.Web.Session
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// 
        /// </summary>
        User CurrentUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
}
