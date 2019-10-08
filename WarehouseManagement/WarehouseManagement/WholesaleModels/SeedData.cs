using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;

namespace WarehouseManagement.WholesaleModels
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider service)
        {
            using (var context = new WholesaleContext(service.GetRequiredService<DbContextOptions<WholesaleContext>>()))
            {
                if (context.Items.Any()||context.Cathegories.Any())
                {
                    return;
                }
                else
                {
                    context.Cathegories.AddRange(
                        new Cathegory
                        {
                            CathegoryId=1,
                            Name="Bathroom Products"

                        },
                        new Cathegory
                        {
                            CathegoryId=2,
                            Name="Fruits"

                        },
                        
                        new Cathegory
                        {
                            CathegoryId=3,
                            Name="Vegetables"

                        });
                    context.Items.AddRange(

                     new Items
                     {
                         ItemId = 1,
                         Name = "Soap",
                         cathegory={
                             CathegoryId=1
                         }

                     },
                        new Items
                        {
                            ItemId = 2,
                            
                            Name = "Shampoo",
                            
                            cathegory ={
                             CathegoryId=1
                         }
                           
                        },
                           new Items
                           {
                               ItemId = 3,
                               Name = "Apples",
                               cathegory ={
                             CathegoryId=2
                         }
                           },
                              new Items
                              {
                                  ItemId = 4,
                                  Name = "Tomatoes",
                                  cathegory ={
                             CathegoryId=3
                         }
                              },
                                 new Items
                                 {
                                     ItemId = 5,
                                     Name = "Toothpaste",
                                     cathegory ={
                             CathegoryId=1
                         }
                                 },
                                    new Items
                                    {
                                        ItemId = 6,
                                        Name = "Oranges",
                                        cathegory ={
                             CathegoryId=2
                         }
                                    },
                                       new Items
                                       {
                                           ItemId = 7,
                                           Name = "Potatoes",
                                           cathegory ={
                             CathegoryId=3
                         }
                                       },
                                          new Items
                                          {
                                              ItemId = 8,
                                              Name = "Cucumbers",
                                              cathegory ={
                             CathegoryId=3
                         }
                                          },
                                             new Items
                                             {
                                                 ItemId = 9,
                                                 Name = "Strawberries",
                                                 cathegory ={
                             CathegoryId=2
                         }
                                             },
                                                new Items
                                                {
                                                    ItemId = 10,
                                                    Name = "Blueberries",
                                                    cathegory ={
                             CathegoryId=2
                         }
                                                }



                     );
                    context.SaveChanges();
                }
                
            }
        }

    }
}
