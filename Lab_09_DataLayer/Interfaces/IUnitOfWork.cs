using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_09_01.DataLayer.Entities;

namespace Lab_09_01.DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Seller> Sellers { get; }
        IRepository<Car> Cars { get; }
        void Save();
    }
}
