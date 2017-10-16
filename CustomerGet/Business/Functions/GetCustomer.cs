using CustomerGet.Common.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerGet.Business.Functions
{
    public static class GetCustomer
    {
        public static Customer Get(Guid id)
        {
            var task = Task.Run(() => GetAsync(id));
            var result = task.Result;

            return result;
        }

        private static async Task<Customer> GetAsync(Guid id)
        {
            var apiURL = "http://CustomerGet.Service20171015.azurewebsites.net/api/GetCustomer?id=" + id;
            var client = new HttpClient();
            var response = await client.GetAsync(apiURL);
            var content = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.Deserialize<Customer>(content);

            return customer;
        }
    }
}