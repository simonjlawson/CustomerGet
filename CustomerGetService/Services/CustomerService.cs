using CustomerGet.Service.DataFactories;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CustomerGet.Service.Services
{
    /// <summary>
    /// Accesses DataFactories and returns their output as JSon strings
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private int PageSize = 100;

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
        /// <param name="page">Page number of the Results, starts at 0</param>
        /// <returns>A JSon string</returns>
        public string GetCustomers(int page = 0)
        {
            var customers = DataFactory.GetCustomers();
            var totalCustomers = customers.ListOfCustomers.Count();

            //Cap the range of possible page numbers
            var maxPage = (totalCustomers / PageSize);
            if (page < 0)
                page = 0;
            if (page > maxPage)
                page = maxPage;
            
            //Paginate the results
            customers.ListOfCustomers = customers.ListOfCustomers.Skip(page * PageSize).Take(PageSize);

            var customersJson = JsonConvert.SerializeObject(customers);
            return customersJson;
        }

        public string PutCustomer(Guid id, string firstname)
        {
            var customer = DataFactory.PutCustomer(id, firstname);
            var customerJson = JsonConvert.SerializeObject(customer);
            return customerJson;
        }
    }
}
