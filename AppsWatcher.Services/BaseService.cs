using System.Reflection;
using log4net;

namespace AppsWatcher.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
