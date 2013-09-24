using System.Web.Mvc;
using AppsWatcher.Common.Core;
using AppsWatcher.Services.Contracts;

namespace AppsWatcher.WebHost.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string password)
        {
            var service = ComponentsContainer.Instance.Resolve<IAuthenticationService>();
            var response = service.Authenticate(user, password);

            if (response.Succed)
            {
                return View("Dashboard", response.Data);
            }

            if (string.IsNullOrEmpty(response.Message))
            {
                ViewBag.ResponseMessage = response.Message;
            }

            return View("Index");
        }
    }
}
