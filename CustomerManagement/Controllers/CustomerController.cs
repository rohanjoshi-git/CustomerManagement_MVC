using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Models;
using CustomerManagement.Dal;
using CustomerManagement.ViewModel;
using System.Threading;

namespace CustomerManagement.Controllers
{
    [Authorize]
    public class CustomerUIController : Controller
    {
        // GET: Customer
        public ActionResult Load() // Connecting via browser HTML
        {
            Customer obj =
                new Customer
                {
                    CustomerCode = "1001",
                    CustomerName = "Shiv"
                };

            if (Request.QueryString["Type"] == "JSON")
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("Customer", obj);
            }

        }

        public JsonResult LoadJSON()  // Connecting via JavaScript (not reqd a seperate action)
        {
            Customer obj =
                new Customer
                {
                    CustomerCode = "1001",
                    CustomerName = "Shiv"
                };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Enter()
        {
            //return View("EnterCustomer", new Customer());

            // View Model object
            CustomerViewModel obj = new CustomerViewModel();
            // single object is fresh
            obj.customer = new Customer(); // initially empty

            // fill the customers collections - Removed as it will happen in getCustomers

            return View("EnterCustomer", obj);
        }

        public ActionResult EnterSearch()
        {
            // View Model object
            CustomerViewModel obj = new CustomerViewModel();
            obj.customers = new List<Customer>();
            return View("SearchCustomer", obj);
        }

        
        /* commented methods which send JSON data
        // returns all customers
        public ActionResult getCustomers() // JSON Collection
        {
            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            // Delay for Synchronous execution (10sec)
            //Thread.Sleep(3000);

            return Json(customersColl, JsonRequestBehavior.AllowGet);
        }

        // returns the Customer for entered Customor Name
        [ActionName("getCustomerByName")]
        public ActionResult getCustomers(Customer objCustomer) // JSON Collection
        {
            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = (from c in dal.Customers
                                            where c.CustomerName == objCustomer.CustomerName
                                            select c).ToList<Customer>();

            // Delay for Synchronous execution (10sec)
           // Thread.Sleep(3000);

            return Json(customersColl, JsonRequestBehavior.AllowGet);
        }


        // return the Customer for entered Customor Name
        // returns View - SearchCustomer
        public ActionResult SearchCustomer()
        {
            // View Model object
            CustomerViewModel obj = new CustomerViewModel();

            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            // to fill all customers
            // List<Customer> customersColl = dal.Customers.ToList<Customer>();

            string str = Request.Form["txtCustomerName"].ToString();

            // to filter customers with conditions
            List<Customer> customersColl
                = (from x in dal.Customers
                   where x.CustomerName == str
                   select x).ToList<Customer>();
            obj.customers = customersColl;

            return View("SearchCustomer", obj);
        }

        */

        public ActionResult Submit(Customer obj) // validation runs
        {

            //// no need - as JSON object is being passed from front end
            //// manual binding of object with form elements
            //Customer obj = new Customer();
            //obj.CustomerName = Request.Form["customer.CustomerName"];
            //obj.CustomerCode = Request.Form["customer.CustomerCode"];
            //// no need - end

            if (ModelState.IsValid)
            {
                // insert the Customer object to database
                // EF DAL
                Dal.Dal Dal = new Dal.Dal();
                Dal.Customers.Add(obj); // in memory
                Dal.SaveChanges(); // physical commit

                //return View("Customer", obj);
            }

            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            //return View("EnterCustomer",vm);    // removed as JSON will be sent 
            return Json(customersColl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnterCustomer()
        {
            return View();
        }

        //[HandleError]  // will return error.cshtml if error occurs // handled at global.aspx
        public ActionResult SearchCustomer()
        {
            return View();
        }
    }
}