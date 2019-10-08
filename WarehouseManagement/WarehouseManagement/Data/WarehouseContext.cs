using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.WarehouseModels;

namespace WarehouseManagement.Models
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext (DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        public DbSet<WarehouseManagement.WarehouseModels.wItems> wItems { get; set; }
        public DbSet<WarehouseManagement.WarehouseModels.wCathegory> wCathegories { get; set; }
    }
}
