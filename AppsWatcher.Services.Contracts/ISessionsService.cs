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
        SingleResponse<long> Save(Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="day"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        CollectionResponse<SessionHeader> GetSessions(int start, int end, long? sessionId = null, DateTime? day = null, string userLogin = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        SingleResponse<Session> GetSession(DateTime day, string userLogin);
    }
}
