using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Order
    {
        [JsonProperty("total")]
        public float total { get; set; }
        [JsonProperty("orderId")]
        public int orderId { get; set; }
        [JsonProperty("itemNumber")]
        public int itemNumber { get; set; }
        [JsonProperty("isPaid")]
        public Boolean isPaid { get; set; }
        [JsonProperty("deliveryMethod")]
        public string deliveryMethod { get; set; }
        [JsonProperty("customer")]
        public Customer customer { get; set; }
        [JsonProperty("details")]
        public List<Order_Detail> details { get; set; }
        [JsonProperty("states")]
        public List<Order_State> states { get; set; }

    }
}