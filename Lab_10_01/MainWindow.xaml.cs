using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab_10_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Order> orderList = new List<Order>();

        public MainWindow()
        {
            InitializeComponent();

            orderList.AddRange(new Order[] {
                new Order(0,DateTime.Parse("10.01.2018"),"Костюк"),
                new Order(1,DateTime.Parse("22.01.2018"),"Моисак"),
                new Order(2,DateTime.Parse("01.02.2018"),"Моисак"),
                new Order(3,DateTime.Parse("14.01.2018"),"Костюк"),
                new Order(4,DateTime.Parse("09.01.2018"),"Володько"),
                new Order(5,DateTime.Parse("03.02.2018"),"Перепечко")
            });

            lBox.DataContext = orderList;
        }

        private void serialize_bt_Click(object sender, RoutedEventArgs e)
        {

            if (binary_rb.IsChecked == true)
            { 
                BinaryFormatter bf = new BinaryFormatter();

                using (FileStream fs = new FileStream("bin.dat", FileMode.OpenOrCreate))
                {
                    bf.Serialize(fs, orderList);
                }
            }

            if (xml_rb.IsChecked == true)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
                using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
                {
                    xs.Serialize(fs, orderList);
                }
            }

            if (objectxml_rb.IsChecked == true)
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("root");
                foreach (Order o in orderList)

                {   
                    // Создать элемент Order
                    XmlElement order = doc.CreateElement("Order");
                    order.SetAttribute("Number", o.Number.ToString());

                    //Создать подэлементы
                    XmlElement date = doc.CreateElement("Date");
                    date.InnerText = o.Date.ToString();
                    XmlElement manager = doc.CreateElement("Manager");
                    manager.InnerText = o.Manager;
                    XmlElement cost = doc.CreateElement("Cost");
                    cost.InnerText = o.Cost.ToString();

                    // Добавить подэлементы в элемент Car
                    order.AppendChild(date);
                    order.AppendChild(manager);
                    order.AppendChild(cost);

                    // Добавить элемент Car в документ 
                    try
                    {
                        root.AppendChild(order);
                    }
                    catch (InvalidOperationException) { };
                }
                doc.AppendChild(root);
                doc.Save("DOM.xml");

                }

                if (linqxml_rb.IsChecked == true)
            {
                XDocument doc = new XDocument();
                // Создание корня документа
                XElement root = new XElement("Orders");
                foreach (var order in orderList)
                {
                    // Создание одного элемента
                    XElement orderElement = new XElement("order",
                        new XAttribute("number", order.Number),
                        new XElement("date", order.Date),
                        new XElement("manager", order.Manager),
                        new XElement("cost", order.Cost));
                    root.Add(orderElement);
                }             
                // Поместить корень в документ
                doc.Add(root);
                // Сохранить документ
                doc.Save("linq.xml");
            }
        }

        private void deserialize_bt_Click(object sender, RoutedEventArgs e)
        {
            List<Order> newOrderList = new List<Order>();

            if (binary_rb.IsChecked == true)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream("bin.dat", FileMode.Open))
                {
                    newOrderList = (List<Order>)formatter.Deserialize(fs);
                }
            }

            if (xml_rb.IsChecked == true)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Order>));

                using (FileStream fs = new FileStream("data.xml", FileMode.Open))
                {
                    newOrderList = (List<Order>)xs.Deserialize(fs);
                }
            }

            if (objectxml_rb.IsChecked == true)
            {
                newOrderList.Clear();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("DOM.xml");

                XmlSerializer serializer = new XmlSerializer(typeof(List<Order>));
                newOrderList = (List<Order>)serializer.Deserialize(new XmlNodeReader(xmlDoc));
            }

            if (linqxml_rb.IsChecked == true)
            {
                newOrderList.Clear();

                XDocument doc = XDocument.Load("linq.xml");
                // Выделить root
                var root = doc.Root;
                // Обойти коллекцию элементов внутри root
                foreach (var element in root.Elements())
                {
                    var order = new Order();
                    order.Number = int.Parse(element.Attribute("number").Value);
                    order.Date = DateTime.Parse(element.Element("date").Value);
                    order.Manager = element.Element("manager").Value;
                    // Сохранить объект Car

                    newOrderList.Add(order);                            }      
            }

            lBox2.DataContext = newOrderList;
        }
    }
}
