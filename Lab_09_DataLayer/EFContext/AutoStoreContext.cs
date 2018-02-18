using System.Data.Entity;
using Lab_09_01.DataLayer.Entities;

namespace Lab_09_01.DataLayer.EFContext
{
    class AutoStoreContext : DbContext
    {
        public AutoStoreContext(string name) : base(name) { Database.SetInitializer(new AutoStoreInitializer()); }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Seller> Sellers { get; set; }
    }
}
