using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Contracts;
using Dapper;

namespace AppsWatcher.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionsRepository : Repository<Session>, ISessionsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public SessionsRepository(IDbConnection connection)
            : base(connection)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="sessionId"></param>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public IEnumerable<Session> GetSessions(int start, int end, long? sessionId = null, DateTime? day = null, string userLogin = null)
        {
            return Connection.Query<Session>("GetSessions", new { start, end, sessionId, day, userLogin }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public Session GetSession(DateTime day, string userLogin)
        {
            return Connection.Query<Session>("GetSessions", new { start = 0, end = 0, day, userLogin }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
