using System;
using System.Collections.Generic;
using AppsWatcher.Common.Models;

namespace AppsWatcher.Repositories.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISessionsRepository : IRepository<Session>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="sessionId"></param>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        IEnumerable<Session> GetSessions(int start, int end, long? sessionId = null, DateTime? day = null, string userLogin = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <param name="pullDetail"></param>
        /// <returns></returns>
        Session GetSession(DateTime day, string userLogin, bool pullDetail = false);
    }
}
