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

namespace CustomerGet.Service
{
    public static class GetCustomer
    {
        [FunctionName("GetCustomer")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // parse query parameter
            string id = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
                .Value;

            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
                return req.CreateResponse(HttpStatusCode.BadRequest, "Id is not a valid");

            string resultJson = null;
            try
            {
                var customerDataFactory = new ShelteredDepthsDataFactory(new ShelteredDepthsApi());
                var customerService = new CustomerService(customerDataFactory);
                resultJson = customerService.GetCustomer(idGuid);
            }
            catch (Exception e)
            {
                log.Error("GetCustomer failed", e);
                return req.CreateResponse(HttpStatusCode.BadRequest, $"An error has occured: {e}");
            }

            return resultJson == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Empty Data")
                : req.CreateResponse(HttpStatusCode.OK, resultJson);
        }
    }
}
