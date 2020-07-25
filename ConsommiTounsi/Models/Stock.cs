using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    
    public class Stock
    {
        [JsonProperty("quantity")]

        [DisplayName("Quantity")]
        public long? quantity { get; set; }
        [JsonProperty("price")]
        [DisplayName("Price")]
        public float? price { get; set; }
        [JsonProperty("product")]
        public Product product { get; set; }
        [JsonProperty("supplier")]
        public Supplier supplier { get; set; }
    }
}