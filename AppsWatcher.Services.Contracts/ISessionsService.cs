using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISessionsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Response Save(Session session);
    }
}
