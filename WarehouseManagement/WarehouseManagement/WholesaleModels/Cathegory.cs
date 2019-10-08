using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.WholesaleModels
{
    public class Cathegory
    {
        [Key]
        public int CathegoryId { get; set; }
        public string Name { get; set; }
        
        public ICollection<Items> items { get; set; }
    }
}
