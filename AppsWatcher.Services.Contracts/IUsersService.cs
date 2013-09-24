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
        SingleResponse<User> Add(NewUser instance);

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
    }
}
