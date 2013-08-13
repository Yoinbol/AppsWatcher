using System;
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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="day"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<Session> GetSessions(int page, int pageSize, DateTime? day = null, string userName = null)
        {
            return Connection.Query<Session>("GetSessions", new { page, pageSize, day, userName }, commandType: CommandType.StoredProcedure);
        }
    }
}
