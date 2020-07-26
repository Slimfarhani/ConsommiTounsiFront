using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class AdViews
    {
        [JsonProperty("adViewId")]
        public long adViewId { get; set; }
        [JsonProperty("adId")]
        public long adId { get; set; }
        [JsonProperty("userId")]
        public long userId { get; set; }
    }
}