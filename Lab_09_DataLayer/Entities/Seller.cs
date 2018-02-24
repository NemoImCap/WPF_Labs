using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_09_01.DataLayer.Entities
{
    public class Seller : IDataErrorInfo
    {

        public Seller()
        {
            Cars = new List<Car>();
        }

        string IDataErrorInfo.this[string columnName] => throw new NotImplementedException();

        public int SellerId { get; set; }
        public String Name { get; set; }
        public String Owner { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public List<Car> Cars { get; set; }

        string IDataErrorInfo.Error => throw new NotImplementedException();
    }
}
