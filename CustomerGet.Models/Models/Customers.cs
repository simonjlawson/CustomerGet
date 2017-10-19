using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerGet.Common.Models
{
    /// <summary>
    /// Root Customer Collection model
    /// </summary>
    public class Customers
    {
        [JsonProperty("customers")]
        public IEnumerable<Customer> ListOfCustomers { get; set; }
    }
}
