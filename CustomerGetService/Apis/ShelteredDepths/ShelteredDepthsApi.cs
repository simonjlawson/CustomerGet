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
            try
            {
                var url = "https://sheltered-depths-66346.herokuapp.com/customers";
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetCustomerAsync(Guid id)
        {
            try
            {
                var url = "https://sheltered-depths-66346.herokuapp.com/customer?id=" + id.ToString();
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }
    }
}
