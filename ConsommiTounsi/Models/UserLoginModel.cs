using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class UserLoginModel
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
        [JsonProperty("userName")]
        public string userName { get; set; }
        [JsonProperty("password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [JsonProperty("role")]
        public string role { get; set; }

    }
}