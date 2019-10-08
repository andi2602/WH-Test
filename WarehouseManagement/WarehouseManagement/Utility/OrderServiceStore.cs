using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.StorefrontModels;
using WarehouseManagement.WarehouseModels;

namespace WarehouseManagement.Utility
{
    public class OrderServiceStore : IOrderServiceStore
    {
        private StoreContext storeContext;
        private WarehouseContext warehouseContext;

        public OrderServiceStore(StoreContext store, WarehouseContext warehouse)
        {
            storeContext = store;
            warehouseContext = warehouse;
        }

        public void fillWholeStore(int quantity)
        {
           
            using (warehouseContext)
            {

                foreach (var item in warehouseContext.wItems.ToList())
                {
                    storeItems storeItem = new storeItems
                    {
                        Name = " ",
                        Quantity = 0,
                        Manufacturer = " ",
                        Description = " ",
                        Price = 0
                        
                    };

                    storeItem.Name = item.Name;
                    storeItem.Quantity += quantity;
                    storeItem.Manufacturer = item.Manufacturer;
                    storeItem.Price = item.Price;
                    storeItem.Description = item.Description;
                    item.Quantity -= quantity;

                    storeContext.storeItems.Add(storeItem);
                    warehouseContext.SaveChanges();
                    storeContext.SaveChanges();

                }
               
            }

        }

        public void orderSpecificItem(int quantity, storeItems storeItem)
        {
           // wItems wItem = new wItems();

            using (warehouseContext)
            {
                foreach (var i in warehouseContext.wItems.ToList())
                {
                    if (i.Name.Equals(storeItem.Name))
                    {
                        storeItem.Quantity += quantity;
                       // storeContext.storeItems.Add(storeItem);
                        i.Quantity -= quantity;
                    }
                    warehouseContext.SaveChanges();
                    storeContext.SaveChanges();
                }
                
            }

        }



    }
}
