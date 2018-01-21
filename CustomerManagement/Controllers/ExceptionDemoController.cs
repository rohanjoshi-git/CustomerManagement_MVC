using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Controllers
{

    /*
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);

            ViewResult objViewResult = new ViewResult();
            objViewResult.ViewName = "Error";
            filterContext.Result = objViewResult;

            filterContext.ExceptionHandled = true; // compulsory line 
        }
    }
    */

    public class ExceptionDemoController : Controller
    {
        // GET: ExceptionDemo
        //[HandleError]  // will return error.cshtml if error occurs  // handled at global.aspx
        //[HandleError(ExceptionType = typeof(DivideByZeroException), View = "DivideByZeroError")] // to apply attribute at Action level
        public ActionResult Index()
        {
            int i = 0;
            i /= i;
            return View();
        }

        public ActionResult Index2()
        {
            // logic
            int i = 0;
            i /= i;
            return View();
        }
    }
}