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
using ControlLibrary;

namespace Lab_03_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MovementControl> hourses = new List<MovementControl>();

        private DispatcherTimer dT = new DispatcherTimer();
        int countFinish = 0;

        public MainWindow()
        {
            InitializeComponent();

            hourses.Add(Zero);
            hourses.Add(Rain);
            hourses.Add(Sun);

            Zero.Init(TimeSpan.FromSeconds(10));
            Rain.Init(TimeSpan.FromSeconds(9));
            Sun.Init(TimeSpan.FromSeconds(11));

            dT.Tick += new EventHandler(dT_Tick);

            dT.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Zero.Stop();
            Rain.Stop();
            Sun.Stop();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            Zero.Move();
            Rain.Move();
            Sun.Move();

            dT.Start();

            countFinish = 0;
            raceresult.Items.Clear();
            for (int i = 0; i < hourses.Count; i++)
            {
                hourses[i].currentCountRound = 1;
            }
        }

        void dT_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i< hourses.Count; i++)
            {
                if (hourses[i] != null)
                {
                    Point pt = new Point(hourses[i].X, hourses[i].Y);
                    HitTestResult result = VisualTreeHelper.HitTest(canvas, pt);


                    if (result != null)
                    {
                        var img = result.VisualHit as Image;

                        if (img != null)
                        {
                            if (img.Name == "finish")
                            {
                                if (hourses[i].currentCountRound >= Convert.ToInt32(roundCount.Text))
                                {
                                    hourses[i].Finish();
                                    if (hourses[i].bIsFinished)
                                    {
                                        countFinish++;
                                        raceresult.Items.Add(hourses[i].HourseName);
                                    }
                                }
                                else
                                {
                                    hourses[i].bIsHitted = true;
                                }
                            }
                        }
                        else
                        {
                            if (hourses[i].bIsHitted)
                            {
                                hourses[i].bIsHitted = false;
                                hourses[i].currentCountRound++;
                            }

                        }
                    }

                }
            }

            if(countFinish == hourses.Count)
            {
                dT.Stop();
            }
        }

        private void Hourse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MovementControl mhourse = sender as MovementControl;

            hourseName.Text = mhourse.HourseName;
            hourseSpeed.Text = mhourse.Speed.ToString();
            hourseroundcount.Text = mhourse.currentCountRound.ToString();
        }

        private void Hourse_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MovementControl mhourse = sender as MovementControl;

            hourseposX.Text = "X = " + mhourse.X;
            hourseposY.Text = "Y = " + mhourse.Y;
        }
    }
}
