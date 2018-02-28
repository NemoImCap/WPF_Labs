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

            sellerService = new SellerService("TestDbConnection");
            sellers = sellerService.GetAll();
            cBoxGroup.DataContext = sellers;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCar();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            UpdateCar();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveCar();
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

        private void AddCar()
        {
            var car = new CarViewModel();
            var dialog = new EditCarWindow(car, false);
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

        private void RemoveCar()
        {
            CarViewModel car = lBox.SelectedItem as CarViewModel;

            if (car != null)
            {
                var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo); if (result == MessageBoxResult.Yes)
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

        private void UpdateCar()
        {
            CarViewModel car = lBox.SelectedItem as CarViewModel;
            if (car != null)
            {
                EditCarWindow ecw = new EditCarWindow(car, true);
                ecw.Title = "Редактировать " + car.CarId + " - " + car.Brand;
                var result = ecw.ShowDialog();
                if (result == true)
                {
                    sellerService.UpdateCar(car);
                    ecw.Close();

                    var seller = (SellerViewModel)cBoxGroup.SelectedItem;
                    int sIndex = sellers.IndexOf(seller);
                    int cIndex = seller.Cars.IndexOf(car);

                    ResetCollection();
                    cBoxGroup.SelectedIndex = sIndex;
                    lBox.SelectedIndex = cIndex;
                }
            }
            else
            {
                MessageBox.Show("Выберите автомобиль для редактирования","Ошибка получения индекса",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void lBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item = null;
            item = GetElementFromPoint(lBox, e.GetPosition(lBox));
            if (item != null)
                UpdateCar();
        }
        private object GetElementFromPoint(ListBox box, Point point)
        {
            UIElement element = (UIElement)box.InputHitTest(point);
            while (true)
            {
                if (element == box)
                {
                    return null;
                }
                object item = box.ItemContainerGenerator.ItemFromContainer(element);
                bool itemFound = !(item.Equals(DependencyProperty.UnsetValue));
                if (itemFound)
                {
                    return item;
                }
                element = (UIElement)VisualTreeHelper.GetParent(element);
            }
        }

        private void lBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                RemoveCar();
            }
            if(e.Key == Key.Enter)
            {
                UpdateCar();
            }
            if(e.Key == Key.Insert)
            {
                AddCar();
            }
        }
    }
}
