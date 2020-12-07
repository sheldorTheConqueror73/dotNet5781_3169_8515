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
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.Collections.ObjectModel;
using System.IO;
using dotNet5781_03B_3169_8515.utility;


namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public static bool sound = true;
        public static bool show = false;
        public static bool autosave = false;
        private static List<buses> busPool=new List<buses>();
        public  List<buses> BusPool
        {
            get { return busPool; }
            set { busPool = value; }
        }
        Random r = new Random();
        readonly string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
        DispatcherTimer timer;
        internal static System.Media.SoundPlayer player;
        public MainWindow()//add mini payer to menu
        {
            InitializeComponent();
            if(autosave)
                buses.load(ref busPool, $"{appPath}\\src\\storage\\DataFile.txt",show);
            else
                initBus();
            readSettings();
            bsDisplay.ItemsSource = busPool;
            cbSort.SelectedIndex = 0;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(refreshingProgram);
            timer.Interval = new TimeSpan(0, 0, 1);
            
            
         
        }

        /// <summary>
        /// used to initialize 13 random buses
        /// </summary>
        public void initBus()
        {
            for (int i = 0; i < 13; i++)
            {
                bool flag = true;
                string id = "";
                DateTime rd = randomDate();
                while (flag)
                {
                    if(rd.Year<2018)
                        id = r.Next(1000000, 10000000).ToString();//make sure id format matches MD 
                    else
                        id = r.Next(10000000, 100000000).ToString();
                    flag = false;
                    foreach (var bus in busPool)
                        if (id == bus.Id)//need to change accessers
                        {
                            flag = true;
                            break;
                        }
                }
                DateTime lastM = randomDate(1);              
                busPool.Add(new buses(rd, lastM, id, r.Next(0, buses.FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000),"ready",new Timerclass(0) { TimeNow="00:00:00"}, "/src/pics/okIcon.png"));
            }
            busPool[0].LastMaintenance=new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            busPool[0].Status = "dangerous";
            BusPool[0].Dangerous = true;
            busPool[0].IconPath = "/src/pics/warningIcon.png";
            busPool[1].Distance=19999;
            busPool[2].Fuel = 0;

        }

       /// <summary>
      ///  returns a random date from current date to 1/1/1980
      /// </summary>
      /// <param name="mode">if mode is set 1 it will return a random date but in same year</param>
     /// <returns></returns>
        private DateTime randomDate(int mode = 0)
        {
            int month, day, year;
            year = r.Next(1980, DateTime.Now.Year + 1);
            if (year == DateTime.Now.Year)
            {
                month = r.Next(1, DateTime.Now.Month + 1);
                if (month == DateTime.Now.Month)
                    day = r.Next(1, DateTime.Now.Day + 1);
                else
                    day = r.Next(1, 32);
            }
            else
            {
                month = r.Next(1, 13);
                day = r.Next(1, 32);
            }
            if (mode == 1)
                year = DateTime.Now.Year;
            try { 
            return new DateTime(year, month, day);
            }
            catch(Exception e)
            {
                return randomDate(mode);
            }
        }
        /// <summary>
        /// opens teh buss action menu
        /// </summary>
        private void bsDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((bsDisplay.SelectedItem as buses) == null)
                return;
            busDetailsByDoubleClick bDLClk = new busDetailsByDoubleClick(timer);
            string st= (bsDisplay.SelectedItem as buses).IdFormat;
            int fuel = (bsDisplay.SelectedItem as buses).Fuel;
            DateTime lmaintenance = (bsDisplay.SelectedItem as buses).LastMaintenance;


            bDLClk.labStatus.Content = (bsDisplay.SelectedItem as buses).Status;
            if ((bsDisplay.SelectedItem as buses).Status=="ready")
            {
                bDLClk.labStatus.Foreground = Brushes.LawnGreen;
            }
            else if ((bsDisplay.SelectedItem as buses).Status=="mid-ride")//need to chnage fater update
            {
                bDLClk.labStatus.Foreground = Brushes.Orange;
            }
            else if ((bsDisplay.SelectedItem as buses).Status== "refueling"|| (bsDisplay.SelectedItem as buses).Status == "maintenance")
            {
                bDLClk.labStatus.Foreground = Brushes.Red;
            }
            bDLClk.labNameBus.Content = "Bus Id: " + st;
            bDLClk.labfuel.Content = fuel.ToString();
            bDLClk.labDistance.Content = (bsDisplay.SelectedItem as buses).Distance.ToString();
            bDLClk.labtotalDist.Content = (bsDisplay.SelectedItem as buses).TotalDistance.ToString(); 
            bDLClk.labRegistration.Content= (bsDisplay.SelectedItem as buses).RegistrationDate.ToString().Split(' ')[0];
           
            if ((bsDisplay.SelectedItem as buses).Dangerous)
            {
                bDLClk.labDangerous.Content = "Yes";
                bDLClk.labDangerous.Foreground = Brushes.Red;

            }
            else
            {
                bDLClk.labDangerous.Content = "No";
                bDLClk.labDangerous.Foreground = Brushes.LawnGreen;
            }        

            bDLClk.labLMaintenance.Content = lmaintenance.ToString().Split(' ')[0];

            
            bDLClk.fuel1 += value => (bsDisplay.SelectedItem as buses).Fuel=value;
            bDLClk.lmaintenance += value=> (bsDisplay.SelectedItem as buses).LastMaintenance=value;
            bDLClk.lmaintenance += value => (bsDisplay.SelectedItem as buses).Distance =0;
            bDLClk.status1 += value => (bsDisplay.SelectedItem as buses).Status = value;
            bDLClk.tim += value => (bsDisplay.SelectedItem as buses).Timer = new Timerclass(value);
            bDLClk.ShowDialog();
        }
        /// <summary>
        /// returns the index of the given id string, or -1 
        /// </summary>
        /// <param name="id">id of bus to find</param>
        /// <returns></returns>
        public int indexOf(string id)
        {
            int i = 0;
            foreach(buses bs in busPool)
            {
                if (bs.Id == id)
                    return i;
                i++;
            }
            return -1;
        }
        /// <summary>
        /// button to add new bus
        /// </summary>
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            addBusWindow adbw = new addBusWindow(sound);
            adbw.ShowDialog();
        }
        /// <summary>
        /// sends bus to a drive
        /// </summary>
        private void Button_SendDrive(object sender, RoutedEventArgs e)
        {
           
            var fxElt = sender as FrameworkElement;
            buses lineData = fxElt.DataContext as buses;
            if (lineData.Status != "ready")
            {
                MessageBox.Show("you cannot drive a bus unless its status is ready");
                return;
            }

            
            if (lineData == null)
                return;
            busDrive busDrive = new busDrive(ref fxElt,timer,sound);
            busDrive.tim += value => lineData.Timer = new Timerclass(value);
           
            busDrive.ShowDialog();
           
        }
        /// <summary>
        /// deketes bus from list
        /// </summary>
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            buses lineData = fxElt.DataContext as buses;
            if((lineData.Status!="ready")&&(lineData.Status != "dangerous"))
            {
                MessageBox.Show("you cannot delete a bus unless its status is ready");
                return;
            }
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete this bus?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                return;
            busPool.RemoveAt(indexOf(lineData.Id));
            bsDisplay.Items.Refresh();
        }

       /// <summary>
       /// <summary>
       /// syncs buslist to viewlist
       /// </summary>
            private void refreshingProgram(Object ob,EventArgs e)
            {          
                bool flag = false;
            
                foreach (buses bs in busPool)
                {
                
                 if (bs.Status == "refueling" && bs.Timer.TimeNow == "00:00:00")
                {
                    flag = true;
                    bs.Fuel = 1200;
                    if (bs.Dangerous == true)
                    {
                        bs.Status = "dangerous";
                        bs.IconPath = "/src/pics/warningIcon.png";
                    }
                    else
                    {
                        bs.Status = "ready";
                        bs.IconPath = "/src/pics/okIcon.png";
                    }
                    bsDisplay.Items.Refresh();
                    if (NoOperationExist())
                        timer.Stop();
                }
                if (bs.Status == "maintenance" && bs.Timer.TimeNow == "00:00:00")
                {
                    flag = true;
                    bs.LastMaintenance = DateTime.Now;
                    bs.Distance = 0;
                    bs.Status = "ready";
                    bs.IconPath = "/src/pics/okIcon.png";
                    if (bs.Dangerous == true)
                        bs.Dangerous = false;
                    bsDisplay.Items.Refresh();
                    if (NoOperationExist())
                        timer.Stop();
                }
                if (bs.Status == "mid-ride" && bs.Timer.TimeNow == "00:00:00")
                {
                    flag = true;                   
                    bs.Status = "ready";
                    bs.IconPath = "/src/pics/okIcon.png";
                    bsDisplay.Items.Refresh();
                    if (NoOperationExist())
                        timer.Stop();
                }
               
            }
            if (flag)
            {
                busSort();
                flag = false;
                bsDisplay.Items.Refresh();
            }
        }
       
        /// <summary>
        /// checks if there is a bus which is not ready
        /// </summary>
        /// <returns>true if there is a bus whos not ready, or false if not</returns>
        private bool NoOperationExist()
        {
            foreach(buses bs in busPool)
            {
                if ((bs.Status == "refueling") || (bs.Status == "maintenance") || (bs.Status == "mid-ride"))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// reads settings from file
        /// </summary>
        private void readSettings()
        {
            bool flag,save,alert,effects;
            string[] settings;
            try { settings = File.ReadAllLines($"{appPath}\\src\\storage\\settings.txt"); }
            catch (Exception e) { return; }
            flag = bool.TryParse(settings[0].Split('=')[1], out save);
            if (!flag)
                return;
            flag = bool.TryParse(settings[1].Split('=')[1], out effects);
            if (!flag)
                return;
            flag = bool.TryParse(settings[2].Split('=')[1], out alert);
            if (!flag)
                return;
            autosave = save;
            sound = effects;
            show= alert;
            if (save)
                btnautosave.IsChecked = true;
            else
                btnautosave.IsChecked = false;
            if (effects)
                btnsound.IsChecked = true;
            else
                btnsound.IsChecked = false;
            if (alert)
                btnsaveAlerts.IsChecked = true;
            else
                btnsaveAlerts.IsChecked = false;
        }
        /// <summary>
        /// writes settings to file
        /// </summary>
        private void writeSettings()
        {
            string[] settings = new string[3] { $"autosave={autosave}", $"soundEffects={sound}", $"showAlerts={show}" };
            try { File.WriteAllLines($"{appPath}\\src\\storage\\settings.txt", settings); }
            catch(Exception e) {  }
        }
        /// <summary>
        /// button used to save data to file
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            buses.save(busPool, $"{appPath}\\src\\storage\\DataFile.txt", show);
        }
        /// <summary>
        /// button used to load data from file
        /// </summary>

        private void btnLaod_Click(object sender, RoutedEventArgs e)
        {
            buses.load(ref busPool, $"{appPath}\\src\\storage\\DataFile.txt",show);
            bsDisplay.ItemsSource = busPool;
            bsDisplay.Items.Refresh();
          
        }
        /// <summary>
        /// select sort button
        /// </summary>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cbSort.SelectedItem;
            string value = typeItem.Content.ToString();
            if (value != "select sort")
            {
                busSort();
                bsDisplay.Items.Refresh();
            }
        }

        private delegate bool compare1(string x, string y);
       
        /// <summary>
        /// sorts bus list by paramter taken from combobox
        /// </summary>
        public void busSort()
        {            
            ComboBoxItem typeItem = (ComboBoxItem)cbSort.SelectedItem;
            string value = typeItem.Content.ToString();
            if (value == "ID")
            {
                busPool.Sort((x, y) => x.Id.CompareTo(y.Id));
                return;
            }
            
            int mode = 0;
            if (value == "Time")
                mode = 1;              
            if (value == "Status")
                mode = 2;
                
            for (int i = 0; i < busPool.Count(); i++)
            {
                for (int j = 0; j < busPool.Count() - 1; j++)
                {
                    if (mode == 1)
                        if (buses.sortTime(busPool[j].Timer.TimeNow, busPool[j+1].Timer.TimeNow))
                         {
                             buses tmp = busPool[j];
                             busPool[j] = busPool[j + 1];
                             busPool[j + 1] = tmp;
                        } 
                    if(mode==2)
                        if (buses.sortStatus(busPool[j].Status, busPool[j + 1].Status))
                        {
                            buses tmp = busPool[j];
                            busPool[j] = busPool[j + 1];
                            busPool[j + 1] = tmp;
                        }
                }
            }           
        }

        /// <summary>
        /// setting button save is checked
        /// </summary>
        private void btnautosave_Checked(object sender, RoutedEventArgs e)
        {
            autosave = true;
           
        }
        //setting button alert is checked
        private void btnsaveAlerts_Checked(object sender, RoutedEventArgs e)
        {
            show = true;
           
        }
        //setting button sound effects is checked
        private void btnsound_Checked(object sender, RoutedEventArgs e)
        {
            sound = true;
          
        }
        //setting button sound effects is unchecked
        private void btnsound_Unchecked(object sender, RoutedEventArgs e)
        {
            sound = false;
            
        }
        //setting button alert is unchecked
        private void btnsaveAlerts_Unchecked(object sender, RoutedEventArgs e)
        {
            show = false;
           
        }
        //setting button save is unchecked
        private void btnautosave_Unchecked(object sender, RoutedEventArgs e)
        {
            autosave = false;
           
        }
        //save settings (and data if autosave is enabled) to file(s) when main window closes
        private void Window_Closed(object sender, EventArgs e)
        {
            if(autosave)
                buses.save(busPool, $"{appPath}\\src\\storage\\DataFile.txt",show);
            writeSettings();
        }
        //deletes all data from file
        private void btnreset_Click(object sender, RoutedEventArgs e)
        {
            try {File.Create($"{appPath}\\src\\storage\\DataFile.txt");}
            catch(Exception exc) { return; }
            MessageBox.Show("data deleted, my lord");
        }
    }

}
