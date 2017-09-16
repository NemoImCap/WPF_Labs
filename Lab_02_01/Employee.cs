using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02_01
{
    public class Employee
    {

        //public Employee(string surName, double pay, string workposition, string cityname, string streetname, int numberhouse)
        //{
        //    SurName = surName;
        //    Pay = pay;
        //    WorkPosition = workposition;
        //    CityName = cityname;
        //    StreetName = streetname;
        //    NumberHouse = numberhouse;
        //}

        public string SurName { get; set; }
        public double Pay { get; set; }
        public string WorkPosition { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int NumberHouse { get; set; }
    }
}
