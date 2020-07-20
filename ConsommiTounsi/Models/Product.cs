using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    
    public class Product
    {
        [JsonProperty("productId")]
        public long productId { get; set; }
        [JsonProperty("weight")]
        public float weight { get; set; }
        [JsonProperty("productName")]
        public string productName { get; set; }
        [JsonProperty("category")]
        public string category { get; set; }
        [JsonProperty("barCode")]
        public long barCode { get; set; }
        [JsonProperty("imageUrl")]
        public string imageUrl { get; set; }

    }
}