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
    public class CustomerController : Controller
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

            if(Request.QueryString["Type"] == "JSON")
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

        public ActionResult getCustomers() // JSON Collection
        {
            // fill the customers collections
            CustomerDal dal = new CustomerDal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            // Delay for Synchronous execution (10sec)
            Thread.Sleep(3000);

            return Json(customersColl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchCustomer()
        {
            // View Model object
            CustomerViewModel obj = new CustomerViewModel();

            // fill the customers collections
            CustomerDal dal = new CustomerDal();
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

        public ActionResult Submit() // validation runs
        {

            // manual binding of object with form elements
            Customer obj = new Customer();
            obj.CustomerName = Request.Form["customer.CustomerName"];
            obj.CustomerCode = Request.Form["customer.CustomerCode"];

            if (ModelState.IsValid)
            {
                // insert the Customer object to database
                // EF DAL
                CustomerDal Dal = new CustomerDal();
                Dal.Customers.Add(obj); // in memory
                Dal.SaveChanges(); // physical commit

                //return View("Customer", obj);
            }
            
            // fill the customers collections
            CustomerDal dal = new CustomerDal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            //return View("EnterCustomer",vm);    // removed as JSON will be sent 
            return Json(customersColl, JsonRequestBehavior.AllowGet);        
        }
    }
}