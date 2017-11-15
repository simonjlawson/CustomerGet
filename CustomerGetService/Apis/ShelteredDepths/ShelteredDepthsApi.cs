using CustomerGet.Common.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerGet.Service.Apis
{
    /// <summary>
    /// Reads the Customer API
    /// </summary>
    public class ShelteredDepthsApi : ICustomerServiceApi
    {
        public async Task<string> GetCustomersAsync()
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customers";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCustomerAsync(Guid id)
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customer?id=" + id.ToString();
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutCustomersAsync(Guid id, string firstname)
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customers";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
