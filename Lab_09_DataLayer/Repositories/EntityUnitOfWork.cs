using System;
using Lab_09_01.DataLayer.Entities; 
using Lab_09_01.DataLayer.Interfaces;
using Lab_09_01.DataLayer.EFContext;

namespace Lab_09_01.DataLayer.Repositories
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        AutoStoreContext context;
        SellersRepository sellersRepository;
        CarRepository carsRepository;

        public EntityUnitOfWork(string name)
        {
            context = new AutoStoreContext(name);
        }
        public IRepository<Seller> Sellers
        {
            get
            {
                if (sellersRepository == null)
                    sellersRepository = new SellersRepository(context);

                return sellersRepository;
            }
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (carsRepository == null)
                    carsRepository = new CarRepository(context);
                return carsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
    }
}
