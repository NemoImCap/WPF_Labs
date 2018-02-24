using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab_09_01.DataLayer.Entities
{
    public class Car : IDataErrorInfo
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Бренд
        /// </summary>
        [Required]
        public String Brand { get; set; }
        /// <summary>
        /// Модель
        /// </summary>
        [Required]
        public String Model { get; set; }
        /// <summary>
        /// Путь к изображению авто
        /// </summary>
        public String PicturePath { get; set; }
        /// <summary>
        /// Год выпуска
        /// </summary>
        [Required]
        public DateTime Year { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int EngineCapacity { get; set; }
        public decimal Cost { get; set; }
        /// <summary>
        /// Продан ли автомобиль
        /// </summary>
        public bool Sold { get; set; }

        public int SellerId { get; set; }
        public Seller Seller { get; set; }

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
                }
                return error;
            }
        }
    }
}
