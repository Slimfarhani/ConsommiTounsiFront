using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=consommitounsichaine")
        {

        }
        // dbset
        //public DbSet<Entity> Entities { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ajouter les config + conv
            //modelBuilder.Conventions.Add(new ExpConvention());
        }
    }
}
