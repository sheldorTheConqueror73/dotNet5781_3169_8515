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

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for busDetailsByDoubleClick.xaml
    /// </summary>
    public partial class busDetailsByDoubleClick : Window
    {
        public event Action<int> fuel1;
        public event Action<DateTime> lmaintenance;
        private int counter;
        DispatcherTimer timer;
        private int mode=0;

        public busDetailsByDoubleClick()
        {
            InitializeComponent();
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                TimeSpan ts = TimeSpan.FromSeconds(counter);
                labTimer.Content = ts.ToString(@"hh\:mm\:ss");
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

        private void timerFunc(int _counter)
        {
            counter = _counter;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void refuel_Button_Click(object sender, RoutedEventArgs e)
        {
            btnRefuel.Content = "Refueling...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            mode = 1;
            MessageBox.Show("sending to refuel...");
            timerFunc(720);//need to add the simulation;
        }

        private void fuelEvent()
        {
            if (fuel1 != null)
                fuel1(1200);
            labfuel.Content = "1200";           
        }

        private void maintenanceEvent()
        {
            DateTime date = DateTime.Now;
            if (lmaintenance != null)
                lmaintenance(date);
            labLMaintenance.Content = date.ToString();          
        }

        private void maintenance_Button_Click(object sender, RoutedEventArgs e)
        {
            btnMaintenance.Content = "Maintenance...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            mode = 2;
            MessageBox.Show("sending to maintenance...");
            timerFunc(8640);//need to add the simulation;
        }

      
    }
}
