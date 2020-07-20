using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    
    public class Stock
    {
        [JsonProperty("quantity")]
        public long quantity { get; set; }
        [JsonProperty("mesure")]
        public string mesure { get; set; }
        [JsonProperty("price")]
        public float price { get; set; }
        [JsonProperty("product")]
        public Product product { get; set; }
    }
}