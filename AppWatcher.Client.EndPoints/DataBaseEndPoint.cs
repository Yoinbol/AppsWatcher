using System;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseEndPoint : EndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISessionsService _sessionsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionsService"></param>
        public DataBaseEndPoint(ISessionsService sessionsService)
        {
            _sessionsService = sessionsService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override Response Save(Session session)
        {
            return _sessionsService.Save(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public override SingleResponse<Session> LoadSession(DateTime day, string userLogin)
        {
            return _sessionsService.GetSession(day, userLogin);
        }
    }
}
