using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Comment_React
    {
        [JsonProperty("Comment_ReactId")]
        public long Comment_ReactId { get; set; }
        [JsonProperty("Reaction")]
        public string Reaction { get; set; }
    }
}