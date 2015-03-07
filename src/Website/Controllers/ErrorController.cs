using System.Web.Mvc;

namespace Prekenweb.Website.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        [HandleError]
        public ActionResult Unknown()
        {
            return View();
        }

        [HandleError]
        public ActionResult NoAccess()
        {
            return View();
        }
        
        [HandleError]
        public ActionResult NotFound()
        {
            return View();
        }

        [HandleError]
        public ActionResult ServerError()
        {
            return View();
        }

    }
}
