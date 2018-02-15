using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Values : IDataErrorInfo
    {

        public int a { get; set; }
        public int b { get; set; }
        public int n { get; set; }


        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "n":
                        if (n <= 0)
                        {
                            error = "Количество итераций должно быть больше 0";
                        }
                        break;
                    case "a":
                        if(a >= b)
                        {
                            error = "Нижний предел должен быть меньше верхнего";
                        }
                        break;
                    case "b":
                        if (b <= a )
                        {
                            error = "Верхний предел должен быть больше нижнего";
                        }
                        break;
                }
                return error;
            }
        }

        string IDataErrorInfo.Error => throw new NotImplementedException();
    }
}
