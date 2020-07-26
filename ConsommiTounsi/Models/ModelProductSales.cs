using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class ModelProductSales
    {

        [JsonProperty("tQuantity")]
        public long tQuantity { get; set; }
        [JsonProperty("productName")]
        public string productName { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("price")]
        public float price { get; set; }
        
        
    }
}