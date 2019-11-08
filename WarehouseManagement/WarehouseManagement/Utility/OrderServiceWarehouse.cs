using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.WarehouseModels;

namespace WarehouseManagement.Utility
{
    public class OrderServiceWarehouse :IOrderServiceWarehouse
    {
        private WholesaleContext wholesaleContext;
      private WarehouseContext warehouseContext;

        public OrderServiceWarehouse(WholesaleContext wholesale, WarehouseContext warehouse)
        {
            wholesaleContext = wholesale;
            warehouseContext = warehouse;
        }

        //function to fill warehouse database based on wholesale database (transfers all items)

        public void OrderItemsForWarehouse(int quantity)
        {
            if (!warehouseContext.wItems.Any())
            {

                using (wholesaleContext)
                {
               

                    foreach (var item in wholesaleContext.Items)
                    {
                        wItems wItem = new wItems
                        {
                            Name = " ",
                            Quantity = 0,
                            Description=" ",
                            Manufacturer=" ",
                            Price=0
                        };
                        // wItem.wItemId = item.ItemId;
                        wItem.Name = item.Name;
                        wItem.Quantity += quantity;
                        wItem.Manufacturer = item.Manufacturer;
                        wItem.Price = item.Price;
                        wItem.Description = item.Description;
                        // wItem.cathegory.wCathegoryId = item.cathegory.CathegoryId;
                        warehouseContext.wItems.AddAsync(wItem);
                        warehouseContext.SaveChangesAsync();

                    }

                }
            }
            else
            {
                using (warehouseContext)
                {
                    foreach(var w in warehouseContext.wItems.ToList())
                    {
                        w.Quantity += quantity;
                        warehouseContext.SaveChanges();
                    }
                }
            }
        }

        //function to transfer specific item from wholesale to warehouse

        public void OrderSpecificItem(wItems item,int quantity)
        {
            using (wholesaleContext)
            {
                foreach (var i in wholesaleContext.Items.ToList())
                {
                    if (i.Name.Equals(item.Name))
                    {
                        item.Quantity += quantity;
                       // warehouseContext.wItems.Add(item);
                    }
                    warehouseContext.SaveChanges();
                }
               
            }
        }

        public void RestockWarehouse()
        {
            int temp = 0;
            using (warehouseContext)
            {
                foreach(var item in warehouseContext.wItems.ToList())
                {
                    if (item.Quantity < 50)
                    {
                        temp = 50 - item.Quantity;
                        item.Quantity += temp;

                        warehouseContext.SaveChanges();
                    }
                }
            }
        }

    }
}
