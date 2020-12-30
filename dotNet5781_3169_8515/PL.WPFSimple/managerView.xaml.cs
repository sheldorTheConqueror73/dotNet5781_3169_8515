using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        int busOrder;
        public managerView()
        {
            busOrder = 0;
            InitializeComponent();
            initSource();
            
        }
        

        #region buses
        private void busesView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(true, false, 1);
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddBus.Content.Equals("Add"))
            {
                btnAddBus.Content = "Submit";
                initTextBoxes(true, true, 1);
                lbDanger.Visibility = System.Windows.Visibility.Hidden;
                tbDangerous.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                int fuel, dist, totalDIst;
                try { validateInput(out fuel, out dist, out totalDIst); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                string plateNumber = tbid.Text;
                DateTime rd = dpRegiDate.SelectedDate.Value;
                DateTime lm = dplmiDate.SelectedDate.Value;
                try { bl.addBus(new BO.Bus(rd, lm, plateNumber, fuel, dist, false, totalDIst, "ready")); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally { initTextBoxes(false, false, 1); }
                refreshBuses();
                btnAddBus.Content = "Add";
                lbDanger.Visibility = System.Windows.Visibility.Visible;
                tbDangerous.Visibility = System.Windows.Visibility.Visible;
            }

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int fuel, dist, totalDIst;
            try { validateInput(out fuel, out dist, out totalDIst); }
            catch (Exception exc) { MessageBox.Show(exc.Message); return; }
            finally
            {
                tbid.IsEnabled = true;
                tbfuel.IsEnabled = true;
                tbDistance.IsEnabled = true;
                tbtotalDist.IsEnabled = true;
                busesView.Items.Refresh();
            }
            if (busesView.SelectedItem == null)
            {
                MessageBox.Show("You can not Update an empty list");
                return;
            }
            try
            {
                var bus = new BO.Bus(dpRegiDate.SelectedDate.Value, dplmiDate.SelectedDate.Value, tbid.Text, fuel, dist, tbDangerous.Text == "YES" ? true : false, totalDIst, (busesView.SelectedItem as BO.Bus).status);
                bus.id = (busesView.SelectedItem as BO.Bus).id;
                bl.updateBus(bus);
            }
            catch (Exception ecx) { MessageBox.Show(ecx.Message); return; }

        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.removeBus(id);
            refreshBuses();
            initTextBoxes(false, true, 1);
        }


        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.refuel(lineData.id);
            int index = 0;
            foreach (var item in busesView.Items)
            {
                if ((item as BO.Bus).id == id)
                    break;
                index++;
            }
            refreshBuses();
            busesView.SelectedIndex = index;
        }
        private void refreshBuses()
        {
            tbiBuses.DataContext = bl.GetAllBuses(busOrder);
            busesView.Items.Refresh();
        }

        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.maintain(lineData.id);
            int index = 0;
            foreach (var item in busesView.Items)
            {
                if ((item as BO.Bus).id == id)
                    break;
                index++;
            }
            refreshBuses();
            busesView.SelectedIndex = index;

        }


        private void busesView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(false, false, 1);
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            busOrder = cbSort.SelectedIndex;
            refreshBuses();
        }
        #endregion


        #region stations
        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStations.SelectedItem != null && cbStations.SelectedItem != null)
            {
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
                //init_lvFollowStation_PreviewMouseDown();
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
            initTextBoxByCbInStations();
            lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
            lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
        }

        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddStation.Content.Equals("Add"))
            {
                btnAddStation.Content = "Submit";
                btnUpdateStation.Visibility = Visibility.Hidden;
                btnDeleteStation.Visibility = Visibility.Hidden;
                initTextBoxes(true, true, 3);

            }
            else
            {
                try { validStationInput(); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally
                {
                    init_lvFollowStation_PreviewMouseDown();
                }
                string code = tbStationCode.Text;
                string address = tbStationAddress.Text;
                float latitude = float.Parse(tbStationLat.Text.ToString());
                float longitude = float.Parse(tbStationLong.Text.ToString());
                try { bl.addStation(new BO.busLineStation(code, latitude, longitude, address)); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally { initTextBoxes(false, false, 1); }
                initTextBoxByCbInStations();
                btnAddBus.Content = "Add";
                tbStationDriveTm.Visibility = Visibility.Visible;
                tbStationDistance.Visibility = Visibility.Visible;
            }
        }

        private void tbStationCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void lvFollowStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbStationDriveTm.IsEnabled = true;
            tbStationDistance.IsEnabled = true;
            btnUpdateTimOrDis.Visibility = Visibility.Visible;
            btnAddStation.Visibility = Visibility.Collapsed;
            btnUpdateStation.Visibility = Visibility.Collapsed;
            btnDeleteStation.Visibility = Visibility.Collapsed;
            initTextBoxes(false, false, 3);
        }
        private void lvFollowStation_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            init_lvFollowStation_PreviewMouseDown();
        }
        private void init_lvFollowStation_PreviewMouseDown()
        {
            tbStationDriveTm.IsEnabled = false;
            tbStationDistance.IsEnabled = false;
            btnUpdateTimOrDis.Visibility = Visibility.Collapsed;
            btnAddStation.Visibility = Visibility.Visible;
            btnUpdateStation.Visibility = Visibility.Visible;
            btnDeleteStation.Visibility = Visibility.Visible;
            btnUpdateStation.Content = "Update";
            btnAddStation.Content = "Add";
            initTextBoxes(false, false, 3);
            //initTextBoxByCbInStations();
        }
        private void btnUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            if (btnUpdateStation.Content.Equals("Update"))
            {
                btnAddStation.Visibility = Visibility.Hidden;
                btnUpdateStation.Content = "Submit";
                btnDeleteStation.Visibility = Visibility.Hidden;
                initTextBoxes(true, false, 3);
            }
            else
            {
                try { validStationInput(); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally
                {
                    init_lvFollowStation_PreviewMouseDown();
                    initTextBoxByCbInStations();
                }
                if (cbStations.SelectedItem == null)
                {
                    MessageBox.Show("You can not Update an empty list");
                    return;
                }
                try
                {
                    var station = new BO.busLineStation(tbStationCode.Text, float.Parse(tbStationLat.Text), float.Parse(tbStationLong.Text), tbStationAddress.Text);
                    station.id = (cbStations.SelectedItem as BO.busLineStation).id;
                    bl.updateStation(station);
                    initTextBoxByCbInStations();
                }
                catch (Exception ecx) { MessageBox.Show(ecx.Message); return; }
            }
        }
        #endregion
       
        
        #region utility
        private void initSource()
        {
            dplmiDate.DisplayDateEnd = DateTime.Now;
            dpRegiDate.DisplayDateEnd = DateTime.Now;
            tbiBuses.DataContext = bl.GetAllBuses(busOrder);
            busesView.SelectedIndex = 0;
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.SelectedIndex = 0;
            lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
            lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.SelectedIndex = 0;
            lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.busLine).id);
        }
        private void initTextBoxes(bool flagEnabled, bool flagContent, int tabItem)
        {
            if (tabItem == 1)//buses
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
                    tbid.Clear();
                    tbfuel.Clear();
                    tbDistance.Clear();
                    tbtotalDist.Clear();
                    dpRegiDate.Text = DateTime.Now.ToString();
                    dplmiDate.Text = DateTime.Now.ToString();
                    tbDangerous.Clear();//----------------------------------------------------------------------------------------------fix denger bindning
                }
            }
            else if (tabItem == 2)//lines
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
                    tbLineNumber.Clear();
                    tbLineArea.Clear();
                    cbLineFirstSta.SelectedIndex=0;
                    cbLineLastSta.SelectedIndex=0;
                }
            }
            else//stations
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
                    tbStationCode.Clear();
                    tbStationAddress.Clear();
                    tbStationLat.Clear();
                    tbStationLong.Clear();
                }
            }
        }
        private void initTextBoxByCbInStations()
        {
            int index = 0;
            foreach (var item in cbStations.Items)
            {
                if ((item as BO.busLineStation).id == (cbStations.SelectedItem as BO.busLineStation).id)
                    break;
                index++;
            }
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.Items.Refresh();
            cbStations.SelectedIndex = index;
        }
        private void validateInput(out int fuel, out int distance, out int totaldistance)
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
                if (((latter > '9') || (latter < '0')) && (latter != '-'))
                    throw new InvalidUserInputExecption("Invalid input: id must be an integer");
            }
            if ((tbid.Text.Length == 8 && ((DateTime)dpRegiDate.SelectedDate).Year < 2018) || (tbid.Text.Length == 7 && ((DateTime)dpRegiDate.SelectedDate).Year >= 2018))
                throw new InvalidUserInputExecption("Invalid input: id format doesn't match registration date");
            tbfuel.IsEnabled = false;
            if ((tbfuel.Text == null) || (tbfuel.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: fuel field cannot be empty");
            flag = int.TryParse(tbfuel.Text, out fuel);
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


        private void validStationInput()//valid input check for stations
        {
            if ((tbStationCode.Text == null) || (tbStationCode.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: code field cannot be empty");
            if (tbStationCode.Text.Length > 6)
                throw new InvalidUserInputExecption("Invalid input: code must be up-to 6 digits");
            foreach (char latter in tbStationLat.Text)
            {
                if (((latter > '9') || (latter < '0')) && (latter != '-') && (latter != '.'))
                    throw new InvalidUserInputExecption("Invalid input: latitude must be an number");
            }
            int index = tbStationLat.Text.IndexOf('-');
            if (index != -1 && index != 0)
                throw new InvalidUserInputExecption("Invalid input: latitude must be an number");
            foreach (char latter in tbStationLong.Text)
            {
                if (((latter > '9') || (latter < '0')) && (latter != '-') && (latter != '.'))
                    throw new InvalidUserInputExecption("Invalid input: longitude must be an number");
            }
            index = tbStationLat.Text.IndexOf('-');
            if (index != -1 && index != 0)
                throw new InvalidUserInputExecption("Invalid input: latitude must be an number");
            if (float.Parse(tbStationLat.Text) > 90 || float.Parse(tbStationLat.Text) < -90)
                throw new InvalidUserInputExecption("Invalid input: latitude must be an number between -90 to 90");
            if (float.Parse(tbStationLong.Text) > 90 || float.Parse(tbStationLong.Text) < -90)
                throw new InvalidUserInputExecption("Invalid input: longitude must be an number between -180 to 180");

        }
        #endregion

       
        
    }
}
