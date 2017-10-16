using CustomerGet.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerGet.Business.Functions
{
    public static class GetAllCustomers
    {
        public static IEnumerable<Customer> Get()
        {
            var task = Task.Run(() => GetAllAsync());
            var result = task.Result;

            return result;
        }

        private static async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var apiURL = "http://CustomerGet.Service20171015.azurewebsites.net/api/GetAll";
            var client = new HttpClient();
            var response = await client.GetAsync(apiURL);
            var content = await response.Content.ReadAsStringAsync();

            var customers = JsonConvert.Deserialize<Customers>(content);

            return customers.ListOfCustomers;
        }
    }
}