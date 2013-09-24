using System;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Repositories.Contracts;
using AppsWatcher.Services.Contracts;
using AppsWatcher.Services.Helpers.Contracts;

namespace AppsWatcher.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalAuthenticationService : BaseService, IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SingleResponse<User> Authenticate(string userLogin, string password)
        {
            var response = new SingleResponse<User>();

            try
            {
                //Get the user
                var usersRepository = ComponentsContainer.Instance.Resolve<IUsersRepository>();
                var user = usersRepository.GetFirst(new { userLogin });

                //Does the user exists
                if (user != null)
                {
                    var authenticationResponse = Authenticate(user, password);

                    if (authenticationResponse.Succed)
                    {
                        response.Succed = true;
                        response.Data = user;
                    }
                    else
                    {
                        response.Succed = false;
                        response.Message = authenticationResponse.Message;
                    }
                }
                else
                {
                    response.Succed = false;
                    response.Message = "User does not exists";
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
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public Response ResetPassword(int userId, string oldPassword, string newPassword)
        {
            var response = new Response();

            try
            {
                //Get the user
                var usersRepository = ComponentsContainer.Instance.Resolve<IUsersRepository>();
                var user = usersRepository.GetFirst(new { userId });

                //Does the user exists
                if (user != null)
                {
                    if (user.Salt.HasValue && user.Password != null)
                    {
                        response = Authenticate(user, oldPassword);

                        if (response.Succed) 
                        {
                            //Update
                        }
                    }
                    else
                    {
                        //User does not have local credentials
                    }
                }
                else
                {
                    response.Succed = false;
                    response.Message = "User does not exists";
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                response.Succed = false;
                response.Message = "Unexpected error";
            }

            return response;
        }

        #region Internal utility

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected Response Authenticate(User user, string password)
        {
            var response = new Response();

            if (user.Salt.HasValue && user.Password != null)
            {
                //Validate the password
                string actualPassword = user.Password.GetString();
                var securityProvider = ComponentsContainer.Instance.Resolve<ISecurityProvider>();
                password = securityProvider.Compute(password, user.Salt.Value).GetString();

                //Is the password correct??
                if (actualPassword.Equals(password, StringComparison.InvariantCultureIgnoreCase))
                {
                    response.Succed = true;
                    response.Message = string.Empty;
                }
                else
                {
                    response.Succed = false;
                    response.Message = "Invalid password";
                }
            }
            else
            {
                //User does not have local credentials
                response.Succed = false;
                response.Message = "User does not have local credentials";
            }

            return response;
        }

        #endregion
    }
}
