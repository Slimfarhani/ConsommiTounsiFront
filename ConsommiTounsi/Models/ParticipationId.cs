using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class ParticipationId
    {
        [JsonProperty("eventId")]
        public long EventId { get; set; }
        [JsonProperty("customerId")]
        public long CustomerId { get; set; }
    }
}