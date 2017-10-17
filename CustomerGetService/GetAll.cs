using System.Linq;
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
                var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
                var customerService = new CustomerService(customerDataFactory);
                resultJson = customerService.GetCustomers();
            }
            catch (Exception e)
            {
                log.Error("GetAll failed",e);
                return req.CreateResponse(HttpStatusCode.BadRequest, $"An error has occured {e.Message}");
            }

            return resultJson == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "No data found")
                : req.CreateResponse(HttpStatusCode.OK, resultJson);
        }
    }
}
