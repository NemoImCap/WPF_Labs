using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double result = 0;

        Values values;

        BackgroundWorker backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();
            values = new Values();
            values.n = 1;
            backgroundWorker = (BackgroundWorker)this.Resources["worker"];
        }

        private void dispacher_bt_Click(object sender, RoutedEventArgs e)
        {
            change_bt.IsEnabled = false;
            worker_bt.IsEnabled = false;
            Thread t = new Thread(Calculate);
            t.Start();
        }


        private void Calculate()
        {
            var step = Math.Round((double)(values.n / 100));
            double summ = 0;
            double h = 0;

            h = (values.b - values.a) / (double)values.n;

            for (int i = 1; i <= values.n; i++)
            {
                summ += Math.Sin(values.a + h * i);

                if (i % step == 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => progress.Value = i/step ));
                }
            }

            result = summ * h;

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => change_bt.IsEnabled = true));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => worker_bt.IsEnabled = true));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => result_tb.Text = "Результат Dispatcher = "+result.ToString()));
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var step = Math.Round((double)(values.n / 100));
            double summ = 0;
            double h = 0;

            h = (values.b - values.a) / (double)values.n;

            for (int i = 1; i <= values.n; i++)
            {
                summ += Math.Sin(values.a +  h * i);

                if (i % step == 0)
                {
                    if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                    {
                        backgroundWorker.ReportProgress((int)(i / step));
                    }
                }
            }

            result = summ * h;
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            result_tb.Text = "Результат Backgroundworker = " + result.ToString();
        }

        private void worker_bt_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputDataWindow input_dlg = new InputDataWindow(values);

            input_dlg.ShowDialog();
        }
    }
}
