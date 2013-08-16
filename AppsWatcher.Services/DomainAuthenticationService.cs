using System;
using AppsWatcher.Common.Responses;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainAuthenticationService : BaseService, IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Response Authenticate(string userLogin, string password)
        {
            throw new NotImplementedException();
        }
    }
}
