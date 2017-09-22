using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Список сотрудников
        /// </summary>
        ObservableCollection<Employee> employees;

        /// <summary>
        /// Валидация данных сотрудников
        /// </summary>
        Employee employee = new Employee();
        
        /// <summary>
        /// Список должностей
        /// </summary>
        ObservableCollection<string> workpositions;
        /// <summary>
        /// Список городов
        /// </summary>
        ObservableCollection<string> citynames;
        /// <summary>
        /// Список улиц
        /// </summary>
        ObservableCollection<string> streetnames;


        public MainWindow()
        {
            InitializeComponent();

            //// ///// ///// /// ////
            //employee = new Employee();
            grid.DataContext = employee;
            InitData();
            ///// ///// /////
        }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        private void InitData()
        {
            workpositions = new ObservableCollection<string>();
            citynames = new ObservableCollection<string>();
            streetnames = new ObservableCollection<string>();

            //employees = new ObservableCollection<Employee>();

            employees = LoadEmployeesFromFile("data.txt");
            workpositions = InitComboBoxData(TypeComboBox.WORKPOS);
            citynames = InitComboBoxData(TypeComboBox.CITYNAME);
            streetnames = InitComboBoxData(TypeComboBox.STREETNAME);

            work_cb.DataContext = workpositions;
            city_cb.DataContext = citynames;
            street_cb.DataContext = streetnames;
            result_lb.DataContext = employees;
        }

        /// <summary>
        /// Загрузка списка сотрудников из файла
        /// </summary>
        /// <param name="filename">имя файла</param>
        /// <returns>Список сотрудников</returns>
        private ObservableCollection<Employee> LoadEmployeesFromFile(string filename)
        {
            ObservableCollection<Employee> empl = new ObservableCollection<Employee>();

            if (!File.Exists(filename))
            {
                MessageBox.Show("Файл "+filename+" не существует!");
                return new ObservableCollection<Employee>();
            }
            else
            {
                try
                {
                    string[] mass = File.ReadAllLines(filename);
                    int n = 0;

                    foreach (string m in mass)

                    {
                        char[] sep = { '|' };

                        string[] mass_s = m.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                        try
                        {
                            empl.Add(
                                new Employee() { SurName = mass_s[0], Pay = Convert.ToDouble(mass_s[1]), WorkPosition = mass_s[2], CityName = mass_s[3], StreetName = mass_s[4], NumberHouse = Convert.ToInt32(mass_s[5]) }
                            );
                            n++;
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(String.Format("Строка:{0} - {1}", n + 1, ex.Message));
                            //return null;
                        }
                    }
                    return empl;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new ObservableCollection<Employee>();
                }
            }
        }

        /// <summary>
        /// Сохранение списка сотрудников в файл
        /// </summary>
        /// <param name="collection">Список сотрудников</param>
        /// <param name="filename">имя файла</param>
        /// <returns>сообщение о завершении сохранения</returns>
        private string SaveEmployeesToFile(ObservableCollection<Employee> collection, string filename)
        {
            if (collection != null)
            {
                if (collection.Count > 0)
                {
                    try
                    {
                        FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                        StreamWriter writer = new StreamWriter(fs);

                        foreach (Employee item in collection)
                        {
                            writer.WriteLine(item.SurName + "|" + item.Pay + "|" + item.WorkPosition + "|" + item.CityName + "|" + item.StreetName + "|" + item.NumberHouse);
                        }

                        writer.Flush();
                        writer.Dispose();
                        fs.Close();

                        return "Успешное сохранение файла - "+filename;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            return "Нету данных для сохранения. Файл "+filename+" не будет создан";
        }

        /// <summary>
        /// Тип данных для ComboBox
        /// </summary>
        enum TypeComboBox
        {
            WORKPOS,
            CITYNAME,
            STREETNAME
        }

        /// <summary>
        /// Инициализация данных для ComboBox
        /// </summary>
        private ObservableCollection<string> InitComboBoxData(TypeComboBox type)
        {
            HashSet<string> data = new HashSet<string>();

            if(employees != null)
            {
                if(employees.Count > 0)
                {
                    if (type == TypeComboBox.WORKPOS)
                    {
                        foreach (Employee item in employees)
                        {
                            data.Add(item.WorkPosition);
                        }
                    }
                    else if (type == TypeComboBox.CITYNAME)
                    {
                        foreach (Employee item in employees)
                        {
                            data.Add(item.CityName);
                        }
                    }
                    else if (type == TypeComboBox.STREETNAME)
                    {
                        foreach (Employee item in employees)
                        {
                            data.Add(item.StreetName);
                        }
                    }

                    return new ObservableCollection<string>(data);
                }
            }

            return new ObservableCollection<string>();
        }

        /// <summary>
        /// Добавление в коллекцию типа string значения и выбор этого значения в ComboBox
        /// </summary>
        /// <param name="inObject">Объект типа ComboBox</param>
        /// <param name="collection">коллекция типа string</param>
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

        /// <summary>
        /// Добавить сотрудника в список
        /// </summary>
        private void add_tb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Добавление и выбор введенного значения в ComboBox
                AddAndSelectNameInCombobox(work_cb, workpositions);
                AddAndSelectNameInCombobox(city_cb, citynames);
                AddAndSelectNameInCombobox(street_cb, streetnames);

                employees.Add(
                    new Employee() { SurName = surename_tb.Text, Pay = Convert.ToDouble(zp_tb.Text), WorkPosition = work_cb.Text, CityName = city_cb.Text, StreetName = street_cb.Text, NumberHouse = Convert.ToInt32(numberhouse_tb.Text) }
                ); // Добавление в коллекцию сотрудника

                // Очистка полей
                surename_tb.Clear();
                zp_tb.Clear();
                work_cb.Text = string.Empty;
                city_cb.Text = string.Empty;
                street_cb.Text = string.Empty;
                numberhouse_tb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Событие при закрытии окна
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show(SaveEmployeesToFile(employees, "data.txt")); // Сохранение данных в файл и вывод сообщения
        }
    }
}
