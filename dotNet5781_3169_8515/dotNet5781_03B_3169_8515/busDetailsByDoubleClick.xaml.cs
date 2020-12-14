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
using dotNet5781_03B_3169_8515.utility;


namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for busDetailsByDoubleClick.xaml
    /// show details of bus and bring  the option: send to refueal/maintenance.
    /// </summary>
    public partial class busDetailsByDoubleClick : Window
    {
        /// <summary>
        /// event for sync property of bus between this window and the main window
        /// </summary>
        public event Action<int> fuel1;
        public event Action<DateTime> lmaintenance;
        public event Action<string> status1;
        public event Action<double> tim;

        private string st = "";
        private int counter = 0;//conter of the timer
        DispatcherTimer timer;//timer that responsibility for show timer when the bus send to action in this window.
        private int mode = 0;
        MainWindow mainWindow1;
        DispatcherTimer refresh;//timer of refreshing data of bus when this window is reopening.  
        DispatcherTimer dt;//property of timer that continue timer when this window opening when the bus is mid-action.

        /// <summary>
        /// ctor of the class get time in case of the bus in action. 
        /// </summary>

        public busDetailsByDoubleClick(DispatcherTimer _dt)
        {
            dt = _dt;
            InitializeComponent();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow1 = window as MainWindow;
                }
            }

            status1 += value => labStatus.Content = value;
            fuel1 += value => labfuel.Content = value;
            lmaintenance += value => labLMaintenance.Content = value;
            lmaintenance += value => labDistance.Content = 0;

            refresh = new DispatcherTimer();
            refresh.Tick += new EventHandler(refreshBusDetails);


            if ((mainWindow1.bsDisplay.SelectedItem as buses).Status != "ready")
            {
                btnRefuel.IsEnabled = false;
                btnMaintenance.IsEnabled = false;
                refresh.Start();
            }
        }
        /// <summary>
        /// the timer function, counting the time to zero.
        /// </summary>

        private void timer_Tick(Object obj, EventArgs e)
        {
            if (counter > 0)
            {
                if (mainWindow1.bsDisplay.SelectedItem != null)
                {
                    st = (mainWindow1.bsDisplay.SelectedItem as buses).Timer.TimeNow;
                    setTextToLabTimer(st);
                    counter--;
                }
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
        /// <summary>
        /// send to the label of the timer the time that left in string.
        /// </summary>
        /// <param name="text">the time the left</param>
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

        /// <summary>
        /// initialize the timer and starting it.
        /// </summary>
        private void timerFunc()
        {

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            labTimer.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// send the bus to refuel and start the timer according to time of refueling.
        /// </summary>

        private void refuel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (labfuel.Content.ToString() == "1200")
            {
                MessageBox.Show("bus already is fueled.");
                return;
            }
            dt.Start();
            (mainWindow1.bsDisplay.SelectedItem as buses).IconPath = "/src/pics/gasIcon2.png";
            btnRefuel.Content = "Refueling...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            mode = 1;
            MessageBox.Show("sending to refuel...");
            labStatus.Foreground = Brushes.Red;
            counter = 12;

            if (tim != null)
            {
                tim(counter);
                mainWindow1.busSort();
                mainWindow1.bsDisplay.Items.Refresh();
            }
            if (status1 != null)
            {
                status1("refueling");
                mainWindow1.busSort();
                mainWindow1.bsDisplay.Items.Refresh();
            }
            timerFunc();
        }
        /// <summary>
        /// update the property and arguments of the bus after refueling.
        /// </summary>
        private void fuelEvent()
        {
            btnRefuel.Content = "send to refuel";
            btnRefuel.IsEnabled = true;
            btnMaintenance.IsEnabled = true;
            if (labDangerous.Content.ToString() == "Yes")
            {
                labStatus.Content = "dangerous";
            }
            else
            {
                labStatus.Foreground = Brushes.LawnGreen;
                labStatus.Content = "ready";
            }
            if (fuel1 != null)
                fuel1(1200);
            labfuel.Content = "1200";
            mainWindow1.bsDisplay.Items.Refresh();
        }
        /// <summary>
        /// update the property and arguments of the bus after maintenance.
        /// </summary>
        private void maintenanceEvent()
        {

            btnMaintenance.Content = "send to maintenance";
            btnRefuel.IsEnabled = true;
            btnMaintenance.IsEnabled = true;
            labStatus.Content = "ready";
            labfuel.Content = "1200";
            (mainWindow1.bsDisplay.SelectedItem as buses).Dangerous = false;
            labDangerous.Content = "No";
            labDangerous.Foreground = Brushes.LawnGreen;
            DateTime date = DateTime.Now;
            if (lmaintenance != null)
            {
                lmaintenance(date);
                fuel1(1200);
            }
            labLMaintenance.Content = date.ToString().Split(' ')[0];
            labStatus.Foreground = Brushes.LawnGreen;
            mainWindow1.bsDisplay.Items.Refresh();

        }
        /// <summary>
        ///  send the bus to maintenance and start the timer according to time of maintenance.
        /// </summary>

        private void maintenance_Button_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            btnMaintenance.Content = "Maintenance...";
            btnRefuel.IsEnabled = false;
            btnMaintenance.IsEnabled = false;
            (mainWindow1.bsDisplay.SelectedItem as buses).IconPath = "/src/pics/repairIcon2.png";
            mode = 2;
            MessageBox.Show("sending to maintenance...");
            labStatus.Foreground = Brushes.Red;
            counter = 144;
            if (tim != null)
            {
                tim(counter);
                mainWindow1.busSort();
                mainWindow1.bsDisplay.Items.Refresh();
            }
            if (status1 != null)
            {
                status1("maintenance");
                mainWindow1.busSort();
                mainWindow1.bsDisplay.Items.Refresh();
            }
            timerFunc();//need to add the simulation;
        }

        private bool flag = true;
        /// <summary>
        /// function of the timer that responsible for update property and arguments of bus  when thiw window is reopening .
        /// </summary>

        public void refreshBusDetails(Object obj, EventArgs e)
        {
            if (mainWindow1.bsDisplay.SelectedItem != null)
            {
                if ((mainWindow1.bsDisplay.SelectedItem as buses).Status == "ready" || (mainWindow1.bsDisplay.SelectedItem as buses).Status == "dangerous")
                {
                    btnMaintenance.IsEnabled = true;
                    btnRefuel.IsEnabled = true;
                    if ((mainWindow1.bsDisplay.SelectedItem as buses).Status == "ready")
                    {
                        labStatus.Content = "ready";
                        (mainWindow1.bsDisplay.SelectedItem as buses).IconPath = "/src/pics/okIcon.png";
                        labStatus.Foreground = Brushes.LawnGreen;
                    }
                    if ((mainWindow1.bsDisplay.SelectedItem as buses).Dangerous == true)
                    {
                        labStatus.Content = "dangerous";
                        (mainWindow1.bsDisplay.SelectedItem as buses).IconPath = "/src/pics/warningIcon.png";
                        labStatus.Foreground = Brushes.Red;
                    }


                    if ((mainWindow1.bsDisplay.SelectedItem as buses).Fuel == 1200)
                        labfuel.Content = "1200";
                    if ((mainWindow1.bsDisplay.SelectedItem as buses).LastMaintenance.Day == (DateTime.Now.Day) && labStatus.Content.ToString() != "dangerous")
                    {
                        labLMaintenance.Content = (mainWindow1.bsDisplay.SelectedItem as buses).LastMaintenance.ToString().Split(' ')[0];
                        labDistance.Content = "0";
                    }


                    refresh.Stop();
                }
                else
                {
                    if (flag)
                    {
                        counter = (int.Parse((((mainWindow1.bsDisplay.SelectedItem as buses).Timer.TimeNow.ToString().Split(':')[0]))) * 3600 + int.Parse((((mainWindow1.bsDisplay.SelectedItem as buses).Timer.TimeNow.ToString().Split(':')[1]))) * 60 + int.Parse((((mainWindow1.bsDisplay.SelectedItem as buses).Timer.TimeNow.ToString().Split(':')[2])))) - 1;
                        timerFunc();
                        flag = false;
                    }
                }
            }

        }
    }
}

