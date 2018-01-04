using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Models;
using CustomerManagement.Dal;
using CustomerManagement.ViewModel;

namespace CustomerManagement.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Load()
        {
            Customer obj = 
                new Customer 
                    { 
                        CustomerCode = "1001",
                        CustomerName = "Shiv" 
                    };

            return View("Customer",obj);
        }
        public ActionResult Enter()
        {
            //return View("EnterCustomer", new Customer());

            // View Model object
            CustomerViewModel obj = new CustomerViewModel();
            // single object is fresh
            obj.customer = new Customer(); // initially empty
            // fill the customers collections
            CustomerDal dal = new CustomerDal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();
            obj.customers = customersColl;

            return View("EnterCustomer", obj);
        }

        public ActionResult EnterSearch()
        {
            // View Model object
            CustomerViewModel obj = new CustomerViewModel();
            obj.customers = new List<Customer>();
            return View("SearchCustomer", obj);
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

            CustomerViewModel vm = new CustomerViewModel();

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
                vm.customer = new Customer(); // to refresh the view with new emply object

                //return View("Customer", obj);
            }
            else
            {
                vm.customer = obj;  // if the object is not valid, persist the object and show it to user
            }
            // fill the customers collections
            CustomerDal dal = new CustomerDal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();
            vm.customers = customersColl;

            return View("EnterCustomer",vm);            
        }
    }
}