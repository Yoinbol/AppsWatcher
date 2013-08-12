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
        public Response Save(Session session)
        {
            return _service.Save(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="day"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CollectionResponse<SessionHeader> GetSessions(int page, int pageSize, DateTime? day = null, string userName = null)
        {
            return _service.GetSessions(page, pageSize, day, userName);
        }
    }
}
