using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Repository.Entities
{
    public class ShopBridgeDBContext : DbContext
    {
        public ShopBridgeDBContext() : base("ShopBridge") 
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
    }
}
