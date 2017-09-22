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
            ofd.Filter = "Файлы (dat)|*.dat|Все файлы|*.*";
            var result = ofd.ShowDialog();
            if (result == true)
            {
                try
                {
                    // создаем объект BinaryReader
                    using (BinaryReader reader = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
                    {
                        shapes.Clear();
                        paint.Children.Clear();

                        // пока не достигнут конец файла
                        // считываем каждое значение из файла
                        while (reader.PeekChar() > -1)
                        {
                            double ShapePosX = reader.ReadDouble();
                            double ShapePosY = reader.ReadDouble();
                            int StrokeThickness = reader.ReadInt32();
                            double OuterRadius = reader.ReadDouble();
                            double InnerRadius = reader.ReadDouble();
                            byte BR = reader.ReadByte();
                            byte BG = reader.ReadByte();
                            byte BB = reader.ReadByte();
                            byte BA = reader.ReadByte();

                            byte SR = reader.ReadByte();
                            byte SG = reader.ReadByte();
                            byte SB = reader.ReadByte();
                            byte SA = reader.ReadByte();



                            Shape s = new Shape
                            {
                                ShapePositionX = ShapePosX,
                                ShapePositionY = ShapePosY,
                                StrokeThickness = StrokeThickness,
                                OuterRadius = OuterRadius,
                                InnerRadius = InnerRadius,
                                RColor = BR,
                                GColor = BG,
                                BColor = BB,
                                AColor = BA,

                                SRColor = SR,
                                SGColor = SG,
                                SBColor = SB,
                                SAColor = SA
                            };

                            s.InitShape(paint);
                            shapes.Add(s);
                        }
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
            sfd.Filter = "Файлы (dat)|*.dat|Все файлы|*.*";
            var result = sfd.ShowDialog();
            if (result == true)
            {

                try
                {
                    // создаем объект BinaryWriter
                    using (BinaryWriter writer = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create)))
                    {
                        // записываем в файл значение каждого поля структуры
                        foreach (Shape s in shapes)
                        {
                            writer.Write(s.ShapePositionX);
                            writer.Write(s.ShapePositionY);
                            writer.Write(s.StrokeThickness);
                            writer.Write(s.OuterRadius);
                            writer.Write(s.InnerRadius);
                            writer.Write(s.RColor);
                            writer.Write(s.GColor);
                            writer.Write(s.BColor);
                            writer.Write(s.AColor);
                            writer.Write(s.SRColor);
                            writer.Write(s.SGColor);
                            writer.Write(s.SBColor);
                            writer.Write(s.SAColor);
                        }
                    }

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
