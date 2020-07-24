using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Comment
    {
        [JsonProperty("CommentId")]
        public long CommentId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
    }
        
}