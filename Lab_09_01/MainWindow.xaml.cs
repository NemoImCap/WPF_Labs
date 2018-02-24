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

using Lab_09_01.BusinessLayer.Interfaces;
using Lab_09_01.BusinessLayer.Models;
using Lab_09_01.BusinessLayer.Services;

namespace Lab_09_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<SellerViewModel> sellers;
        ISellerService sellerService;

        public MainWindow()
        {  
            InitializeComponent();
            //InitCarList();

            sellerService = new SellerService("TestDbConnection");
            sellers = sellerService.GetAll();
            cBoxGroup.DataContext = sellers;
        }

        private void InitCarList()
        {
            //context.Cars.Load();
            //dGrid.DataContext = context.Cars.Local;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var car = new CarViewModel();
            var dialog = new EditCarWindow(car);
            var result = dialog.ShowDialog();
            if (result == true)
            {
                var seller = (SellerViewModel)cBoxGroup.SelectedItem;
                int sIndex = sellers.IndexOf(seller);
                sellerService.AddCarToSeller(seller.SellerId, car);
                dialog.Close();
                ResetCollection();

                cBoxGroup.SelectedIndex = sIndex;
                lBox.SelectedIndex = seller.Cars.IndexOf(seller.Cars.Last());
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            CarViewModel car = lBox.SelectedItem as CarViewModel;
            EditCarWindow ecw = new EditCarWindow(car);
            ecw.Title = "Редактировать " + car.CarId + " - " + car.Brand;
            var result = ecw.ShowDialog();
            if (result == true)
            {
                sellerService.UpdateCar(car);
                ResetCollection();
                ecw.Close();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo); if (result == MessageBoxResult.Yes)
            {
                CarViewModel car = lBox.SelectedItem as CarViewModel;

                if (car != null)
                {
                    var seller = (SellerViewModel)cBoxGroup.SelectedItem;
                    int sIndex = sellers.IndexOf(seller);
                    int cIndex = seller.Cars.Last() == car ? seller.Cars.IndexOf(car) - 1 : seller.Cars.IndexOf(car);
                    sellerService.RemoveCarFromSeller(seller.SellerId, car.CarId);
                    ResetCollection();

                    cBoxGroup.SelectedIndex = sIndex;
                    lBox.SelectedIndex = cIndex;
                    
                }
            }

        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void ResetCollection()
        {
            sellers.Clear();
            foreach (SellerViewModel seller in sellerService.GetAll())
            {
                sellers.Add(seller);
            }
        }
    }
}
