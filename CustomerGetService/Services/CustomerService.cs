using CustomerGet.Service.DataFactories;
using Newtonsoft.Json;
using System;

namespace CustomerGet.Service.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerDataFactory DataFactory { get; }

        public CustomerService(ICustomerDataFactory dataFactory)
        {
            DataFactory = dataFactory;
        }

        public string GetCustomer(Guid id)
        {
            var customer = DataFactory.GetCustomer(id);
            var customerJson = JsonConvert.SerializeObject(customer);
            return customerJson;
        }

        public string GetCustomers()
        {
            var customers = DataFactory.GetCustomers();
            var customersJson = JsonConvert.SerializeObject(customers);
            return customersJson;
        }
    }
}
