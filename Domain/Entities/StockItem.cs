using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StockItem
    {
        public long StockItemId { get; set; }
        public long ProductId { get; set; }
        public long UserID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
