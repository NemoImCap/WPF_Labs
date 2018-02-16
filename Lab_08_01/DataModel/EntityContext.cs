using System.Data.Entity;

namespace Lab_08_01
{
    class EntityContext : DbContext
    {
        public EntityContext(string name) : base(name)
        {
            Database.SetInitializer(new DataBaseInitializer());
        }
        public DbSet<Car> Cars { get; set; }
    }
}
