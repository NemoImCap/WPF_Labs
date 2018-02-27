using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab_09_01.DataLayer.Entities
{
    public class Car
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
    }
}
