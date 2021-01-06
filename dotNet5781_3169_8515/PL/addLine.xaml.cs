using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for addLine.xaml
    /// </summary>
    public partial class addLine : Window
    {
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        int mode,lineId;
        List<BO.BusLineStation> fList;
        List<BO.BusLineStation> tList;
        List<TimeSpan> time=new List<TimeSpan>();
        List<double> distance=new List<double>();
        public addLine(int mode=0,int lineId=-1,string number="")
        {
            this.mode = mode;
            this.lineId = lineId;
            InitializeComponent();
            if(mode==0)
            {
                fList = bl.GetAllbusLineStation().ToList();
                tList = new List<BO.BusLineStation>();
            }
            else
            {
                tList = bl.GetAllStationInLine(lineId).ToList();
                fList = bl.GetAllStationNotInLine(lineId).ToList();
                bl.reconstructTimeAndDistance(lineId,out distance,out time);
                txbLineNumber.Text = number;
                txbLineNumber.IsEnabled = false;

            }
            lvfrom.ItemsSource = fList;
            lvto.ItemsSource = tList;
        }

        private void lvfrom_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            if (lvfrom.SelectedItem == null)
                return;
            int id = (lvfrom.SelectedItem as BO.BusLineStation).id;
            foreach(var station in fList)
                if(station.id==id)
                {
                    tList.Add(station);
                    fList.Remove(station);
                    break;
                }
            //addLineUserPromt promt = new addLineUserPromt(distance,time);
            // if(tList.Count!=1)
            //  promt.ShowDialog();
            if (tList.Count == 1)
            {
                refresh();
                return;
            }
            var sCoord = new GeoCoordinate(tList[tList.Count-2].Latitude, tList[tList.Count - 2].Longitude);
            var eCoord = new GeoCoordinate((lvfrom.SelectedItem as BO.BusLineStation).Latitude, (lvfrom.SelectedItem as BO.BusLineStation).Longitude);
            
            if(sCoord.GetDistanceTo(eCoord)<1000)
                distance.Add((int)sCoord.GetDistanceTo(eCoord));
            else
                distance.Add((float)(sCoord.GetDistanceTo(eCoord)/1000));
            Random r =new Random();
            TimeSpan ts = TimeSpan.FromHours((sCoord.GetDistanceTo(eCoord)/1000) / ((r.Next(30, 60))));
            time.Add(new TimeSpan((int)ts.Hours,(int)ts.Minutes,(int)ts.Seconds));   
            refresh();
        }

        private void lvto_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            if (lvto.SelectedItem == null)
                return;
            int id = (lvto.SelectedItem as BO.BusLineStation).id;
            int index = lvto.SelectedIndex;
            foreach (var station in tList)
                if (station.id == id)
                {
                    fList.Add(station);
                    tList.Remove(station);
                    break;
                }
            var temp1 = time[index-1];
            time.Remove(temp1);
            var temp2 = distance[index-1];
            distance.Remove(temp2);
            refresh();
        }
        private  void refresh()
        {
            lvfrom.Items.Refresh();
            lvfrom.UnselectAll();
            lvto.Items.Refresh();
            lvto.UnselectAll();
        }
        private  void validateInput()
        {
            foreach (var chr in txbLineNumber.Text)
                if (chr < '0' || chr > '9')
                    throw new InvalidUserInputExecption("Line number must be an integer");
            if(txbLineNumber.Text.Length>3)
                throw new InvalidUserInputExecption("Line number cannot be more than 3 digits longs");
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try { validateInput(); }
            catch (Exception exc) { lblError.Content = exc.Message; return; }
            if (mode==0)
            { 
               try {  bl.addLine(txbLineNumber.Text, cmbarea.SelectedIndex, tList,distance,time);}
               catch (Exception exc) { lblError.Content = exc.Message; return; }
             
            }
            else
            {
                try { bl.updateLine(lineId,txbLineNumber.Text, cmbarea.SelectedIndex, tList, distance, time); }
                catch (Exception exc) { lblError.Content = exc.Message; return; }

            }
            tList.Clear();
            fList.Clear();
            time.Clear();
            distance.Clear();
            this.Close();
        }
    }
}
