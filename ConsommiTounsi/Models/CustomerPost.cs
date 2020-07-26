using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class CustomerPost
    {
        public Post post { get; set; }
        public Customer customer { get; set; }
    }
}