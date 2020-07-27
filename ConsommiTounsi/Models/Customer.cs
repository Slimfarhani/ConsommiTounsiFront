using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Customer
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
       
        [JsonProperty("userName")]
        [Required]
        public string userName { get; set; }
        
        [JsonProperty("password")]
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        
        [JsonProperty("role")]
        public string role { get; set; }
        [Required]
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [Required]
        [JsonProperty("lastName")]
        public string lastName { get; set; }

        public string birthdateString { get; set; }
        [Required(ErrorMessage = "birthdate. is required")]
        [JsonProperty("birthdate")]
        public DateTime birthdateFormatted { get; set; }
        [JsonProperty("mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("((\\+|00)216)?(7|2|5|9|4)[0-9]{7}", ErrorMessage = "Please enter valid phone no.")]

        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("address")]

        public string address { get; set; }
        [JsonProperty("gender")]
        [Required]
        public string gender { get; set; }

        [JsonProperty("urlImage")]
        
        public string urlImage { get; set; }

        public string OldUserName{ get; set; }
    }
}