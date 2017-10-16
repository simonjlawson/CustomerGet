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
            apiCustomer.customer = new Customer { id = id.ToString() };

            var apiResult = Task.Run(() => Api.GetCustomerAsync(id));

            //For the sake of example added a 2 second timeout for calling functions
            apiResult.Wait(2000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    var customerObj = JsonConvert.DeserializeObject<CustomerRecord>(apiResult.Result);
                    if (customerObj != null)
                        apiCustomer = customerObj;
                }
                catch (Exception e)
                {
                    //Leave apiCustomers in it's default state
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

            //For the sake of example added a 2 second timeout for calling functions
            apiResult.Wait(2000);

            if (apiResult.IsCompleted)
            {
                try
                {
                    var customersObj = JsonConvert.DeserializeObject<Customers>(apiResult.Result);
                    if (customersObj != null)
                        apiCustomers = customersObj;
                }
                catch (Exception e)
                {
                    //Leave apiCustomers in it's default state
                }
            }

            //Map Feed object to Common object
            Mapper.Initialize(x => { x.AddProfile<CustomerMappingProfile>(); });
            var customers = Mapper.Map<Customers, Common.Models.Customers>(apiCustomers);

            return customers;
        }
    }
}
