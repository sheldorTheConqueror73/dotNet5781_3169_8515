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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for busDetailsByDoubleClick.xaml
    /// </summary>
    public partial class busDetailsByDoubleClick : Window
    {
        public event Action<int> fuel1;
        public event Action<DateTime> lmaintenance;
        public event Action<string> status1;


        private string st = "";
        private int counter=0;
        DispatcherTimer timer;
        private int mode = 0;

        public busDetailsByDoubleClick()
        {
            InitializeComponent();
            status1 += value => labStatus.Content = value;
            fuel1 += value => labfuel.Content = value;
            lmaintenance += value => labLMaintenance.Content = value;
            lmaintenance += value => labDistance.Content = 0;            

           /* while (counter!=0)
            {
                if (counter!=0)
                {
                    btnRefuel.IsEnabled = false;
                    btnMaintenance.IsEnabled = false;
                    Thread.Sleep(1000);
                }
                else
                {
                    btnRefuel.IsEnabled = true;
                    btnMaintenance.IsEnabled = true;
                    break;
                }
            }*/

        }

        private void timer_Tick(Object obj, EventArgs e)
        {
            if (counter > 0)
            {
                TimeSpan ts = TimeSpan.FromSeconds(counter);
                this.st = ts.ToString(@"hh\:mm\:ss");
                setTextToLabTimer(st);
                counter--;
            }
            else
            {

                timer.Stop();
                labTimer.Visibility = Visibility.Hidden;
                if (mode == 1)
                    fuelEvent();
                else if (mode == 2)
                    maintenanceEvent();
            }

        }

        void setTextToLabTimer(string text)
        {
            if (!CheckAccess())
            {
                Action<string> func = setTextToLabTimer;
                Dispatcher.BeginInvoke(func, new object[] { text });
            }
            else
            {
                this.labTimer.Content = text;
            }
        }


        public void DataWindow_Closing(object sender, CancelEventArgs e)
        {
           // e.Cancel = true;
           // this.Visibility = Visibility.Hidden;
        }

        private void timerFunc()
        {
            labTimer.Visibility = Visibility.Visible;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            //this.thread = new Thread(timer_Tick);            
            //this.thread.Start();
            timer.Start();
        }

        private void refuel_Button_Click(object sender, RoutedEventArgs e)
        {
            btnRefuel.Content = "Refueling...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            mode = 1;
            MessageBox.Show("sending to refuel...");
            counter = 12;
            if (status1 != null)
                status1("refueling");
            timerFunc(); //need to add the simulation;

        }

        private void fuelEvent()
        {
            btnRefuel.Content = "send to refuel";
            btnRefuel.IsEnabled = true;
            btnMaintenance.IsEnabled = true;
            if (fuel1 != null)
                fuel1(1200);
            labfuel.Content = "1200";
            if (status1 != null)
                status1("ready");
        }

        private void maintenanceEvent()
        {
            btnMaintenance.Content = "send to maintenance";
            btnRefuel.IsEnabled = true;
            btnMaintenance.IsEnabled = true;
            DateTime date = DateTime.Now;
            if (lmaintenance != null)
                lmaintenance(date);
            labLMaintenance.Content = date.ToString();
            if (status1 != null)
                status1("ready");
        }

        private void maintenance_Button_Click(object sender, RoutedEventArgs e)
        {
            btnMaintenance.Content = "Maintenance...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            mode = 2;
            MessageBox.Show("sending to maintenance...");
            counter = 14;
            if (status1 != null)
                status1("maintenance");
            timerFunc();//need to add the simulation;
        }


      /* while(!=ready)
            {
            btn1+btn2.isenbled=false;
            sleep(1000);
    }
    btn1+btn.isenbled-true;*/

    }
}
