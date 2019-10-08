using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.StorefrontModels;

namespace WarehouseManagement.Interfaces
{
    public interface IOrderServiceStore
    {
         void fillWholeStore(int quantity);
        void orderSpecificItem(int quantity, storeItems storeItem);
    }
}
