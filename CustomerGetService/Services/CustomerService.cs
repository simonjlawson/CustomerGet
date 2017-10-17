using CustomerGet.Service.DataFactories;
using Newtonsoft.Json;
using System;

namespace CustomerGet.Service.Services
{
    /// <summary>
    /// Accesses DataFactories and returns their output as JSon strings
    /// </summary>
    public class CustomerService : ICustomerService
    {
        public ICustomerDataFactory DataFactory { get; }

        public CustomerService(ICustomerDataFactory dataFactory)
        {
            DataFactory = dataFactory;
        }

        /// <summary>
        /// Get a Customer record by GUID and serialise to JSon
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>A JSon string</returns>
        public string GetCustomer(Guid id)
        {
            var customer = DataFactory.GetCustomer(id);
            var customerJson = JsonConvert.SerializeObject(customer);
            return customerJson;
        }

        /// <summary>
        /// Get all Customer records serialise to JSon
        /// </summary>
        /// <returns>A JSon string</returns>
        public string GetCustomers()
        {
            var customers = DataFactory.GetCustomers();
            var customersJson = JsonConvert.SerializeObject(customers);
            return customersJson;
        }
    }
}
