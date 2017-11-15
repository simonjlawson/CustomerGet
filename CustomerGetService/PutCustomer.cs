using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using CustomerGet.Service.Services;
using CustomerGet.Service.DataFactories;
using CustomerGet.Service.Apis;
using System.Net.Http.Headers;

namespace CustomerGet.Service
{
    public static class PutCusomter
    {
        [FunctionName("PutCustomer")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            //Parse query parameter
            string id = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
                .Value;


            string firstname = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "firstname", true) == 0)
                .Value;

            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
                return req.CreateResponse(HttpStatusCode.BadRequest, "Id is not a valid");

            bool result = false;
            try
            {
                //TODO: Implement IOC
                var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
                var customerService = new CustomerService(customerDataFactory);
                result = customerService.PutCustomer(idGuid, firstname);
            }
            catch (Exception e)
            {
                log.Error("GetCustomer failed", e);
                return req.CreateResponse(HttpStatusCode.BadRequest, $"An error has occured: {e}");
            }

            var jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            return req.CreateResponse(HttpStatusCode.OK, result, jsonFormatter, new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
