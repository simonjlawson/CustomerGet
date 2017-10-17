using CustomerGet.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace CustomerGet.Business.Functions
{
    public class LiveCustomerDataFactory : ICustomerDataFactory
    {
        private ICustomerServiceApi Api;

        public LiveCustomerDataFactory(ICustomerServiceApi api)
        {
            Api = api;

        }
        public Customers GetAll()
        {
            Customers customers = new Customers() { ListOfCustomers = new List<Customer>() }; ;

            try
            {
                var task = Task.Run(() => Api.GetAllAsync());
                task.Wait(2000);

                if (task.IsCompleted)
                { 
                    if (task.Result != null && task.Result != string.Empty)
                        customers = JsonConvert.DeserializeObject<Customers>(task.Result);
                }
            }
            catch
            {
                //Failure returns a default object
            }

            return customers;
        }

        public Customer Get(Guid id)
        {
            Customer customer = new Customer() { id = id.ToString() };

            try
            {
                var task = Task.Run(() => Api.GetAsync(id));
                task.Wait(2000);

                if (task.IsCompleted)
                {
                    if (task.Result != null || task.Result != string.Empty)
                    {
                        var customerRecord = JsonConvert.DeserializeObject<CustomerRecord>(task.Result);
                        customer = customerRecord.customer;
                    }
                }
            }
            catch (Exception e)
            {
                //Failure returns a default object
            }

            return customer;
        }
    }
}