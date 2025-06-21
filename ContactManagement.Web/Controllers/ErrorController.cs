using System.Web.Mvc;

namespace ContactManagement.MVC5.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            if (ViewBag.ErrorMessage == null)
                ViewBag.ErrorMessage = "An unexpected error occurred.";

            Response.StatusCode = 500;

            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;

            return View();
        }

        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}