using Newtonsoft.Json;
using System;

namespace CustomerGet.Common.Models
{
    ///// <summary>
    ///// Root Customer Record to help Serialising Customer JSon
    ///// </summary>
    //public class CustomerRecord
    //{
    //    [JsonProperty("customer")]
    //    public Customer customer { get; set; }
    //}

    /// <summary>
    /// Standard Customer Model used for CustomerGet.Service
    /// </summary>
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
