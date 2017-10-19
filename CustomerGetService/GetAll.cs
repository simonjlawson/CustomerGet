using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using CustomerGet.Service.DataFactories;
using CustomerGet.Service.Services;
using CustomerGet.Service.Apis;
using System.Net.Http.Headers;
using System.Linq;

namespace CustomerGet.Service
{
    public static class GetAll
    {
        [FunctionName("GetAll")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            string resultJson = null;

            try
            {
                //Parse page parameter and default to 0
                string pageString = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => string.Compare(q.Key, "page", true) == 0)
                    .Value;
                int page;
                int.TryParse(pageString, out page);

                //TODO: Implement IOC
                var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
                var customerService = new CustomerService(customerDataFactory);
                resultJson = customerService.GetCustomers(page);
            }
            catch (Exception e)
            {
                log.Error("GetAll failed",e);
                return req.CreateResponse(HttpStatusCode.BadRequest, $"An error has occured {e.Message}");
            }

            var jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            return resultJson == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "No data found")
                : req.CreateResponse(HttpStatusCode.OK, resultJson, jsonFormatter, new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
