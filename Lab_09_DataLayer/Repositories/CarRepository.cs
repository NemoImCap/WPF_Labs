using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Lab_09_01.DataLayer.EFContext;
using Lab_09_01.DataLayer.Entities;
using Lab_09_01.DataLayer.Interfaces;

namespace Lab_09_01.DataLayer.Repositories
{
    class CarRepository : IRepository<Car>
    {
        AutoStoreContext context;

        public CarRepository(AutoStoreContext context)
        {
            this.context = context;
        }

        public void Create(Car t)
        {
            context.Cars.Add(t);
        }

        public void Delete(int id)
        {
            var car = context.Cars.Find(id);
            context.Cars.Remove(car);
            //context.Entry<Car>(car).State = EntityState.Deleted;
        }

        public IEnumerable<Car> Find(Func<Car, bool> predicate)
        {
            return context.Cars.Include(x => x.Brand).Where(predicate).ToList();
        }

        public Car Get(int id)
        {
            return context.Cars.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return context.Cars.Include(x => x.Brand);
        }

        public void Update(Car t)
        {
            context.Cars.
            context.Entry<Car>(t).State = EntityState.Modified;
        }
    }
}
