using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Order_Detail
    {
        [JsonProperty("supplier")]
        public Supplier supplier { get; set; }
        [JsonProperty("product")]
        public Product product { get; set; }
        [JsonProperty("order_DetailId")]
        public long order_DetailId { get; set; }
        [JsonProperty("price")]
        public float price { get; set; }
        [JsonProperty("quantity")]
        public int quantity { get; set; }
    }
}