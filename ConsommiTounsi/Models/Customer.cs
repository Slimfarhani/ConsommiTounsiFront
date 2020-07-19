using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Customer
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
        [JsonProperty("userName")]
        public string userName { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("birthday")]
        public string birthday { get; set; }
        [JsonProperty("mail")]
        public string mail { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("urlImage")]
        public string urlImage { get; set; }
        [JsonProperty("gender")]
        public string gender { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
    }
}