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
        Response Authenticate(string userLogin, string password);
    }
}
