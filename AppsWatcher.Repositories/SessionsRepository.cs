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
        /// <param name="pullDetail"></param>
        /// <returns></returns>
        public Session GetSession(DateTime day, string userLogin, bool pullDetail = false)
        {
            var result = Connection.QueryMultiple("GetSessions", new { start = 0, end = 0, day, userLogin, pullDetail }, commandType: CommandType.StoredProcedure);
            var session = result.Read().Select(Parse).FirstOrDefault();

            if (session != null && pullDetail)
            {
                session.Applications = result.Read<ApplicationTrack>().ToList();
            }

            return session;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public override Session Parse(dynamic row)
        {
            return new Session
            {
                SessionId = row.SessionId,
                Day = row.Day,
                AddedOn = row.AddedOn,
                Duration = row.Duration,
                UserId = row.UserId,
                User = new User 
                {
                    UserId = row.UserId,
                    UserLogin = row.UserLogin,
                    FirstName = row.FirstName,
                    LastName = row.LastName
                }
            };
        }
    }
}
