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
            if (!storeContext.storeItems.Any())
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
                        if (item.Quantity > quantity)   //adding check for warehouse availability
                        {
                            storeItem.Name = item.Name;
                            storeItem.Quantity += quantity;
                            storeItem.Manufacturer = item.Manufacturer;
                            storeItem.Price = item.Price;
                            storeItem.Description = item.Description;
                            item.Quantity -= quantity;

                            storeContext.storeItems.AddAsync(storeItem);
                        }
                        warehouseContext.SaveChangesAsync();
                        storeContext.SaveChangesAsync();

                    }

                }

            }
            else
            {
                using (storeContext)
                {
                    foreach (var itm in storeContext.storeItems.ToList())
                    {
                        var w = warehouseContext.wItems.FirstOrDefault(i => i.Name == itm.Name);
                        if (w.Quantity > quantity)
                        {
                            itm.Quantity += quantity;
                            w.Quantity -= quantity;
                            warehouseContext.wItems.Update(w);
                            storeContext.SaveChanges();
                            warehouseContext.SaveChanges();
                        }
                    }
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
                    if (i.Name.Equals(storeItem.Name)&&i.Quantity>quantity) //adding check for warhouse availability
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

        public void RestockStore()
        {
            int temp = 0;
            using (storeContext)
            {
                foreach(var item in storeContext.storeItems.ToList())
                {
                    if (item.Quantity < 50)
                    {
                        var w = warehouseContext.wItems.FirstOrDefault(i => i.Name == item.Name);
                        temp = 50 - item.Quantity;
                        if (w.Quantity > temp)
                        {
                            item.Quantity += temp;
                            w.Quantity -= temp;
                            warehouseContext.wItems.Update(w);
                            warehouseContext.SaveChanges();
                            storeContext.SaveChanges();
                        }


                    }
                }



            }
        }

    }



}
