using System;
using System.Web.Http;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISessionsService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public SessionsController(ISessionsService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpPost]
        public Response Save(Session session)
        {
            return _service.Save(session);
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
        [HttpGet]
        public CollectionResponse<SessionHeader> GetSessions(int start, int end, long? sessionId = null, DateTime? day = null, string userLogin = null)
        {
            return _service.GetSessions(start, end, sessionId, day, userLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpGet]
        public SingleResponse<Session> GetSession(DateTime day, string userLogin)
        {
            return _service.GetSession(day, userLogin);
        }
    }
}
