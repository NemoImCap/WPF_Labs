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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_03_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            test.Init(TimeSpan.FromSeconds(10));
            test1.Init(TimeSpan.FromSeconds(15));
            test2.Init(TimeSpan.FromSeconds(5));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test.Stop();
            test1.Stop();
            test2.Stop();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            test.Move();
            test1.Move();
            test2.Move();
        }

        private void test_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MessageBox.Show("fds");
        }
    }
}
