using AutoMapper;
using CustomerGet.Service.Apis;
using CustomerGet.Service.Apis.ShelteredDepths.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CustomerGet.Service.DataFactories
{
    /// <summary>
    /// Provides access to the Customer API and serialises into Customer objects
    /// </summary>
    public class ShelteredDepthsDataFactory : ICustomerDataFactory
    {
        public ICustomerServiceApi Api;

        public ShelteredDepthsDataFactory(ICustomerServiceApi api)
        {
            Api = api;
        }

        /// <summary>
        /// Get a single Customer record
        /// </summary>
        /// <param name="id">The GUID for the customer</param>
        /// <returns>The Customer record for the matching id, return an empty Customer record if unsuccessful</returns>
        public Common.Models.Customer GetCustomer(Guid id)
        {
            CustomerRecord apiCustomer = new CustomerRecord()
                { customer = new Customer { id = id.ToString() } };

            var apiResult = Task.Run(() => Api.GetCustomerAsync(id));

            //For the sake of example added a 5 second timeout for calling functions
            apiResult.Wait(5000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    var customerObj = JsonConvert.DeserializeObject<CustomerRecord>(apiResult.Result);
                    if (customerObj != null)
                        apiCustomer = customerObj;
                }
                catch
                {
                    //Failure will return a default object
                }
            }

            //Map Service object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customer = Mapper.Map<Common.Models.Customer>(apiCustomer.customer);

            return customer;
        }

        /// <summary>
        /// Gets the complete list of Customer records
        /// </summary>
        /// <returns></returns>
        public Common.Models.Customers GetCustomers()
        {
            Customers apiCustomers = new Customers()
                { ListOfCustomers = new System.Collections.Generic.List<Customer>() };

            var apiResult = Task.Run(() => Api.GetCustomersAsync());

            //For the sake of example added a 5 second timeout for calling functions
            apiResult.Wait(5000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    var customersObj = JsonConvert.DeserializeObject<Customers>(apiResult.Result);
                    if (customersObj != null)
                        apiCustomers = customersObj;
                }
                catch
                {
                    //Failure will return a default object
                }
            }

            //Map Service object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customers = Mapper.Map<Customers, Common.Models.Customers>(apiCustomers);

            return customers;
        }
    }
}
