using System;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Repositories.Contracts;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionsService : ISessionsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Response Save(Session session)
        {
            throw new NotImplementedException();
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
            var response = new CollectionResponse<SessionHeader>();

            try
            {
                var repository = ComponentsContainer.Instance.Resolve<ISessionsRepository>();
                response.Data = repository.GetAll(); //repository.GetSessions(page, pageSize, day, userName);
            }
            catch (Exception ex)
            {
                response.Succed = false;
                response.Message = "Unexpected error";
            }

            return response;
        }
    }
}
