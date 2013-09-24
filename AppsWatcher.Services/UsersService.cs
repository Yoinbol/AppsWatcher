using System;
using System.Transactions;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Repositories.Contracts;
using AppsWatcher.Services.Contracts;
using AppsWatcher.Services.Helpers.Contracts;
using Autofac;

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
        public SingleResponse<User> Add(NewUser instance)
        {
            var response = new SingleResponse<User>();

            try
            {
                using (var lifetimeScope = ComponentsContainer.Instance.BeginLifetimeScope())
                {
                    using (var transactionScope = new TransactionScope())
                    {
                        var usersRepository = lifetimeScope.Resolve<IUsersRepository>();
                        var user = usersRepository.GetFirst(new { instance.UserLogin });

                        if (user == null)
                        {
                            if (!string.IsNullOrEmpty(instance.FirstName) && !string.IsNullOrEmpty(instance.LastName) && !string.IsNullOrEmpty(instance.Password))
                            {
                                var securityProvider = lifetimeScope.Resolve<ISecurityProvider>();
                                var salt = securityProvider.CreateSalt();

                                user = new User 
                                {
                                    FirstName = instance.FirstName,
                                    LastName = instance.LastName,
                                    Email = instance.Email,
                                    UserLogin = instance.UserLogin,
                                    Password = securityProvider.Compute(instance.Password, salt),
                                    Salt = salt
                                };

                                usersRepository.Add(user);
                                transactionScope.Complete();
                                response.Data = user;
                            }
                            else
                            {
                                response.Succed = false;
                                response.Message = "You are missing some information about this user";
                            }
                        }
                        else
                        {
                            response.Succed = false;
                            response.Message = string.Format("User {0} already exists into the system", user.UserLogin);
                        }
                    }
                }
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
        public Response ResetPassword(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
