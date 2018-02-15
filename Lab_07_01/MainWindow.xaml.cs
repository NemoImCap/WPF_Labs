using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_07_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Car> Cars;

        public MainWindow()
        {
            Cars = new ObservableCollection<Car>();
            InitializeComponent();
            lBox.DataContext = Cars;
        }

        /// <summary>
        /// Заполнение коллекции данными
        /// </summary>
        void FillData()
        {
            Cars.Clear();
            foreach (var item in Car.GetAllCars())
            {
                Cars.Add(item);
            }
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var car = new Car()
            {
                Brand = "BMW",
                Model = "E3",
                Year = DateTime.Parse("01.01.2005"),
                Cost = 3000,
                Sold = false
            };

            car.Insert();
            FillData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var car = ((Car)lBox.SelectedItem);
            car.Brand = "Новый бренд";
            car.Sold = true;
            car.Update();
            FillData();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var id = 0;

            if ((Car)lBox.SelectedItem != null)
            {
                id = ((Car)lBox.SelectedItem).CarId;
                Car.Delete(id);
                FillData();
            }
        }
    }
}
