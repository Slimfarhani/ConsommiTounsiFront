using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    
    public class Product
    {
        [JsonProperty("productId")]
        public long productId { get; set; }
        [JsonProperty("weight")]
        [DisplayName("Weight")]
        public float weight { get; set; }
        [JsonProperty("productName")]
        [DisplayName("Name")]
        public string productName { get; set; }
        [JsonProperty("category")]
        [DisplayName("Category")]
        public string category { get; set; }
        [JsonProperty("barCode")]
        [DisplayName("Bar Code")]
        public long barCode { get; set; }
        [JsonProperty("imageUrl")]
        [DisplayName("Image")]
        public string imageUrl { get; set; }
        [JsonProperty("mesure")]
        [DisplayName("Mesure")]
        public string mesure { get; set; }
        [JsonProperty("description")]
        [DisplayName("Description")]
        public string description { get; set; }

    }
}