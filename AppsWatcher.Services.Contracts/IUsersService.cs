using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        SingleResponse<User> Add(User instance);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        CollectionResponse<User> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userLogin"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        CollectionResponse<User> Get(int? userId = null, string userLogin = null, string firstName = null, string lastName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Response UpdatePassword(int userId, string oldPassword, string newPassword);
    }
}
