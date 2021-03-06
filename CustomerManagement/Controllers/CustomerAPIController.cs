﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CustomerManagement.Models;

namespace CustomerManagement.Controllers
{
    public class Error
    {
        public List<string> Errors { get; set; }
    }

    public class ClientData
    {
        public bool isValid { get; set; }
        public Object data { get; set; }
    }


    public class CustomerController : ApiController
    {
        ClientData objClientData = new ClientData();

        // Insert
        public /*List<Customer>*/ Object Post(Customer objCustomer)  // Object - to avoid typecasting error (Error to Customers)
        {
            if (ModelState.IsValid)
            {
                // insert the Customer object to database
                // EF DAL
                Dal.Dal Dal = new Dal.Dal();
                Dal.Customers.Add(objCustomer); // in memory
                Dal.SaveChanges(); // physical commit
            }
            else  // get all the errors in ModelState and put it in Error collection
            {
                var Err = new Error();
                Err.Errors = new List<string>(); // to avoid null reference in Err.Errors.Add
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Err.Errors.Add(error.ErrorMessage);
                    }
                }
                objClientData.isValid = false;
                objClientData.data = Err;
                return objClientData;
            }

            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = dal.Customers.ToList<Customer>();

            //return customersColl; // WebAPI will decide the content type on its own (HTML, JSON, JPEG...)
            objClientData.isValid = true;
            objClientData.data = customersColl;
            return objClientData;  // WebAPI will decide the content type on its own (HTML, JSON, JPEG...)
        }

        // Select
        public List<Customer> Get()
        {
            // Read the query string
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();

            string customerCode = allUrlKeyValues.SingleOrDefault(x => x.Key == "CustomerCode").Value;
            string customerName = allUrlKeyValues.SingleOrDefault(x => x.Key == "CustomerName").Value;

            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = new List<Customer>();

            if (customerName != null)
            {
                // Select the record using LINQ (used in Serch Customer Screen)
                // fill the customers collections
                customersColl = (from c in dal.Customers
                                                where c.CustomerName == customerName
                                 select c).ToList<Customer>();
            }

            else if (customerCode != null)
            {
                customersColl = (from c in dal.Customers
                                 where c.CustomerCode == customerCode
                                 select c).ToList<Customer>();
            }

            else
            {
                customersColl = dal.Customers.ToList<Customer>();
            }
            
            return customersColl;
        }

        /* // Start - commented as it will be handled in one Get method
        // Select (used in Serch Customer Screen
        public List<Customer> Get(string CustomerName)  // parameter name should match the query string parameters
        {
            // fill the customers collections
            Dal.Dal dal = new Dal.Dal();
            List<Customer> customersColl = (from c in dal.Customers
                                            where c.CustomerName == CustomerName
                                            select c).ToList<Customer>();
            return customersColl;
        }
        */ // End - commented as it will be handled in one Get method

        // Update
        public List<Customer> Put(Customer objCustomer)
        {
            // Select the record using LINQ
            Dal.Dal objDal = new Dal.Dal();
            Customer custUpdate = (from c in objDal.Customers
                                   where c.CustomerCode == objCustomer.CustomerCode
                                   select c).ToList<Customer>()[0];

            // Update the record
            custUpdate.CustomerName = objCustomer.CustomerName;
            custUpdate.CustomerAmount = objCustomer.CustomerAmount;

            List<Customer> customerColl = objDal.Customers.ToList<Customer>();
            return customerColl;
        }

        // Delete
        public List<Customer> Delete(Customer objCustomer)
        {
            // Select the record using LINQ
            Dal.Dal objDal = new Dal.Dal();
            Customer custDelete = (from c in objDal.Customers
                                   where c.CustomerCode == objCustomer.CustomerCode
                                   select c).ToList<Customer>()[0];

            objDal.Customers.Remove(custDelete);
            objDal.SaveChanges();

            List<Customer> customerColl = objDal.Customers.ToList<Customer>();
            return customerColl;
        }
    }
}
