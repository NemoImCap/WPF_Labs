using Lab_09_01.BusinessLayer.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Lab_09_01
{
    /// <summary>
    /// Interaction logic for EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {
        CarViewModel car;
        public EditCarWindow()
        {
            InitializeComponent();
        }

        public EditCarWindow(CarViewModel car,bool bEdit) : this()
        {
            this.car = car;
            sold_tb.IsEnabled = bEdit;
            grid.DataContext = car;
        }

        private void ok_bt_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
