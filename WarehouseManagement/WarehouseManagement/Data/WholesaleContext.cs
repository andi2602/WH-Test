using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.WholesaleModels;

namespace WarehouseManagement.Models
{
    public class WholesaleContext : DbContext
    {
        public WholesaleContext (DbContextOptions<WholesaleContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseManagement.WholesaleModels.Items> Items { get; set; }
        public DbSet<WarehouseManagement.WholesaleModels.Cathegory> Cathegories { get; set; }
    }
}
