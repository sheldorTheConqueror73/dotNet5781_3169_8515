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
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for managerView.xaml
    /// </summary>
    public partial class managerView : Window
    {

        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        public managerView()
        { 
            InitializeComponent();
            initSource();
            
        }
        private void initSource()
        {
            dplmiDate.DisplayDateEnd = DateTime.Now;
            dpRegiDate.DisplayDateEnd = DateTime.Now;
            tbiBuses.DataContext = bl.GetAllBuses();
            busesView.SelectedIndex = 0;
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.SelectedIndex = 0;
            lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
            lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.SelectedIndex = 0;
            lvStationOfLine.ItemsSource=bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.busLine).id);
        }
        private void busesView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(true,false,1);
        }
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddBus.Content.Equals("Add"))
            {
                btnAddBus.Content = "Submit";
                initTextBoxes(true, true,1);
                lbDanger.Visibility = System.Windows.Visibility.Hidden;
                tbDangerous.Visibility= System.Windows.Visibility.Hidden;
            }
            else
            {
                //------------------------------------------------------------------------------------------fix insert
                int fuel, dist, totalDIst;
                try { validateInput(out fuel, out dist, out totalDIst); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                string plateNumber = tbid.Text;
                DateTime rd = dpRegiDate.SelectedDate.Value;
                DateTime lm = dplmiDate.SelectedDate.Value;
                try { bl.addBus(new BO.Bus(rd,lm, plateNumber,fuel,dist,false,totalDIst,"ready"));  }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally {      initTextBoxes(false, false,1);  }
                refreshBuses();
                btnAddBus.Content = "Add";
                lbDanger.Visibility = System.Windows.Visibility.Visible;
                tbDangerous.Visibility = System.Windows.Visibility.Visible;
            }

        }
       

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int fuel, dist, totalDIst;
            try { validateInput(out fuel,out dist, out totalDIst); }
            catch (Exception exc) { MessageBox.Show(exc.Message); return; }
            finally
            {
                tbid.IsEnabled = true;
                tbfuel.IsEnabled = true;
                tbDistance.IsEnabled = true;
                tbtotalDist.IsEnabled = true;
                busesView.Items.Refresh();
            }
            if(busesView.SelectedItem==null)
            {
                MessageBox.Show("You can not Update an empty list");
                return;
            }
            try {
                var bus = new BO.Bus(dpRegiDate.SelectedDate.Value, dplmiDate.SelectedDate.Value, tbid.Text, fuel, dist, tbDangerous.Text == "YES" ? true : false, totalDIst, (busesView.SelectedItem as BO.Bus).status);
                bus.id = (busesView.SelectedItem as BO.Bus).id;
            bl.updateBus(bus);
            }
            catch (Exception ecx) { MessageBox.Show(ecx.Message); return; }
            
        }
        private void validateInput (out int fuel, out int distance, out int totaldistance)
        {
            bool flag;
            tbid.IsEnabled = false;
            if ((tbid.Text == null) || (tbid.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: Id field cannot be empty");
            string temp = "";
            foreach (char element in tbid.Text)
                if (element != '-')
                    temp += element;
            if ((temp.Length != 8) && (temp.Length != 7))
                throw new InvalidUserInputExecption("Invalid input: id must be 7 or 8 digits");
            foreach (char latter in tbid.Text)
            {
                if (((latter > '9') || (latter < '0'))&&(latter!='-'))
                    throw new InvalidUserInputExecption("Invalid input: id must be an integer");
            }
            if ((tbid.Text.Length == 8 && ((DateTime)dpRegiDate.SelectedDate).Year < 2018) || (tbid.Text.Length == 7 && ((DateTime)dpRegiDate.SelectedDate).Year >= 2018))
                throw new InvalidUserInputExecption("Invalid input: id format doesn't match registration date");
             tbfuel.IsEnabled = false;
            if (( tbfuel.Text == null) || ( tbfuel.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: fuel field cannot be empty");
            flag = int.TryParse( tbfuel.Text, out fuel);
            if (!flag)
                throw new InvalidUserInputExecption("Invalid input: fuel must be an integer");
            if ((fuel > 1200) || (fuel < 0))
                throw new InvalidUserInputExecption("Invalid input: fuel must be within the range of 0 to 1200");
            tbDistance.IsEnabled = false;
            if ((tbDistance.Text == null) || (tbDistance.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: distance field cannot be empty");
            flag = int.TryParse(tbDistance.Text, out distance);
            if (!flag)
                throw new InvalidUserInputExecption("Invalid input: distance since last maintenance must be an integer");
            if ((distance > 20000) || (distance < 0))
                throw new InvalidUserInputExecption("Invalid input: distance since last maintenance must be within the range of 0 to 20000");
            tbtotalDist.IsEnabled = false;
            if ((tbtotalDist.Text == null) || (tbtotalDist.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: total distance field cannot be empty");
            flag = int.TryParse(tbtotalDist.Text, out totaldistance);
            if (!flag)
                throw new InvalidUserInputExecption("Invalid input: total distance must be an integer");
            if (totaldistance < 0)
                throw new InvalidUserInputExecption("Invalid input: total distance must not be lesser than 0");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.removeBus(id);
            refreshBuses();
            initTextBoxes(false, true,1);
        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStations.SelectedItem != null && cbStations.SelectedItem!= null)
            {
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
            }
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.busLine).id);
        }
        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            int id = (cbStations.SelectedItem as BO.busLineStation).id;
            if (id == 0)
                cbStations.SelectedIndex = 1;
            else
                cbStations.SelectedIndex = id - 1;
            bl.removeStation(id);
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.SelectedIndex = 0;
            cbStations.Items.Refresh();
            lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
            lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
        }
        
         private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void lvFollowStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbStationDriveTm.IsEnabled = true;
            tbStationDistance.IsEnabled = true;
            initTextBoxes(false, false, 3);
        }
        private void lvFollowStation_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            tbStationDriveTm.IsEnabled = false;
            tbStationDistance.IsEnabled = false;
            initTextBoxes(true, false, 3);
        }

        #region utility

        private void initTextBoxes(bool flagEnabled, bool flagContent, int tabItem)
        {
            if (tabItem == 1)
            {
                if (flagEnabled)
                {
                    tbid.IsEnabled = true;
                    tbfuel.IsEnabled = true;
                    tbDistance.IsEnabled = true;
                    tbtotalDist.IsEnabled = true;
                    dpRegiDate.IsEnabled = true;
                    dplmiDate.IsEnabled = true;
                    tbDangerous.IsEnabled = false;
                }
                else
                {
                    tbid.IsEnabled = false;
                    tbfuel.IsEnabled = false;
                    tbDistance.IsEnabled = false;
                    tbtotalDist.IsEnabled = false;
                    dpRegiDate.IsEnabled = false;
                    dplmiDate.IsEnabled = false;
                    tbDangerous.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbid.Text = "";
                    tbfuel.Text = "";
                    tbDistance.Text = "";
                    tbtotalDist.Text = "";
                    dpRegiDate.Text = DateTime.Now.ToString();
                    dplmiDate.Text = DateTime.Now.ToString();
                    tbDangerous.Text = "";//----------------------------------------------------------------------------------------------fix denger bindning
                }
            }
            else if (tabItem == 2)
            {
                if (flagEnabled)
                {
                    tbLineNumber.IsEnabled = true;
                    tbLineArea.IsEnabled = true;
                    cbLineFirstSta.IsEnabled = true;
                    cbLineLastSta.IsEnabled = true;
                }
                else
                {
                    tbLineNumber.IsEnabled = false;
                    tbLineArea.IsEnabled = false;
                    cbLineFirstSta.IsEnabled = false;
                    cbLineLastSta.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbLineNumber.Text = "";
                    tbLineArea.Text = "";
                    cbLineFirstSta.Text = "";
                    cbLineLastSta.Text = "";
                }
            }
            else
            {
                if (flagEnabled)
                {
                    tbStationCode.IsEnabled = true;
                    tbStationAddress.IsEnabled = true;
                    tbStationLat.IsEnabled = true;
                    tbStationLong.IsEnabled = true;
                }
                else
                {
                    tbStationCode.IsEnabled = false;
                    tbStationAddress.IsEnabled = false;
                    tbStationLat.IsEnabled = false;
                    tbStationLong.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbStationCode.Text = "";
                    tbStationAddress.Text = "";
                    tbStationLat.Text = "";
                    tbStationLong.Text = "";
                }
            }
        }

        #endregion

        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.refuel(lineData.id);
            refreshBuses();
            busesView.SelectedIndex = 0;//need for speed
        }
        private void refreshBuses()
        {
            tbiBuses.DataContext = bl.GetAllBuses();
            busesView.Items.Refresh();
        }

        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.maintain(lineData.id);
            refreshBuses();
        }
    }
}
