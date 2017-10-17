using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Configuration;

namespace CustomerGet.Business.Functions
{
    public class CustomerServiceApi : ICustomerServiceApi
    {
        public async Task<string> GetAllAsync()
        {
            var apiURL = ConfigurationManager.AppSettings["CustomerGetServiceUrl"];
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(apiURL + "GetAll");
            var x = await response.Content.ReadAsStringAsync();
            return x;
        }

        public async Task<string> GetAsync(Guid id)
        {
            var apiURL = ConfigurationManager.AppSettings["CustomerGetServiceUrl"];
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(apiURL + "GetCustomer?id=" + id);
            var x = await response.Content.ReadAsStringAsync();
            return x;
        }
    }
}