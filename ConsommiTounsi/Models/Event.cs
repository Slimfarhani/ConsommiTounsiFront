using ConsommiTounsi.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Event
    {
        [JsonProperty("supplier")]
        public Supplier Supplier { get; set; }
        [JsonProperty("eventId")]
        public long EventId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("location")]
        public Governorates Location { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("urlImage")]
        public string UrlImage { get; set; }
        [JsonProperty("state")]
        public int state { get; set; }

        public string StartDateString { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDatedateFormatted { get; set; }


        public string EndDateString { get; set; }
        [JsonProperty("endDate")]
        public DateTime EndDatedateFormatted { get; set; }

    }
}