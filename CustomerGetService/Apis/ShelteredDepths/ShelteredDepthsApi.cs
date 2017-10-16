using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerGet.Service.Apis
{
    public class ShelteredDepthsApi : ICustomerServiceApi
    {
        public async Task<string> GetCustomersAsync()
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customers";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCustomerAsync(Guid id)
        {
            var url = "https://sheltered-depths-66346.herokuapp.com/customer?id=" + id.ToString();
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
