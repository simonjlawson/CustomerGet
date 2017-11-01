using CustomerGet.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace CustomerGet.Business.Functions
{
    /// <summary>
    /// Accesses the backend API and converts the responses to Customer objects
    /// </summary>
    public class LiveCustomerDataFactory : ICustomerDataFactory
    {
        private ICustomerServiceApi Api;

        public LiveCustomerDataFactory(ICustomerServiceApi api)
        {
            Api = api;
        }

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <returns>A list of Customers</returns>
        public Customers GetAll()
        {
            Customers customers = new Customers() { ListOfCustomers = new List<Customer>() }; ;

            try
            {
                var task = Task.Run(() => Api.GetAllAsync());
                task.Wait(5000);

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

        /// <summary>
        /// Get a Customer record by GUID
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>A Customer record, returns a customer record with empty fields if not found</returns>
        public Customer Get(Guid id)
        {
            Customer customer = new Customer() { id = id.ToString() };

            try
            {
                var task = Task.Run(() => Api.GetAsync(id));
                task.Wait(5000);

                if (task.IsCompleted)
                {
                    if (task.Result != null || task.Result != string.Empty)
                    {
                        var customerObj = JsonConvert.DeserializeObject<Customer>(task.Result);
                        if (customerObj != null && customerObj.FirstName != string.Empty)
                            customer = customerObj;
                    }
                }
            }
            catch
            {
                //Failure returns a default object
            }

            return customer;
        }

        public bool Save(Customer customer)
        {

            try
            {
                var task = Task.Run(() => Api.PutAsync(customer));
                task.Wait(5000);

                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                //Log.Error(ex);
                //Failure returns a default object
            }

            return false;
        }

        }
}