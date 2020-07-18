using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
        [JsonProperty("userName")]
        public string userName { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string birthday { get; set; }

        public string email { get; set; }
        public string phone { get; set; }

    }
}