using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.StorefrontModels
{
    public class storeItems
    {
        [Key]
        public int storeItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("storeCathegories")]
        public storeCathegories cathegories { get; set; }
    }
}
