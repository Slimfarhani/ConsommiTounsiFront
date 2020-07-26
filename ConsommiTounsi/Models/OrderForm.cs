using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class OrderForm
    {
        public List<OrderItem> items { get; set; }
        public string Zone;

    }
}