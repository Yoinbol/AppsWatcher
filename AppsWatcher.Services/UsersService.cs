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
    public class UsersService : BaseService, IUsersService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public SingleResponse<User> Add(User instance)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CollectionResponse<User> GetAll()
        {
            var response = new CollectionResponse<User>();

            try
            {
                var usersRepository = ComponentsContainer.Instance.Resolve<IUsersRepository>();
                response.Data = usersRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                response.Succed = false;
                response.Message = "Unexpected error";
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userLogin"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public CollectionResponse<User> Get(int? userId = null, string userLogin = null, string firstName = null, string lastName = null)
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
        public Response UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
