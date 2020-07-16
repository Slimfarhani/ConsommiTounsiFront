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
        [Required]
        public string userName { get; set; }
        [JsonProperty("password")]
        [Required]
        public string password { get; set; }
    }
}