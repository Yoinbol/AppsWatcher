using System;
using AppsWatcher.Common.Core;
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
        public Response Authenticate(string userLogin, string password)
        {
            var response = new Response();

            try
            {
                //Get the user
                var usersRepository = ComponentsContainer.Instance.Resolve<IUsersRepository>();
                var user = usersRepository.GetFirst(new { userLogin });

                //Does the user exists
                if (user != null)
                {
                    //Validate the password
                    string actualStoredPassword = user.Password.GetString();
                    var securityProvider = ComponentsContainer.Instance.Resolve<ISecurityProvider>();
                    password = securityProvider.Compute(password, user.Salt).GetString();

                    //Is the password correct??
                    if (actualStoredPassword.Equals(password, StringComparison.InvariantCultureIgnoreCase))
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
    }
}
