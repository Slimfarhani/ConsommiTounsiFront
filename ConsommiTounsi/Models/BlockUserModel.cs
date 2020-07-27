using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class BlockUserModel
    {
        [JsonProperty("userId")]
        public long userId { get; set; }
        [JsonProperty("blocked")]
        public DateTime Blockdate { get; set; }

    }
}