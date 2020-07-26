using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Ad
    {
        [JsonProperty("adId")]
        public long adId { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }

        
        //public String startDateString { get; set; }

        [JsonProperty("startDate")]
        public DateTime startDateFormatted { get; set; }
        
        //public String endDateString { get; set; }
        [JsonProperty("endDate")]
        public DateTime endDateFormatted { get; set; }

        [JsonProperty("estimatedViewNumber")]
        public int estimatedViewNumber { get; set; }

        [JsonProperty("actualViewNumber")]
        public int actualViewNumber { get; set; }

        [JsonProperty("cost")]
        public float cost { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("isValid")]
        public bool isValid { get; set; }

        [JsonProperty("supplier")]
        public Supplier supplier { get; set; }
    }
}