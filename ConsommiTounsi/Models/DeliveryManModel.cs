using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ConsommiTounsi.Models
{
    public class DeliveryManModel
    {
        [JsonProperty("delivery_ManId")]
        public long Delivery_ManId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("numTel")]
        public string NumTel { get; set; }
        [JsonProperty("deliveryZone")]
        public string DeliveryZone { get; set; }
    }
}