using ContactManagement.MVC5.App_Start;
using ContactManagement.MVC5.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContactManagement.MVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize Autofac
            AutofacConfig.RegisterDependencies();
        }
        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;

            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index"; // fallback error action

            int statusCode = httpException?.GetHttpCode() ?? 500;
            routeData.Values["statusCode"] = statusCode;

            // Optional: pass extra error details via TempData
            var controller = new ErrorController();
            controller.ViewBag.ErrorMessage = exception.Message;
            controller.ViewBag.ControllerName = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context))?.Values["controller"];
            controller.ViewBag.ActionName = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context))?.Values["action"];

            // Set response code
            Response.StatusCode = statusCode;

            // Execute error controller manually
            IController errorController = controller;
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
