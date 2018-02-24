using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lab_09_01.DataLayer.Entities;

namespace Lab_09_01.DataLayer.EFContext
{
    class AutoStoreInitializer: DropCreateDatabaseIfModelChanges<AutoStoreContext>
    {
        protected override void Seed(AutoStoreContext context)
        {
            context.Sellers.AddRange(new Seller[]
            {
                new Seller()
                {
                    Name = "BMW", Owner = "Иванов Иван", Address = "Минск, ул. Климкина", Phone = "8-017-532-33-20",
                    Cars = new List<Car>
                    {
                        new Car() { Brand = "BMW", Model = "M3", PicturePath = "m3.jpg", Year = new DateTime(2010,01,01), EngineCapacity = 2500, Cost = 10000, Sold = false },
                        new Car() { Brand = "BMW", Model = "X3", PicturePath = "x3.jpg", Year = new DateTime(2012,01,01), EngineCapacity = 3500, Cost = 15000, Sold = false },
                        new Car() { Brand = "BMW", Model = "X6", PicturePath = "x6.jpg", Year = new DateTime(2013,01,01), EngineCapacity = 4000, Cost = 20000, Sold = false },
                    }
                },
                new Seller()
                {
                    Name = "Audi", Owner = "Петр Петров", Address = "Минск, ул. Короля", Phone = "8-017-895-31-38",
                    Cars = new List<Car>
                    {
                        new Car() { Brand = "Audi", Model = "TT", PicturePath = "tt.jpg", Year = new DateTime(2010,01,01), EngineCapacity = 3000, Cost = 12000, Sold = false },
                        new Car() { Brand = "Audi", Model = "A4", PicturePath = "a4.jpg", Year = new DateTime(1998,01,01), EngineCapacity = 1600, Cost = 5000, Sold = false },
                        new Car() { Brand = "Audi", Model = "A6", PicturePath = "a6.jpg", Year = new DateTime(2004,01,01), EngineCapacity = 2400, Cost = 10000, Sold = false },
                    }
                },
                new Seller()
                {
                    Name = "Volkswagen", Owner = "Васильев Василий", Address = "Минск, ул. Шаранговича", Phone = "8-017-323-42-09",
                    Cars = new List<Car>
                    {
                        new Car() { Brand = "Volkswagen", Model = "Polo", PicturePath = "polo.jpg", Year = new DateTime(2010,01,01), EngineCapacity = 3000, Cost = 12000, Sold = false },
                        new Car() { Brand = "Volkswagen", Model = "Jetta", PicturePath = "jeеta.jpg", Year = new DateTime(2008,01,01), EngineCapacity = 2000, Cost = 9000, Sold = false },
                    }
                },

            });
        }
}
}
