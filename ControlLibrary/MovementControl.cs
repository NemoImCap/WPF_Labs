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

    public class MovementControl : Control, INotifyPropertyChanged
    {
        #region DependencyProperty Content

        Storyboard sb = new Storyboard();
        static TimeSpan time;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        //public event PropertyChangedEventHandler Moved;
        public event PropertyChangedEventHandler PropertyChanged;




        public ImageSource ImageSource
        {
            get { return GetValue(ImageSourceProperty) as ImageSource; }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty =
          DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MovementControl));

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
            set
            {
                SetValue(XProperty, value);
                OnPropertyChanged("X");
            }
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
                OnPropertyChanged("Y");
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

        #endregion

        public void Init(TimeSpan duration)
        {
            time = TimeSpan.FromSeconds(duration.Seconds*Speed);

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

        private void Sb_Completed(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        public void Move()
        {
            sb.Begin();
        }

        public void Stop()
        {
            if (sb.GetIsPaused())
            {
                sb.Resume();
            }
            else
            {
                sb.Pause();
            }
        }


        static MovementControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovementControl), new FrameworkPropertyMetadata(typeof(MovementControl)));
            // Initialize ImagePath dependency properties
        }
    }
}
