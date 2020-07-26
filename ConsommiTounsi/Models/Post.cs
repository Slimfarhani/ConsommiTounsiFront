using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Post
    {

        [JsonProperty("postId")]
        public long PostId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("ratingTotal")]
        public int RatingTotal { get; set; }
        [JsonProperty("ratingNumber")]
        public int RatingNumber { get; set; }
        [JsonProperty("user")]
        public UserRegisterModel User { get; set; }
        [JsonProperty("urlImage")]
        public string imageUrl { get; set; }
    }
}