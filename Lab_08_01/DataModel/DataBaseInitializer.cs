using System;
using System.Data.Entity;

namespace Lab_08_01
{
    class DataBaseInitializer: DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            context.Cars.AddRange(new Car[]
            {
                new Car() { Brand = "BMW", Model = "E3", Year = DateTime.Parse("01.01.2005"), Cost = 3000, Sold = false },
                new Car() { Brand = "Pegeot", Model = "306", Year = DateTime.Parse("01.01.2000"), Cost = 2000, Sold = false },
                new Car() { Brand = "Toyota", Model = "Camry", Year = DateTime.Parse("01.01.2010"), Cost = 3500, Sold = false },
                new Car() { Brand = "Mersedes", Model = "E1000", Year = DateTime.Parse("01.01.2003"), Cost = 2800, Sold = false },
                new Car() { Brand = "Rover", Model = "600", Year = DateTime.Parse("01.01.1998"), Cost = 2000, Sold = false },
                new Car() { Brand = "Audi", Model = "A4", Year = DateTime.Parse("01.01.1999"), Cost = 4500, Sold = false },
            });
        }
}
}
