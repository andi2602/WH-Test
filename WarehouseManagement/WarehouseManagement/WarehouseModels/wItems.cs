using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.WarehouseModels
{
    public class wItems
    {
        [Key]
        public int wItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("wCathegory")]
        public wCathegory cathegory { get; set; }

    }
}
