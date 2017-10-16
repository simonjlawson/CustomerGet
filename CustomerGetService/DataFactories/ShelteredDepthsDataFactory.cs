using AutoMapper;
using CustomerGet.Service.Apis;
using CustomerGet.Service.Apis.ShelteredDepths.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CustomerGet.Service.DataFactories
{
    public class ShelteredDepthsDataFactory : ICustomerDataFactory
    {
        public ICustomerServiceApi Api;

        public ShelteredDepthsDataFactory(ICustomerServiceApi api)
        {
            Api = api;
        }

        public Common.Models.Customer GetCustomer(Guid id)
        {
            CustomerRecord apiCustomer = new CustomerRecord();

            var apiResult = Task.Run(() => Api.GetCustomerAsync(id));

            //For the sake of example added a 2 second timeout for calling functions
            apiResult.Wait(100000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    apiCustomer = JsonConvert.DeserializeObject<CustomerRecord>(apiResult.Result);
                }
                catch (Exception e)
                {
                    apiCustomer.customer.id = id.ToString();
                }
            }

            //Map Feed object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customer = Mapper.Map<Common.Models.Customer>(apiCustomer.customer);

            return customer;
        }

        public Common.Models.Customers GetCustomers()
        {
            Customers apiCustomers = new Customers()
                { ListOfCustomers = new System.Collections.Generic.List<Customer>() };
            
            var apiResult = Task.Run(() => Api.GetCustomersAsync());

            //Not ideal but for the sake of example added a 2 second timeout for calling functions
            apiResult.Wait(100000);

            if (apiResult.IsCompleted)
            {
                var apiResultJson = apiResult.Result;
                apiCustomers = JsonConvert.DeserializeObject<Customers>(apiResultJson);
            }

            //Map Feed object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customers = Mapper.Map<Customers, Common.Models.Customers>(apiCustomers);

            return customers;
        }
    }
}
