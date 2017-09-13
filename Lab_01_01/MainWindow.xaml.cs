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

namespace Lab_01_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }


        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (resb.Text == "0")
            {
                resb.Text = b.Content.ToString();
            }
            else
            {
                resb.Text += b.Content.ToString();
            }
            
        }

        private void dot_bt_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (resb.Text.Length >= 1)
            {
                if (b.Content.ToString() == ".")
                {
                    if (!resb.Text.Contains("."))
                    {
                        resb.Text += b.Content.ToString();
                    }
                }
            }
        }

        private void erase_bt_Click(object sender, RoutedEventArgs e)
        {
            if (resb.Text.Length > 1)
            {
                resb.Text = resb.Text.Substring(0, resb.Text.Length - 1);
            }

            else
            {
                resb.Text = "0";
                tb.Text = "0";
            }
        }

        private void reset_bt_Click(object sender, RoutedEventArgs e)
        {
            resb.Text = "0";
            tb.Text = "0";
        }

        private void showresult_Click(object sender, RoutedEventArgs e)
        {
        }

        private void zero_bt_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (resb.Text == "0")
            {
                resb.Text = b.Content.ToString();
            }
            else
            {
                resb.Text += b.Content.ToString();
            }
        }

        private void Exp_bt_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "sqr(" + resb.Text + ")";

            try
            {
                resb.Text = Math.Pow(Convert.ToDouble(resb.Text),2).ToString();
            }
            catch(Exception ex)
            {
                resb.Text = ex.Message;
            }
        }

        private void plusminus_Click(object sender, RoutedEventArgs e)
        {
            if (resb.Text != "0")
            {
                try
                {
                    resb.Text = (Convert.ToDouble(resb.Text) * -1).ToString();
                }
                catch(Exception ex)
                {
                    resb.Text = ex.Message;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "1/" + resb.Text;

                if (Convert.ToDouble(resb.Text) != 0)
                {
                    resb.Text = Math.Round(1 / Convert.ToDouble(resb.Text),3).ToString();
                }
                else
                {
                    MessageBox.Show("Деление на ноль недопустимо!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Torad_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "rad ("+resb.Text+")";

                resb.Text = DegreeToRadian(Convert.ToDouble(resb.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tograd_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "grad (" + resb.Text + ")";

                resb.Text = RadianToDegree(Convert.ToDouble(resb.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sin_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "sin (" + resb.Text + ")";

                resb.Text = Math.Round(Math.Sin(Convert.ToDouble(resb.Text)),3).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cos_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "cos (" + resb.Text + ")";

                if (toDegree_bt.IsChecked == true)
                {
                    resb.Text = Math.Round(Math.Cos(DegreeToRadian(Convert.ToDouble(resb.Text))), 3).ToString();
                }

                if (toRadians_bt.IsChecked == true)
                {
                    resb.Text = Math.Round(Math.Cos(Convert.ToDouble(resb.Text)), 3).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tan_tb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "tan (" + resb.Text + ")";

                if (toDegree_bt.IsChecked == true)
                {
                    if (Math.Cos(DegreeToRadian(Convert.ToDouble(resb.Text))) == 0)
                    {
                        resb.Text = Math.Round(Math.Tan(DegreeToRadian(Convert.ToDouble(resb.Text))), 3).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Тангенса угла "+resb.Text+" градусов - не существует!");
                    }
                }

                if (toRadians_bt.IsChecked == true)
                {
                    if (Math.Cos(Convert.ToDouble(resb.Text)) == 0)
                    {
                        resb.Text = Math.Round(Math.Tan(Convert.ToDouble(resb.Text)), 3).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Тангенса угла "+ resb.Text +" радиан - не существует!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb.Text = "sqrt (" + resb.Text + ")";

                 if (Convert.ToDouble(resb.Text) >= 0)
                 {
                    resb.Text = Math.Round(Math.Sqrt(Convert.ToDouble(resb.Text)), 3).ToString();
                 }
                 else
                 {
                    MessageBox.Show("Квадратный корень невозможен из отрицательного числа!");
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toDegree_bt_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("в градусах");
        }

        private void toRadians_bt_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("в радианах");
        }

        private void rad_tb_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "Rad (" + resb.Text + ")";
            resb.Text = DegreeToRadian(Convert.ToDouble(resb.Text)).ToString();
        }

        private void degree_tb_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "Deg (" + resb.Text + ")";
            resb.Text = RadianToDegree(Convert.ToDouble(resb.Text)).ToString();
        }
    }
}
