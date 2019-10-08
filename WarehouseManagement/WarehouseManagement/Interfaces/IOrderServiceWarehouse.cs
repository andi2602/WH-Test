using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.WarehouseModels;

namespace WarehouseManagement.Interfaces
{
    public interface IOrderServiceWarehouse
    {

        void OrderItemsForWarehouse(int quantity);
        void OrderSpecificItem(int quantity, wItems item);
    }
}
