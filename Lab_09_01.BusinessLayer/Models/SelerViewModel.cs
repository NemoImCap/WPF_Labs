using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_09_01.BusinessLayer.Models
{
    public class SellerViewModel
    {
        public int SellerId { get; set; }
        public String Name { get; set; }
        public String Owner { get; set; }
        public ObservableCollection<CarViewModel> Cars { get; set; }
    }
}
