using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.WholesaleModels
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        //public int Quantity { get; set; } = UInt64.MaxValue;

        //[ForeignKey("Cathegory")]

        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }



        public Cathegory cathegory { get; set; }
    }
}
