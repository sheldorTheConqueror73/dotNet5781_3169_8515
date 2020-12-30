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
        managerView manager;
        List<BO.busLineStation> fList;
        List<BO.busLineStation> tList;
        public addLine(managerView managerWindow)
        {
             manager = managerWindow;
            Closing += windowClose;
            InitializeComponent();
            fList = bl.GetAllbusLineStation().ToList();
            tList = new List<BO.busLineStation>();
            lvfrom.ItemsSource = fList;
            lvto.ItemsSource = tList;
        }
        public  void windowClose(object sender, CancelEventArgs e)
        {
            manager.Show();
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
            refresh();
        }

        private void lvto_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            if (lvto.SelectedItem == null)
                return;
            int id = (lvto.SelectedItem as BO.busLineStation).id;
            foreach (var station in tList)
                if (station.id == id)
                {
                    fList.Add(station);
                    tList.Remove(station);
                    break;
                }
            refresh();
        }
        private  void refresh()
        {
            lvfrom.Items.Refresh();
            lvfrom.UnselectAll();
            lvto.Items.Refresh();
            lvto.UnselectAll();
        }
        private static void validateInput()
        {
            throw new NotImplementedException();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            validateInput();
            int[] distance= new int[tList.Count-1];
            TimeSpan[] time = new TimeSpan[tList.Count - 1];
            bl.addLine(txbLineNumber.Text, cmbarea.SelectedIndex, fList,distance,time);
        }
    }
}
