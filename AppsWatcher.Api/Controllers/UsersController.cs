using System.Collections.Generic;
using System.Web.Http;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUsersService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public UsersController(IUsersService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPost]
        public SingleResponse<User> Add(User instance)
        {
            return _service.Add(instance);
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
            return _service.UpdatePassword(userId, oldPassword, newPassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CollectionResponse<User> GetAll()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userLogin"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet]
        public CollectionResponse<User> Get(int? userId = null, string userLogin = null, string firstName = null, string lastName = null)
        {
            return _service.Get(userId, userLogin, firstName, lastName);
        }
    }
}
