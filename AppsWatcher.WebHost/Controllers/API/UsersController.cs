using System.Collections.Generic;
using System.Web.Http;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.WebHost.Controllers.API
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
        public SingleResponse<User> Add(NewUser instance)
        {
            return _service.Add(instance);
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
