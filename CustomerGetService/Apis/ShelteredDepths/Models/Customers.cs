using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerGet.Service.Apis.ShelteredDepths.Models
{
    public class Customers
    {
        [JsonProperty("customers")]
        public List<Customer> ListOfCustomers { get; set; }
    }
}
