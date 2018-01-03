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
        public ActionResult Submit(Customer obj) // validation runs
        {
            if(ModelState.IsValid)
            {
                // insert the Customer object to database
                // EF DAL
                CustomerDal Dal = new CustomerDal();
                Dal.Customers.Add(obj); // in memory
                Dal.SaveChanges(); // physical commit

                //return View("Customer", obj);
            }
            
                return View("EnterCustomer",obj);            
        }
    }
}