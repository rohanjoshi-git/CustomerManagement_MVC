using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CustomerManagement.Models;

namespace CustomerManagement.Controllers
{
    public class CustomerController : ApiController
    {
        public List<Customer> Post(Customer objCustomer)
        {
            if (ModelState.IsValid)
            {
                // insert the Customer object to database
                // EF DAL
                Dal.Dal Dal = new Dal.Dal();
                Dal.Customers.Add(objCustomer); // in memory
                Dal.SaveChanges(); // physical commit
            }

            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            return customersColl; // WebAOI will decide the content type on its own (HTML, JSON, JPEG...)
        }
  
        public List<Customer> Get()
        {
            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();
            return customersColl;
        }

        public List<Customer> Get(string CustomerName)  // parameter name should match the query string parameters
        {
            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = (from c in dal.Customers
                                            where c.CustomerName == CustomerName
                                            select c).ToList<Customer>();
            return customersColl;
        }
    }
}
