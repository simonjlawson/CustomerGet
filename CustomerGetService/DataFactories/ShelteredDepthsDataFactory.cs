using AutoMapper;
using CustomerGet.Service.Apis;
using CustomerGet.Service.Apis.ShelteredDepths.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace CustomerGet.Service.DataFactories
{
    /// <summary>
    /// Provides access to the Customer API and serialises into Customer objects
    /// </summary>
    public class ShelteredDepthsDataFactory : ICustomerDataFactory
    {
        private string CacheKeyCustomers => "cacheKeyCustomers";

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

            //Cache query
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = cache.Get(CacheKeyCustomers);
            if (cachedObject is Customers)
                return (Common.Models.Customers)cachedObject;

            try
            {
                var apiResult = Task.Run(() => Api.GetCustomersAsync());

                //For the sake of example added a 5 second timeout for calling functions
                apiResult.Wait(5000);
                if (apiResult.IsCompleted)
                {
                    var customersObj = JsonConvert.DeserializeObject<Customers>(apiResult.Result);
                    if (customersObj != null)
                        apiCustomers = customersObj;
                }
            }
            catch
            {
                //Failure will return a default object
            }

            //Map Service object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customers = Mapper.Map<Customers, Common.Models.Customers>(apiCustomers);
            
            //Add to memory cache for 1 minute
            var cachePolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1)) };
            cachedObject = cache.Add(CacheKeyCustomers, customers, cachePolicy);

            return customers;
        }

        public bool PutCustomer(Guid id, string firstname)
        {
            CustomerRecord apiCustomer = new CustomerRecord()
            { customer = new Customer { id = id.ToString() } };

            var apiResult = Task.Run(() => Api.PutCustomersAsync(id, firstname));

            //For the sake of example added a 5 second timeout for calling functions
            apiResult.Wait(5000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    var customerObj = JsonConvert.DeserializeObject<string>(apiResult.Result);
                    //We do not know the response so if not empty assume success
                    return (!string.IsNullOrEmpty(customerObj));
                }
                catch
                {
                    return false;
                }
            }
            
            return false;
        }
    }
}
