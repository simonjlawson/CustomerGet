using Newtonsoft.Json;
using System;

namespace CustomerGet.Common.Models
{
    public class Customer
    {
        public string id;
        [JsonProperty("firstname")]
        public string FirstName;
        [JsonProperty("lastname")]
        public string LastName;
        [JsonProperty("gender")]
        public string Gender;
        [JsonProperty("title")]
        public string Title;
    }
}
