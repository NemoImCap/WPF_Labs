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
using System.Windows.Shapes;

namespace Lab_05_01
{


    /// <summary>
    /// Interaction logic for SettingShapeWindow.xaml
    /// </summary>
    public partial class SettingShapeWindow : Window
    {
        Shape mshape;

        public SettingShapeWindow(Shape shape)
        {
            InitializeComponent();

            mshape = shape;
            grid.DataContext = mshape; 
        }

        private void ok_tb_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ow = Owner as MainWindow;
            ow.shape = mshape;
            this.Close();
            //shape.BackgroundColor = Color.FromArgb(255, 125, 255, 0);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bd.Background = new SolidColorBrush(mshape.BackgroundColor);
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sd.Background = new SolidColorBrush(mshape.StrokeColor);
        }
    }
}
