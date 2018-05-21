using System.Web.Mvc;

namespace cjcdonate.Controllers
{
    public class AccessDeniedController : Controller
    {
        // GET: AccessDenied
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}