using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab_02_01
{
    public class Employee: IDataErrorInfo
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

        //private string surname;

        public string SurName { get; set ; }
        public double Pay { get; set; }
        public string WorkPosition { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int NumberHouse { get; set; }

        public string Error => throw new NotImplementedException();


        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "SurName")
                {
                    if (string.IsNullOrEmpty(SurName))
                        result = "Поле не должно быть пустым. Введите Фамилию";
                }
                if (columnName == "Pay")
                {
                    double number;

                    if (string.IsNullOrEmpty(Pay.ToString()))
                    {
                            result = "Поле не должно быть пустым. Введите заработную плату";
                    }
                    if (!Double.TryParse(Pay.ToString(), out number))
                    {
                        result = "Не является числом";
                    }
                    else
                    {
                        if (number <= 0)
                        {
                            result = "Заработная плата должна быть больше 0";
                        }
                    }
                }
                if (columnName == "WorkPosition")
                {
                    if (string.IsNullOrEmpty(WorkPosition))
                        result = "Поле не должно быть пустым. Введите должность";
                }

                if (columnName == "NumberHouse")
                {
                    int number;

                    if (string.IsNullOrEmpty(NumberHouse.ToString()))
                        result = "Поле не должно быть пустым. Введите номер дома";

                    if (!Int32.TryParse(NumberHouse.ToString(), out number))
                    {
                        result = "Не является числом";
                    }
                    else
                    {
                        if (number <= 0)
                        {
                            result = "Номер дома должен быть больше 0";
                        }
                    }
                }
                return result;
            }
        }



    }
}
