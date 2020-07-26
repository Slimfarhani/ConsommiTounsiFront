using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class UserRegisterModel
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
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("name")]
        public string companyName { get; set; }

        public string birthdateString { get; set; }
        [JsonProperty("birthdate")]
        public DateTime birthdateFormatted { get; set; }
        [JsonProperty("mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("urlImage")]
        public string urlImage { get; set; }
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string confirmPassword { get; set; }
        [JsonProperty("blocked")]
        public String blockdate { get; set; }

    }
}