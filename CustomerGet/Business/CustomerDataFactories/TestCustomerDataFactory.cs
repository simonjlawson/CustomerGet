using CustomerGet.Common.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Web.Hosting;

namespace CustomerGet.Business.Functions
{
    /// <summary>
    /// Fakes API responses from interal JSon files
    /// </summary>
    public class TestCustomerDataFactory : ICustomerDataFactory
    {
        /// <summary>
        /// Returns all Customer records from an internal JSon file
        /// </summary>
        /// <returns>Static Customer records</returns>
        public Customers GetAll()
        {
            var fileJson = File.ReadAllText(HostingEnvironment.MapPath("~/Business/TestJson/customers.json"));
            var customers = JsonConvert.DeserializeObject<Customers>(fileJson);
            return customers;
        }

        /// <summary>
        /// Returns a static customer record regardless of ID given
        /// </summary>
        /// <param name="id">Not required</param>
        /// <returns>Static Customer record</returns>
        public Customer Get(Guid id)
        {
            var fileJson = File.ReadAllText(HostingEnvironment.MapPath("~/Business/TestJson/customer.json"));
            var customer = JsonConvert.DeserializeObject<Customer>(fileJson);
            return customer;
        }
    }
}