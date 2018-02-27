using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_09_01.BusinessLayer.Models
{
    public class CarViewModel : IDataErrorInfo
    {
        public int CarId { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public String PicturePath { get; set; }

        private DateTime _date = DateTime.Now;

        public DateTime Year
        {
            get { return _date; }
            set { _date = value; }
        }
        public int EngineCapacity { get; set; }
        public decimal Cost { get; set; }
        public bool Sold { get; set; }


        string IDataErrorInfo.Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Brand":
                        if (String.IsNullOrEmpty(Brand))
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case "Model":
                        if (String.IsNullOrEmpty(Model))
                        {
                            error = "Поле не может быть пустым";
                        }
                        break;
                    case "Year":
                        if (Year.IsDaylightSavingTime())
                        {
                            error = "Не является датой";
                        }
                        break;
                    case "EngineCapacity":
                        if (EngineCapacity <= 0)
                        {
                            error = "Объем двигателя не может быть отрицательным или равен 0";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
