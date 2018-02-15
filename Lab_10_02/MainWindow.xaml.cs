using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab_05_01
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Shape> shapes = new ObservableCollection<Shape>();
        public Shape shape;

        public MainWindow()
        {
            InitializeComponent();

            shape = new Shape();
            data.DataContext = shape;
            bg_tb.Background = new SolidColorBrush(shape.BackgroundColor);
            sc_tb.Background = new SolidColorBrush(shape.StrokeColor);

            CommandBinding bindingSave = new CommandBinding(ApplicationCommands.Save);
            bindingSave.Executed += Save_Executed;
            bindingSave.CanExecute += Save_CanExecute;
            this.CommandBindings.Add(bindingSave);

            CommandBinding bindingOpen = new CommandBinding(ApplicationCommands.Open);
            bindingOpen.Executed += Open_Executed;
            bindingOpen.CanExecute += Open_CanExecute;
            this.CommandBindings.Add(bindingOpen);
        }

        /// <summary>
        /// Открытие файла и добавление элементов на поле
        /// </summary>
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы (xml)|*.xml|Все файлы|*.*";
            var result = ofd.ShowDialog();
            if (result == true)
            {
                try
                {
                    shapes.Clear();
                    paint.Children.Clear();


                    XDocument doc = XDocument.Load(ofd.FileName);
                    // Выделить root
                    var root = doc.Root;
                    // Обойти коллекцию элементов внутри root
                    foreach (var element in root.Elements())
                    {
                        var shape = new Shape();
                        shape.ShapePositionX = int.Parse(element.Attribute("posX").Value);
                        shape.ShapePositionY = int.Parse(element.Element("posY").Value);
                        shape.StrokeThickness = int.Parse(element.Element("stroke").Value);
                        shape.InnerRadius = int.Parse(element.Element("innerradius").Value);
                        shape.OuterRadius = int.Parse(element.Element("outerradius").Value);

                        shape.RColor = byte.Parse(element.Element("rcolor").Value);
                        shape.GColor = byte.Parse(element.Element("gcolor").Value);
                        shape.BColor = byte.Parse(element.Element("bcolor").Value);
                        shape.AColor = byte.Parse(element.Element("acolor").Value);

                        shape.SRColor = byte.Parse(element.Element("srcolor").Value);
                        shape.SGColor = byte.Parse(element.Element("sgcolor").Value);
                        shape.SBColor = byte.Parse(element.Element("sbcolor").Value);
                        shape.SAColor = byte.Parse(element.Element("sacolor").Value);
                        // Сохранить объект Car


                        shape.InitShape(paint);

                        shapes.Add(shape);
                    }

                        this.Title = "Фигуры - " + ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Условение при котором кнопка "Открыть" станет доступной
        /// </summary>
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Сохранение файла 
        /// </summary>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Файлы (xml)|*.xml|Все файлы|*.*";
            var result = sfd.ShowDialog();
            if (result == true)
            {
                try
                {
                    XDocument doc = new XDocument();
                    // Создание корня документа
                    XElement root = new XElement("Shapes");
                    foreach (var shape in shapes)
                    {
                        // Создание одного элемента
                        XElement orderElement = new XElement("shape",
                            new XAttribute("posX", shape.ShapePositionX),
                            new XElement("posY", shape.ShapePositionY),
                            new XElement("stroke",shape.StrokeThickness),
                            new XElement("innerradius", shape.InnerRadius),
                            new XElement("outerradius", shape.OuterRadius),

                            new XElement("rcolor", shape.RColor),
                            new XElement("gcolor", shape.GColor),
                            new XElement("bcolor", shape.BColor),
                            new XElement("acolor", shape.AColor),

                            new XElement("srcolor", shape.SRColor),
                            new XElement("sgcolor", shape.SGColor),
                            new XElement("sbcolor", shape.SBColor),
                            new XElement("sacolor", shape.SAColor)
                            );
                        root.Add(orderElement);
                    }
                    // Поместить корень в документ
                    doc.Add(root);
                    // Сохранить документ
                    doc.Save(sfd.FileName);
                    this.Title = "Фигуры - " + sfd.FileName;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Добавление фигуры на поле Canvas в месте клика мыши
        /// </summary>
        private void paint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickpoint = e.GetPosition((Canvas)sender);

            Shape s = new Shape
            {
                ShapePositionX = clickpoint.X,
                ShapePositionY = clickpoint.Y,
                StrokeThickness = shape.StrokeThickness,
                OuterRadius = shape.OuterRadius,
                InnerRadius = shape.InnerRadius,
                RColor = shape.RColor,
                GColor = shape.GColor,
                BColor = shape.BColor,
                AColor = shape.AColor,

                SRColor = shape.SRColor,
                SGColor = shape.SGColor,
                SBColor = shape.SBColor,
                SAColor = shape.SAColor
            };

            s.InitShape(paint);
            shapes.Add(s);
        }

        /// <summary>
        /// Диалоговое окно настройки линий
        /// </summary>
        private void editshape_tb_Click(object sender, RoutedEventArgs e)
        {
            SettingShapeWindow sswnd = new SettingShapeWindow(shape);
            sswnd.Owner = this;
            sswnd.ShowDialog();

            bg_tb.Background = new SolidColorBrush(shape.BackgroundColor);
            sc_tb.Background = new SolidColorBrush(shape.StrokeColor);
        }

        /// <summary>
        /// Условение при котором кнопка "Сохранить" станет доступной
        /// </summary>
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = paint.Children.Count != 0;
        }

        /// <summary>
        /// Очистка поля Canvas
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            shapes.Clear();
            paint.Children.Clear();
        }

        /// <summary>
        /// Вывод диалога "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AboutWindow abwnd = new AboutWindow();

            abwnd.Owner = this;
            abwnd.ShowDialog();
        }

        /// <summary>
        /// Заыершение работы приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
