using System.Web.Mvc;
using AppsWatcher.Common.Core;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.WebHost.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string password)
        {
            var service = ComponentsContainer.Instance.Resolve<IAuthenticationService>();
            var response = service.Authenticate(user, password);

            if (response.Succed)
            {
                SessionManager.CurrentUser = response.Data;
                return View("Dashboard", response.Data);
            }

            if (string.IsNullOrEmpty(response.Message))
            {
                ViewBag.ResponseMessage = response.Message;
            }

            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            SessionManager.Clear();
            return View("Index");
        }
    }
}
