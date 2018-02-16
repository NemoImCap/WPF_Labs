using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace Lab_08_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityContext context;

        public MainWindow()
        {
            context = new EntityContext("CarDbConnection");    
            InitializeComponent();
            InitCarList();
        }

        private void InitCarList()
        {
            context.Cars.Load();
            dGrid.DataContext = context.Cars.Local;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var car = new Car();
            EditCarWindow ecw = new EditCarWindow(car);
            ecw.Title = "Добавить автомобиль";
            var result = ecw.ShowDialog();
            if (result == true)
            {
                context.Cars.Add(car);
                context.SaveChanges();
                ecw.Close();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Car car = dGrid.SelectedItem as Car;
            EditCarWindow ecw = new EditCarWindow(car);
            ecw.Title = "Редактировать " + car.CarId + " - " + car.Brand;
            var result = ecw.ShowDialog();
            if (result == true)
            {
                context.SaveChanges();
                ecw.Close();
            }
            else
            {
                // вернуть начальное значение
                context.Entry(car).Reload();

                // перегрузить DataContext
                dGrid.DataContext = null;
                dGrid.DataContext = context.Cars.Local;
            }        
            
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo); if (result == MessageBoxResult.Yes)
            {
                Car car = dGrid.SelectedItem as Car;

                if (car != null)
                {
                    context.Cars.Remove(car);
                    context.SaveChanges();
                }
            }
        
        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
