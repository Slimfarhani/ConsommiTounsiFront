using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Post_React
    {
         
        [JsonProperty("Post_ReactId")]
        public long Post_ReactId { get; set; }
        [JsonProperty("Reaction")]
        public string Reaction { get; set; }
    }

}