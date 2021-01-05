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
        int folStatIdSelect = 0;
        public managerView()
        {
            //bl.listToText();
            busOrder = 0;
            InitializeComponent();
            initSource();  
            
        }
        

        #region buses
        private void busesView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(true, false, 1);
            btnUpdate.Visibility = System.Windows.Visibility.Visible;
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
            int id;
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
                id = bus.id;
            }
            catch (Exception ecx) { MessageBox.Show(ecx.Message); return; }
            refreshBuses();
            int index = 0;
            foreach (var item in busesView.Items)
            {
                if ((item as BO.Bus).id ==id)
                    break;
                index++;
            }
            refreshBuses();
            busesView.SelectedIndex = index;
            initTextBoxes(false, false, 1);
            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
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
            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
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
            }

        }

       
        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            if (cbStations.SelectedItem == null)
                return;
            int id = 0;
            if (cbStations.Items.Count > 1)
            {
                id = (cbStations.SelectedItem as BO.busLineStation).id;
                if (cbStations.SelectedIndex == 0)
                    cbStations.SelectedIndex = 1;
                else
                {
                    cbStations.SelectedIndex = cbStations.SelectedIndex-1;
                }
            }
            else
            {
                id = (cbStations.SelectedItem as BO.busLineStation).id;
            }             
            bl.removeStation(id);
            initTextBoxByCbInStations();
            if (cbStations.SelectedItem!=null)
            {
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.busLineStation).id);
                lvStationOfLine.Items.Refresh();
                lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.busLine).id);
            }
            else
            {
                lvFollowStation.ItemsSource=null;
                lvLinesInStation.ItemsSource=null;
            }
        }

        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            tblError.Text = "";
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
                catch (Exception exc) {tblError.Text= exc.Message; return; }
                finally
                {
                    init_lvFollowStation_PreviewMouseDown();
                }
                string code = tbStationCode.Text;
                string address = tbStationAddress.Text;
                float latitude = float.Parse(tbStationLat.Text.ToString());
                float longitude = float.Parse(tbStationLong.Text.ToString());
                try { bl.addStation(new BO.busLineStation(code, latitude, longitude, address)); }
                catch (Exception exc) { tblError.Text = exc.Message; return; }
                finally { initTextBoxes(false, false, 1); initTextBoxByCbInStations(); }
                
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
            if (lvFollowStation.Items.Count == 0)
                return;
            folStatIdSelect = (lvFollowStation.SelectedItem as BO.busLineStation).id;
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
            tblError.Text = "";
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
                catch (Exception exc) { tblError.Text = exc.Message; return; }
                finally
                {
                    init_lvFollowStation_PreviewMouseDown();
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
                catch (Exception exc) { tblError.Text = exc.Message; return; }
            }
        }

        private void btnUpdateTimOrDis_Click(object sender, RoutedEventArgs e)
        {
            try { validINputDriveTimeOrDistance(); }
            catch (Exception exc) { tblError.Text = exc.Message; return;  }
            finally {
                init_lvFollowStation_PreviewMouseDown();
                initTextBoxByCbInStations();
            }           
            try
            {
                int idsta = 0,index=0;
                foreach (var item in lvFollowStation.Items)
                {
                    if ((item as BO.busLineStation).id == folStatIdSelect)
                    {
                        idsta = (item as BO.busLineStation).id;                       
                        break;
                    }
                    index++;
                }
                int idLine = 0, index2 = 0 ;
                foreach (var item in lvLinesInStation.Items)
                {
                    if (index2 == index)
                    {
                        idLine = (item as BO.busLine).id;
                        break;
                    }
                    index2++;
                }
                var folStation = new BO.followStations((cbStations.SelectedItem as BO.busLineStation).id, idsta,idLine, Convert.ToInt32(tbStationDistance.Text),TimeSpan.Parse(tbStationDriveTm.Text));
                folStation.id = bl.GetIdFollowStationBy((cbStations.SelectedItem as BO.busLineStation).id, idsta, idLine);
                bl.updateFollowStation(folStation);
                lvLinesInStation.ItemsSource= bl.GetAllLinesInStation((cbStations.SelectedItem as BO.busLineStation).id);
                initTextBoxByCbInStations();
            }
            catch (Exception exc) { tblError.Text = exc.Message; return; }
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
                    tbldriveTime.Clear();
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
            if (float.Parse(tbStationLong.Text) > 180 || float.Parse(tbStationLong.Text) < -180)
                throw new InvalidUserInputExecption("Invalid input: longitude must be an number between -180 to 180");

        }

        private void validINputDriveTimeOrDistance()
        {
            string fl = tbStationDriveTm.Text;
            string[] sd = fl.Split(':');
            int index1 = fl.IndexOf(':');
            fl = fl.Substring(index1 + 1);
            int index2 = fl.IndexOf(':');
            if (index1 != 2 || index2 != 2)
                throw new InvalidUserInputExecption("Invalid input: time drive field need to be in format of: HH:MM:SS");
            int val1 = 0, val2 = 0, val3 = 0;
            double dis = 0;
            // bool sucsses = int.TryParse(sd[0],out val);
            if (!int.TryParse(sd[0], out val1) || !int.TryParse(sd[1], out val2) || !int.TryParse(sd[2], out val3))
                throw new InvalidUserInputExecption("Invalid input: time drive field need to be in format of: HH:MM:SS");
            if (val1 > 99 || val1 < 0)
                throw new InvalidUserInputExecption("Invalid input: Hours field need to be 0-99");
            if (val2 > 59 || val2 < 0)
                throw new InvalidUserInputExecption("Invalid input: Minutes field need to be 0-59");
            if (val3 > 59 || val3 < 0)
                throw new InvalidUserInputExecption("Invalid input: Seconds field need to be 0-59");
            if (!double.TryParse(tbStationDistance.Text, out dis))
                throw new InvalidUserInputExecption("Invalid input: Distance field need to be a number.");
            if (dis < 0)
                throw new InvalidUserInputExecption("Invalid input: Distance field need to be a possitive number.");

        }
        #endregion

        #region lines
        private void addLine_click(object sender, RoutedEventArgs e)
        {
           addLine addWindow = new addLine();
            addWindow.ShowDialog();
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;
            initTextBoxByCbInStations();

         
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbBusLines.SelectedItem!=null)
            lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.busLine).id);
        }
        private void lvStationOfLine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void lvStationOfLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbBusLines.SelectedItem == null)
                return;

            try { bl.removeLine((cbBusLines.SelectedItem as BO.busLine).id); }
            catch (Exception exc) { MessageBox.Show(exc.Message); return; }
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;


        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbBusLines.SelectedItem == null)
                return;
            addLine addWindow = new addLine(1, (cbBusLines.SelectedItem as BO.busLine).id, (cbBusLines.SelectedItem as BO.busLine).number);
            addWindow.ShowDialog();
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;
        }

<<<<<<< HEAD:dotNet5781_3169_8515/PL/managerView.xaml.cs
     
=======
        #endregion


>>>>>>> 7fd1e0a18a6800e62f050eb8a2c3a2c9a21a036a:dotNet5781_3169_8515/PL.WPFSimple/managerView.xaml.cs
    }
}
