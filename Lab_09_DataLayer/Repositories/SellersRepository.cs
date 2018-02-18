using System;
using System.Collections.Generic;
using System.Linq;
using Lab_09_01.DataLayer.Interfaces;
using Lab_09_01.DataLayer.Entities;
using Lab_09_01.DataLayer.EFContext;
using System.Data.Entity;

namespace Lab_09_01.DataLayer.Repositories
{
    class SellersRepository : IRepository<Seller>
    {
        AutoStoreContext context;
        public SellersRepository(AutoStoreContext context)
        {
            this.context = context;
        }
        public void Create(Seller t) { context.Sellers.Add(t); }

        public void Delete(int id) { var group = context.Sellers.Find(id); context.Sellers.Remove(group); }

        public IEnumerable<Seller> Find(Func<Seller, bool> predicate)
        {
            return context.Sellers.Include(s => s.Cars).Where(predicate).ToList();
        }

        public Seller Get(int id) { return context.Sellers.Find(id); }

        public IEnumerable<Seller> GetAll() { return context.Sellers.Include(s => s.Cars); }

        public void Update(Seller t) { context.Entry<Seller>(t).State = EntityState.Modified; }
    }
}
