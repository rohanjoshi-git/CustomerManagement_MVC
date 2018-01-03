using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CustomerManagement.Models;

namespace CustomerManagement.Dal
{
    public class CustomerDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("tblCustomer");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}