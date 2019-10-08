using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.WarehouseModels
{
    public class wCathegory
    {
        [Key]
        public int wCathegoryId { get; set; }
        public string Name { get; set; }


        public ICollection<wItems> items { get; set; }

    }
}
