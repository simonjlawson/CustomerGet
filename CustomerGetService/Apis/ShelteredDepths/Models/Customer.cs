using Newtonsoft.Json;

namespace CustomerGet.Service.Apis.ShelteredDepths.Models
{
    public class CustomerRecord
    {
        [JsonProperty("customer")]
        public Customer customer { get; set; }
    }

    public class Customer
    {
        [JsonProperty("id")]
        public string id;
        [JsonProperty("firstName")]
        public string FirstName;
        [JsonProperty("lastName")]
        public string LastName;
        [JsonProperty("gender")]
        public string Gender;
        [JsonProperty("title")]
        public string Title;
    }
}
