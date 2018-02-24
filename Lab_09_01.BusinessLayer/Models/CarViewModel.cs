using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_09_01.BusinessLayer.Models
{
    public class CarViewModel
    {
        public int CarId { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public String PicturePath { get; set; }
        public DateTime Year { get; set; }
        public int EngineCapacity { get; set; }
        public decimal Cost { get; set; }
        public bool Sold { get; set; }
    }
}
