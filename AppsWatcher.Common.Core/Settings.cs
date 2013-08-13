using System.Configuration;

namespace AppsWatcher.Common.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        internal static string ConnectionString
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["AppsWatcher"];
                return conn != null ? conn.ConnectionString : string.Empty;
            }
        }
    }
}
