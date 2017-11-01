using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CustomerGet.Common.Models;

namespace CustomerGet.Business.Functions
{
    /// <summary>
    /// Accesses the Site's backend Customer API
    /// </summary>
    public class CustomerServiceApi : ICustomerServiceApi
    {
        /// <summary>
        /// Get all Customer Records
        /// </summary>
        /// <returns>All Customers in JSon form</returns>
        public async Task<string> GetAllAsync()
        {
            var apiURL = ConfigurationManager.AppSettings["CustomerGetServiceUrl"];
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(apiURL + "GetAll");
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(responseContent).ToString();
        }

        /// <summary>
        /// Get a Customer Record by GUID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>A Customer in json form</returns>
        public async Task<string> GetAsync(Guid id)
        {
            var apiURL = ConfigurationManager.AppSettings["CustomerGetServiceUrl"];
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(apiURL + "GetCustomer?id=" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(responseContent).ToString();
        }

        public async Task<bool> PutAsync(Customer customer)
        {
            var apiURL = ConfigurationManager.AppSettings["CustomerGetServiceUrl"];
            var client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.PutAsync(apiURL + "PutCustomer?id=" + customer.id + "&firstname=" + customer.FirstName);
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent == "true";
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}