using CustomerGet.Common.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Web.Hosting;

namespace CustomerGet.Business.Functions
{
    public class TestCustomerDataFactory : ICustomerDataFactory
    {
        public Customers GetAll()
        {
            var fileJson = File.ReadAllText(HostingEnvironment.MapPath("~/Business/TestJson/customers.json"));
            var customers = JsonConvert.DeserializeObject<Customers>(fileJson);
            return customers;
        }

        public Customer Get(Guid id)
        {
            var fileJson = File.ReadAllText(HostingEnvironment.MapPath("~/Business/TestJson/customer.json"));
            var customers = JsonConvert.DeserializeObject<CustomerRecord>(fileJson);
            return customers.customer;
        }
    }
}