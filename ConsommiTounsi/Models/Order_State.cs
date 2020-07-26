using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Order_State
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
        [JsonProperty("order_StateId")]
        public long order_StateId { get; set; }
    }
}