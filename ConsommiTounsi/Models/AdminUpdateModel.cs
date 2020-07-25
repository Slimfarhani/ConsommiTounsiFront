using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class AdminUpdateModel
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
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string confirmPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("OldPasswordVerif", ErrorMessage = "Wrong password, Type again !")]
        public string OldPassword { get; set; }
        public string OldPasswordVerif { get; set; }
    }
}