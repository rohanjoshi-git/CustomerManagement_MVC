using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace CustomerManagement
{
    public class MyCustomExFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);  // it will simply return error.cshtml
            ViewResult objViewResult = new ViewResult();
            objViewResult.ViewName = "Error";
            filterContext.Result = objViewResult;
            filterContext.ExceptionHandled = true;

            Exception e = filterContext.Exception;

            // logic for logging exceptions - create file, or log into database, etc
                
        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Start - Exception Handling
            // to set different views for different errors
            HandleErrorAttribute a1 = new HandleErrorAttribute();
            a1.ExceptionType = typeof(DivideByZeroException);
            a1.View = "DivideByZeroError";
            GlobalFilters.Filters.Add(a1);  // no need to apply [HandleError] on indidual actions or controllers

            // to set different views for different errors
            HandleErrorAttribute a2 = new HandleErrorAttribute();
            a2.ExceptionType = typeof(NullReferenceException);
            a2.View = "NullRefError";
            GlobalFilters.Filters.Add(a2);  // no need to apply [HandleError] on indidual actions or controllers

            // if none of above errors matches
            GlobalFilters.Filters.Add(new HandleErrorAttribute());  // no need to apply [HandleError] on indidual actions or controllers

            // End - Exception Handling

        }
    }
}
