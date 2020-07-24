using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Post
    {
        
        [JsonProperty("PostId")]
        public long PostId { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("RatingTotal")]
        public int RatingTotal { get; set; }
        [JsonProperty("RatingNumber")]
        public int RatingNumber { get; set; }
    }
}