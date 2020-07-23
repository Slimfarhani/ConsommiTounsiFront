using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class Supplier
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
    }
}