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
using dotNet5781_01_3169_8515;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<buses> busPool = new List<buses>();
        Random r = new Random();
        readonly string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
        const short FULL_TANK = 1200;
        private buses currentBus;
        public MainWindow()//add mini payer to menu
        {
            InitializeComponent();
            initBus();
            bsDisplay.ItemsSource = busPool;
            showBuses(busPool[0].getId());
        }

        public List<buses> BusPool
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
                        if (id == bus.getId())//need to change accessers
                        {
                            flag = true;
                            break;
                        }
                }
                //updatedanr
                DateTime rd = randomDate();
                DateTime lastM = randomDate(1);
                busPool.Add(new buses(rd, lastM, id, r.Next(0, FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000),randomStatus(r.Next(0,4))));
            }
            //set 3 buses to match requirments
            busPool[0].setLastMaintenance(new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day));
            busPool[1].setDistance(19999);
            busPool[2].setFuel(0);
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
                if (bus.getId() == id)
                    return i;
                i++;
            }
            throw new Exception("no Match");//change
        }

        private void bsDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string st= (bsDisplay.SelectedItem as buses).getId();
            int fuel = (bsDisplay.SelectedItem as buses).getFuel();
            DateTime lmaintenance = (bsDisplay.SelectedItem as buses).getLastMaintenance();
            busDetailsByDoubleClick bDLClk = new busDetailsByDoubleClick();
            bDLClk.labNameBus.Content = "Bus Id: " + st;
            bDLClk.labfuel.Content = fuel.ToString();
            bDLClk.labDistance.Content = (bsDisplay.SelectedItem as buses).getDistance().ToString();
            bDLClk.labtotalDist.Content = (bsDisplay.SelectedItem as buses).getTotalDistance().ToString(); ;
            if ((bsDisplay.SelectedItem as buses).getDangerous())
                st = "dangerous";
            else
                st = "not dengerous";
             bDLClk.labDangerous.Content =st ;
            bDLClk.labLMaintenance.Content = lmaintenance.ToString();


            bDLClk.fuel1 += value => (bsDisplay.SelectedItem as buses).setFuel(1200);
            bDLClk.Show();
        }

        public int indexOf(string id)
        {
            int i = 0;
            foreach(buses bs in busPool)
            {
                if (bs.getId() == id)
                    return i;
                i++;
            }
            return -1;
        }

    }

}
