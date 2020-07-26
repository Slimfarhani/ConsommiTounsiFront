using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Participation
    {
        [JsonProperty("participationId")]
        public ParticipationId Id { get; set; }
       
        [JsonProperty("donationAmount")]
        public long Donation { get; set; }
    }
}