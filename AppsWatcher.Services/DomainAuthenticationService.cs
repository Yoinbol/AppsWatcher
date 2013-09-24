using System;
using AppsWatcher.Common.Models;
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
        public SingleResponse<User> Authenticate(string userLogin, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public Response ResetPassword(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
