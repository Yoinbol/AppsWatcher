using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SingleResponse<User> Authenticate(string userLogin, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Response ResetPassword(int userId, string oldPassword, string newPassword);
    }
}
