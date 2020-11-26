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

using dotNet5781_01_3169_8515;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      

        private static BindingList<buses> busPool = new BindingList<buses>();
        Random r = new Random();
        readonly string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
        const short FULL_TANK = 1200;
        private buses currentBus;
        
        public MainWindow()//add mini payer to menu
        {
            InitializeComponent();
            initBus();
            bsDisplay.ItemsSource = busPool;           
            showBuses(busPool[0].Id);
        }

        public BindingList<buses> BusPool
        {
            get => busPool;          
        }
        public void initBus()
        {
            for (int i = 0; i < 13; i++)
            {
                bool flag = true;
                string id = "";
                while (flag)
                {
                    id = r.Next(100000, 1000000).ToString();//make sure id format matches MD 
                    flag = false;
                    foreach (var bus in busPool)
                        if (id == bus.Id)//need to change accessers
                        {
                            flag = true;
                            break;
                        }
                }
                //updatedanr
                DateTime rd = randomDate();
                DateTime lastM = randomDate(1);
                busPool.Add(new buses(rd, lastM, id, r.Next(0, FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000),randomStatus(r.Next(0,1))));
            }
            //set 3 buses to match requirments
            busPool[0].LastMaintenance=new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            busPool[1].Distance=19999;
            busPool[2].Fuel = 0;
        }

        private string randomStatus(int i)
        {
            switch (i)
            {
                case 0: return "ready";
                case 1: return "mid-ride";
                case 2: return "refueling";
                default: return "in maintenance";
            }
        }
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
        private void showBuses(string id)
        {
            currentBus = busPool[find(id)];
            busesgrid.DataContext = currentBus;
            bsDisplay.DataContext = currentBus.ToString();
        }
        private int find(string id)
        {
            int i = 0;
            foreach (var bus in busPool)
            {
                if (bus.Id == id)
                    return i;
                i++;
            }
            throw new Exception("no Match");//change
        }

        private void bsDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((bsDisplay.SelectedItem as buses) == null)
                return;
            busDetailsByDoubleClick bDLClk = new busDetailsByDoubleClick();
            string st= (bsDisplay.SelectedItem as buses).Id;
            int fuel = (bsDisplay.SelectedItem as buses).Fuel;
            DateTime lmaintenance = (bsDisplay.SelectedItem as buses).LastMaintenance;

             

            bDLClk.labStatus.Content= (bsDisplay.SelectedItem as buses).Status;
            bDLClk.labNameBus.Content = "Bus Id: " + st;
            bDLClk.labfuel.Content = fuel.ToString();
            bDLClk.labDistance.Content = (bsDisplay.SelectedItem as buses).Distance.ToString();
            bDLClk.labtotalDist.Content = (bsDisplay.SelectedItem as buses).TotalDistance.ToString(); 
            if ((bsDisplay.SelectedItem as buses).Dangerous)
                st = "dangerous";
            else
                st = "not dengerous";
             bDLClk.labDangerous.Content =st ;
            bDLClk.labLMaintenance.Content = lmaintenance.ToString();


            bDLClk.fuel1 += value => (bsDisplay.SelectedItem as buses).Fuel=value;
            bDLClk.lmaintenance += value=> (bsDisplay.SelectedItem as buses).LastMaintenance=value;
            bDLClk.lmaintenance += value => (bsDisplay.SelectedItem as buses).Distance =0;
            bDLClk.status1 += value => (bsDisplay.SelectedItem as buses).Status = value;

            bDLClk.ShowDialog();
        }

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

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_SendDrive(object sender, RoutedEventArgs e)
        {
            
           var fxElt = sender as FrameworkElement;
            buses lineData = fxElt.DataContext as buses;         
            MessageBox.Show(lineData.Id);
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            var fxElt = sender as FrameworkElement;
            buses lineData = fxElt.DataContext as buses;
            busPool.RemoveAt(indexOf(lineData.Id));
        }

    }

}
