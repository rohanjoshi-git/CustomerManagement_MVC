using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Dal;
using CustomerManagement.Models;
using System.Web.Security;

namespace CustomerManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Authenticate()
        {
            return View("Login");
        }

        public ActionResult Validate()
        {
            // Forms Authentication
            string username = Request.Form["UserName"].Trim(); // to remove leading or trailing blank spaces
            string password = Request.Form["Password"].Trim();

            Dal.Dal dal = new Dal.Dal();

            List<User> users = ( from u in dal.Users
                                 where (u.UserName == username)
                                 && (u.Password == password)
                                 select u
                                 ).ToList<User>();

            if(users.Count == 1)
            {
                FormsAuthentication.SetAuthCookie("Cookie", true);
                return View("GotoHome");
            }
            else
            {
                return View("Login");
            }

        }
    }
}