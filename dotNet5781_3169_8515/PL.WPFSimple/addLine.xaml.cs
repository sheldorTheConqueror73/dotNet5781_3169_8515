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
        int mode;
        List<BO.busLineStation> fList;
        List<BO.busLineStation> tList;
        List<TimeSpan> time=new List<TimeSpan>();
        List<int> distance=new List<int>();
        public addLine(int mode=0)
        {
            this.mode = mode;
            InitializeComponent();
            fList = bl.GetAllbusLineStation().ToList();
            tList = new List<BO.busLineStation>();
            lvfrom.ItemsSource = fList;
            lvto.ItemsSource = tList;
        }

        private void lvfrom_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            if (lvfrom.SelectedItem == null)
                return;
            int id = (lvfrom.SelectedItem as BO.busLineStation).id;
            foreach(var station in fList)
                if(station.id==id)
                {
                    tList.Add(station);
                    fList.Remove(station);
                    break;
                }
            addLineUserPromt promt = new addLineUserPromt(distance,time);
            if(tList.Count!=1)
                promt.ShowDialog();
            refresh();
        }

        private void lvto_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            if (lvto.SelectedItem == null)
                return;
            int id = (lvto.SelectedItem as BO.busLineStation).id;
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
            if(mode==0)
            { 
              try { validateInput(); }
               catch (Exception exc) { lblError.Content = exc.Message; return; }
               try {  bl.addLine(txbLineNumber.Text, cmbarea.SelectedIndex, tList,distance,time);}
               catch (Exception exc) { lblError.Content = exc.Message; return; }
                tList.Clear();
                fList.Clear();
                time.Clear();
                distance.Clear();
               this.Close();
            }
            else
            {


            }
        }
    }
}
