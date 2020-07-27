using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Ad
    {
        [JsonProperty("adId")]
        public long adId { get; set; }
        [JsonProperty("title")]
        [DisplayName("Title")]
        public string title { get; set; }


        //public String startDateString { get; set; }

        [JsonProperty("startDate")]
        [DisplayName("Start Date")]
        public DateTime startDateFormatted { get; set; }

        //public String endDateString { get; set; }
        [JsonProperty("End Date")]
        [DisplayName("Estimated View Number")]
        public DateTime endDateFormatted { get; set; }

        [JsonProperty("estimatedViewNumber")]
        [DisplayName("Estimated View Number")]
        public int estimatedViewNumber { get; set; }

        [JsonProperty("actualViewNumber")]
        [DisplayName("Actual View Number")]
        public int actualViewNumber { get; set; }

        [JsonProperty("cost")]
        [DisplayName("Cost")]
        public float cost { get; set; }
        [JsonProperty("type")]
        [DisplayName("Type")]
        public string type { get; set; }
        [JsonProperty("isValid")]
        [DisplayName("Is Valid")]
        public bool isValid { get; set; }

        [JsonProperty("imageUrl")]
        [DisplayName("Image")]
        public string imageUrl { get; set; }
        [JsonProperty("supplier")]
        [DisplayName("Supplier")]
        public Supplier supplier { get; set; }
    }
}