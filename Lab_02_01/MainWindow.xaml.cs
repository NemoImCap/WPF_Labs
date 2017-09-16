using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Lab_02_01
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double number;

            if (value == null || value.ToString() == "")
                return new ValidationResult(false, "Поле не может быть пустым");
            else
            {
                if (Double.TryParse(value.ToString(), out number))
                    return new ValidationResult(false, "Не является строкой");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class NumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double number;

            if (value == null || value.ToString() == "")
                return new ValidationResult(false, "Поле не может быть пустым");
            else
            {
                if (!Double.TryParse(value.ToString(), out number))
                    return new ValidationResult(false, "Не является числом");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class IntValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;

            if (value == null || value.ToString() == "")
                return new ValidationResult(false, "Поле не может быть пустым");
            else
            {
                if (!Int32.TryParse(value.ToString(), out number))
                    return new ValidationResult(false, "Не является целым числом");
            }
            return ValidationResult.ValidResult;
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Сотрудники
        /// </summary>
        ObservableCollection<Employee> employees;
        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<string> workpositions;
        ObservableCollection<string> citynames;
        ObservableCollection<string> streetnames;


        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;

            workpositions = new ObservableCollection<string>();
            citynames = new ObservableCollection<string>();
            streetnames = new ObservableCollection<string>();

            employees = new ObservableCollection<Employee>();


            work_tb.DataContext = workpositions;
            city_tb.DataContext = citynames;
            street_tb.DataContext = streetnames;
            result_lb.DataContext = employees;
        }


        private void AddAndSelectNameInCombobox(ComboBox inObject, ObservableCollection<string> collection)
        {
            if (inObject.Text != "")
            {
                if (!collection.Contains(inObject.Text))
                {
                    collection.Add(inObject.Text);
                    inObject.SelectedValue = inObject.Text;
                }

                else
                {
                    inObject.SelectedValue = inObject.Text;
                }
            }
        }


        private void add_tb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddAndSelectNameInCombobox(work_tb, workpositions);
                AddAndSelectNameInCombobox(city_tb, citynames);
                AddAndSelectNameInCombobox(street_tb, streetnames);

                employees.Add(
                    new Employee() { SurName = surename_tb.Text, Pay = Convert.ToDouble(zp_tb.Text), WorkPosition = work_tb.Text, CityName = city_tb.Text, StreetName = street_tb.Text, NumberHouse = Convert.ToInt32(numberhouse_tb.Text) }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }
    }
}
