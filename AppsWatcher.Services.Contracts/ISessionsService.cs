using System;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="day"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        CollectionResponse<SessionHeader> GetSessions(int page, int pageSize, DateTime? day = null, string userName = null);
    }
}
