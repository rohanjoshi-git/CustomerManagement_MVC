using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerManagement.Models;

namespace CustomerManagement.ViewModel
{
    public class CustomerViewModel
    {
        // Customer
        public Customer customer { get; set; }

        // List of Customers
        public List<Customer> customers { get; set; }

    }
}