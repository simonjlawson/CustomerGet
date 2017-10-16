using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerGet.Common.Models
{
    public class Customers
    {
        [JsonProperty("customers")]
        public IEnumerable<Customer> ListOfCustomers { get; set; }
    }
}
