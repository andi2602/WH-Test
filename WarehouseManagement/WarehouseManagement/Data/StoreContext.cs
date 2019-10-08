using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.StorefrontModels;

namespace WarehouseManagement.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext (DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseManagement.StorefrontModels.storeItems> storeItems { get; set; }
        public DbSet<WarehouseManagement.StorefrontModels.storeCathegories> storeCathegories { get; set; }
    }
}
