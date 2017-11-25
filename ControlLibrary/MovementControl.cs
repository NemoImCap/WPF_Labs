using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ControlLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLibrary;assembly=ControlLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    ///

    public class MovementControl : Control
    {
        #region DependencyProperty Content

        Storyboard sb = new Storyboard();
        static TimeSpan time;
        private BitmapImage[] images;
        private DispatcherTimer dT = new DispatcherTimer();
        private int counter = 0;

        public bool bIsFinished;
        public bool bIsHitted;
        public int currentCountRound = 1;

        public ImageSource ImageSource
        {
            get { return GetValue(ImageSourceProperty) as ImageSource; }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty =
          DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MovementControl));

        public string HourseName
        {
            get { return (string)GetValue(HourseNameProperty); }
            set { SetValue(HourseNameProperty, value); }
        }
        public static readonly DependencyProperty HourseNameProperty =
          DependencyProperty.Register("HourseName", typeof(string), typeof(MovementControl));

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty =
          DependencyProperty.Register("Speed", typeof(double), typeof(MovementControl));

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
                DependencyProperty.Register("X", typeof(double), typeof(MovementControl),
                    new FrameworkPropertyMetadata(default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnMoved),
                    new CoerceValueCallback(CoerceMove)));

        public PathGeometry NewGeometry
        {
            get { return (PathGeometry)GetValue(NewGeometryProperty); }
            set { SetValue(NewGeometryProperty, value); }
        }

        public static readonly DependencyProperty NewGeometryProperty =
                DependencyProperty.Register("NewGeomerty", typeof(PathGeometry), typeof(MovementControl));


        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }

        public static readonly DependencyProperty YProperty =
                DependencyProperty.Register("Y", typeof(double), typeof(MovementControl),
                    new FrameworkPropertyMetadata(default(double),
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnMoved),
                    new CoerceValueCallback(CoerceMove)));


        private static object CoerceMove(DependencyObject d, object value)
        {
            return value;
        }

        private static void OnMoved(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Random rnd = new Random();

            time = TimeSpan.FromSeconds(rnd.Next(15, 25));
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void Init(TimeSpan duration)
        {
            images = new BitmapImage[10];
            images[0] = new BitmapImage(new Uri("/Images/hourse0.png", UriKind.Relative));
            images[1] = new BitmapImage(new Uri("/Images/hourse1.png", UriKind.Relative));
            images[2] = new BitmapImage(new Uri("/Images/hourse2.png", UriKind.Relative));
            images[3] = new BitmapImage(new Uri("/Images/hourse3.png", UriKind.Relative));
            images[4] = new BitmapImage(new Uri("/Images/hourse4.png", UriKind.Relative));
            images[5] = new BitmapImage(new Uri("/Images/hourse5.png", UriKind.Relative));
            images[6] = new BitmapImage(new Uri("/Images/hourse6.png", UriKind.Relative));
            images[7] = new BitmapImage(new Uri("/Images/hourse7.png", UriKind.Relative));
            images[8] = new BitmapImage(new Uri("/Images/hourse8.png", UriKind.Relative));
            images[9] = new BitmapImage(new Uri("/Images/hourse9.png", UriKind.Relative));

            dT.Tick += new EventHandler(dT_Tick);

            time = TimeSpan.FromSeconds(duration.Seconds/Speed);
            dT.Interval = new TimeSpan(0, 0, 0, 0, time.Seconds);
            sb.Completed += Sb_Completed;

            DoubleAnimationUsingPath animation = new DoubleAnimationUsingPath();
            DoubleAnimationUsingPath animation2 = new DoubleAnimationUsingPath();

            ////Geometry.Parse("M 100,200 C 100,25 400,350 400,175 H 280")
            animation = new DoubleAnimationUsingPath
            {
                Duration = time,
                RepeatBehavior = RepeatBehavior.Forever,
                PathGeometry = PathGeometry.CreateFromGeometry(NewGeometry)
            };

            animation2 = new DoubleAnimationUsingPath
            {
                Duration = time,
                RepeatBehavior = RepeatBehavior.Forever,
                PathGeometry = PathGeometry.CreateFromGeometry(NewGeometry)
            };

            animation.Source = PathAnimationSource.X;
            animation2.Source = PathAnimationSource.Y;

            //a.Duration = TimeSpan.FromSeconds(3);
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTarget(animation2, this);



            Storyboard.SetTargetProperty(animation, new PropertyPath(XProperty));
            Storyboard.SetTargetProperty(animation2, new PropertyPath(YProperty));

            sb.Children.Add(animation);
            sb.Children.Add(animation2);
        }

        void dT_Tick(object sender, EventArgs e)
        {
            ImageSource = images[counter % images.Length];
            counter++;
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        public void Move()
        {
            bIsFinished = false;
            sb.Begin();
            dT.Start();
        }

        public void Stop()
        {
            if (sb.GetIsPaused())
            {
                sb.Resume();
                dT.Start();
            }
            else
            {
                sb.Pause();
                dT.Stop();
            }
        }

        public void Finish()
        {
            bIsFinished = true;
            sb.Stop();
            dT.Stop();
        }


        static MovementControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovementControl), new FrameworkPropertyMetadata(typeof(MovementControl)));
            // Initialize ImagePath dependency properties

        }
    }
}
