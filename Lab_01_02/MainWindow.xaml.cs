using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace Lab_01_02
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double number;

            if (value == null)
                return new ValidationResult(false, "value cannot be empty.");
            else
            {
                if (!Double.TryParse(value.ToString(),out number))
                    return new ValidationResult(false, "Не является числом");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class NumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double number;

            if (value == null)
                return new ValidationResult(false, "value cannot be empty.");
            else
            {
                if (Double.TryParse(value.ToString(), out number))
                {
                    if(number > 5)
                        return new ValidationResult(false, "Значение не может быть больше 5");
                }
            }
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<string> results;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            results = new ObservableCollection<string>();
            resul_lb.DataContext = results;
        }

        /// <summary>
        /// Нахождение факториала
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Factorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        private void showres_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                results.Clear(); // очистка коллекции

                double s = 0; // сумма s(x)
                double y = 0; // значение функции y(x)
                int n = 0;

                double xstart = Convert.ToDouble(xstart_tb.Text);
                double xstop = Convert.ToDouble(xstop_tb.Text);
                double xstep = Convert.ToDouble(xstep_tb.Text);
                int count = Convert.ToInt32(numb_tb.Text);

                while(xstart <= xstop)
                {
                    for (int k = 0; k < count; k++)
                    {
                        s += Math.Pow(Math.Log(Math.Pow(xstart, k)), k) / Factorial(k);
                    }

            
                    y = xstart/2;

                    
                    results.Add("s(x) = " + s + ", y(x) = " + y);
                    y = 0;
                    s = 0;
                    n++;
                    xstart+= xstep;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
