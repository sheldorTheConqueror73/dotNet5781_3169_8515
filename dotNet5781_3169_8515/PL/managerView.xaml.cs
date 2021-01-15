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
            busOrder = 0;
            InitializeComponent();
            initSource();


        }


        #region buses

        /// <summary>
        /// enables editing mode to update bus properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void busesView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(true, false, 1);
            btnUpdate.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// calls BL.addbus() to add new bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                finally { initTextBoxes(true, false, 1); }
                string plateNumber = tbId.Text;
                DateTime rd = dpRegiDate.SelectedDate.Value;
                DateTime lm = dpLastMaintenance.SelectedDate.Value;
                try { bl.addBus(new BO.Bus() { registrationDate = rd, lastMaintenance = lm, plateNumber = plateNumber, fuel = fuel, distance = dist, dangerous = false, totalDistance = totalDIst, status = "ready"}); }
                catch (Exception exc) { MessageBox.Show(exc.Message); return; }
                finally { initTextBoxes(true, false, 1); }// disable all textboces anyway
                refreshBuses();
                btnAddBus.Content = "Add";
                lbDanger.Visibility = System.Windows.Visibility.Visible;
                tbDangerous.Visibility = System.Windows.Visibility.Visible;
            }

        }

        /// <summary>
        /// calls BL.updateBus() to update bus 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int fuel, dist, totalDIst;
            try { validateInput(out fuel, out dist, out totalDIst); }//validate user input
            catch (Exception exc) { MessageBox.Show(exc.Message); return; }
            finally
            {
                tbId.IsEnabled = true;
                tbFuel.IsEnabled = true;
                tbDistance.IsEnabled = true;
                tbTotalDist.IsEnabled = true;
                lvBuses.Items.Refresh();
                initTextBoxes(true, false, 1);
            }
            if (lvBuses.SelectedItem == null)
            {
                MessageBox.Show("You can not Update an empty list");
                return;
            }
            try
            {
                var bus = new BO.Bus() {registrationDate= dpRegiDate.SelectedDate.Value,lastMaintenance= dpLastMaintenance.SelectedDate.Value,plateNumber= tbId.Text,fuel= fuel,distance= dist,dangerous= tbDangerous.Text == "YES" ? true : false,totalDistance= totalDIst,status= (lvBuses.SelectedItem as BO.Bus).status };
                bus.id = (lvBuses.SelectedItem as BO.Bus).id;
                bl.updateBus(bus);//calls update function

                id = bus.id;
            }
            catch (Exception ecx) { MessageBox.Show(ecx.Message); return; }
            refreshBuses();
            int index = 0;
            foreach (var item in lvBuses.Items)//finds which index the selected item is in the listView
            {
                if ((item as BO.Bus).id ==id)
                    break;
                index++;
            }
            refreshBuses();
            lvBuses.SelectedIndex = index;
            initTextBoxes(false, false, 1);
            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
        }


        /// <summary>
        /// calls the delete function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;//find the selced line in listView
            int id = lineData.id;
            bl.removeBus(id);//remove bus function
            refreshBuses();
            initTextBoxes(false, true, 1);
        }

        /// <summary>
        /// refules the bus the mouse is hovering over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.refuel(lineData.id);
            int index = 0;
            foreach (var item in lvBuses.Items)// finds which index the selected item is in the listView
            {
                if ((item as BO.Bus).id == id)
                    break;
                index++;
            }
            refreshBuses();
            lvBuses.SelectedIndex = index;
        }
        /// <summary>
        /// updates the listview display
        /// </summary>
        private void refreshBuses()
        {
            tbiBuses.DataContext = bl.GetAllBuses(busOrder);
            lvBuses.Items.Refresh();
        }

        /// <summary>
        /// preforms Maintenance on the bus the mouse is hovering over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            var lineData = fxElt.DataContext as BO.Bus;
            int id = lineData.id;
            bl.maintain(lineData.id);
            int index = 0;
            foreach (var item in lvBuses.Items)// finds which index the selected item is in the listView
            {
                if ((item as BO.Bus).id == id)
                    break;
                index++;
            }
            refreshBuses();
            lvBuses.SelectedIndex = index;

        }

        /// <summary>
        /// when user clicks away from an an entry disable al textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void busesView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(false, false, 1);
            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// when user selects anthoer sorting method refresh listview 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            busOrder = cbSort.SelectedIndex;
            refreshBuses();
            lvBuses.SelectedIndex = 0;
        }
        #endregion


        #region stations
        /// <summary>
        /// when user selects station the list views of follow station and lines are updateing 
        /// </summary>
        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStations.SelectedItem != null && cbStations.SelectedItem != null)
            {
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.BusLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.BusLineStation).id);
            }

        }

       /// <summary>
       /// delete a station
       /// </summary>
        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            if (cbStations.SelectedItem == null)
                return;
            int id = 0;
            if (cbStations.Items.Count > 1)
            {
                id = (cbStations.SelectedItem as BO.BusLineStation).id;
                if (cbStations.SelectedIndex == 0)
                    cbStations.SelectedIndex = 1;
                else
                {
                    cbStations.SelectedIndex = cbStations.SelectedIndex-1;
                }
            }
            else
            {
                id = (cbStations.SelectedItem as BO.BusLineStation).id;
            }             
            bl.removeStation(id);
            initTextBoxByCbInStations();
            if (cbStations.SelectedItem!=null)
            {
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.BusLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.BusLineStation).id);
                lvStationOfLine.Items.Refresh();
                lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.BusLine).id);
            }
            else
            {
                lvFollowStation.ItemsSource=null;
                lvLinesInStation.ItemsSource=null;
            }
        }
        /// <summary>
        /// add station (clear the inputs fields)
        /// </summary>
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
                catch (Exception exc) {tblError.Text= exc.Message; initTextBoxByCbInStations(); return; }
                finally
                {
                    init_lvFollowStation_PreviewMouseDown();  
                }
                string code = tbStationCode.Text;
                string name = tbStationName.Text;
                string address = tbStationAddress.Text;
                float latitude = float.Parse(tbStationLat.Text.ToString());
                float longitude = float.Parse(tbStationLong.Text.ToString());
                try { bl.addStation(new BO.BusLineStation() {code=code,Name=name,Address=address,Latitude=latitude,Longitude=longitude }); }
                catch (Exception exc) { tblError.Text = exc.Message; return; }
                finally { initTextBoxes(false, false, 1); initTextBoxByCbInStations(); }
                
                btnAddBus.Content = "Add";
                tbStationDriveTm.Visibility = Visibility.Visible;
                tbStationDistance.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// allow to user enter only digits to the code field
        /// </summary>
        private void tbStationCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        TimeSpan fTs, eTs;// timespan varibles to save the time drive between stations and the total drive time before changing
        /// <summary>
        /// 
        /// </summary>
        private void lvFollowStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvFollowStation.Items.Count == 0||lvFollowStation.SelectedItem==null)
                return;
            folStatIdSelect = (lvFollowStation.SelectedItem as BO.BusLineStation).id;
            tbStationDriveTm.IsEnabled = true;
            tbStationDistance.IsEnabled = true;
            btnUpdateTimOrDis.Visibility = Visibility.Visible;
            btnAddStation.Visibility = Visibility.Collapsed;
            btnUpdateStation.Visibility = Visibility.Collapsed;
            btnDeleteStation.Visibility = Visibility.Collapsed;
            initTextBoxes(false, false, 3);
            int index = 0;
            foreach (var item in lvFollowStation.Items)
            {
                if ((item as BO.BusLineStation).id == folStatIdSelect)
                {
                    break;
                }

                index++;
            }
            int idLine = 0, index2 = 0;
            foreach (var item in lvLinesInStation.Items)
            {
                if (index2 == index)
                {
                    idLine = (item as BO.BusLine).id;
                    break;
                }
                index2++;
            }
            fTs = TimeSpan.Parse(bl.GetBusLine(idLine).driveTime);
            eTs = TimeSpan.Parse(tbStationDriveTm.Text);
        }
        /// <summary>
        /// when user clicks away on follow station update the drive time and distance text boxes
        /// </summary>
        private void lvFollowStation_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            tblError.Text = "";        
            init_lvFollowStation_PreviewMouseDown();
        }
        /// <summary>
        /// visible the buttons of add/update and dissable follow station textboxes.
        /// </summary>
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
        /// <summary>
        /// update station
        /// </summary>
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
                    var station = new BO.BusLineStation() {code= tbStationCode.Text,Latitude= float.Parse(tbStationLat.Text),Longitude= float.Parse(tbStationLong.Text),Address= tbStationAddress.Text,Name=tbStationName.Text };
                    station.id = (cbStations.SelectedItem as BO.BusLineStation).id;
                    bl.updateStation(station);
                    initTextBoxByCbInStations();
                    init_lvFollowStation_PreviewMouseDown();
                    cbBusLines.ItemsSource = bl.GetAllbusLines();
                    cbBusLines.SelectedIndex = 0;
                }
                catch (Exception exc) { tblError.Text = exc.Message; return; }
            }
        }
        /// <summary>
        /// update the drive time or distance between the current station to the follow .
        /// </summary>
        private void btnUpdateTimOrDis_Click(object sender, RoutedEventArgs e)
        {
            try { validINputDriveTimeOrDistance(); }
            catch (Exception exc) { tblError.Text = exc.Message; initTextBoxByCbInStations(); return;  }
            finally {
                init_lvFollowStation_PreviewMouseDown();  
            }           
            try
            {
                int idsta = 0,index=0;
                string lineNum = "";
                foreach (var item in lvFollowStation.Items)
                {
                    if ((item as BO.BusLineStation).id == folStatIdSelect)
                    {
                        idsta = (item as BO.BusLineStation).id;                       
                        break;
                    }
                    index++;
                }
                int idLine = 0, index2 = 0 ;
                foreach (var item in lvLinesInStation.Items)
                {
                    if (index2 == index)
                    {
                        idLine = (item as BO.BusLine).id;
                        lineNum= (item as BO.BusLine).number;
                        break;
                    }
                    index2++;
                }
                double x = double.Parse(tbStationDistance.Text);
                var folStation = new BO.FollowStations((cbStations.SelectedItem as BO.BusLineStation).id, idsta,idLine, double.Parse(tbStationDistance.Text), TimeSpan.Parse(tbStationDriveTm.Text),lineNum);
                folStation.id = bl.GetIdFollowStationBy((cbStations.SelectedItem as BO.BusLineStation).id, idsta, idLine);
                bl.updateFollowStation(folStation,fTs,eTs, TimeSpan.Parse(tbStationDriveTm.Text));
                lvLinesInStation.ItemsSource= bl.GetAllLinesInStation((cbStations.SelectedItem as BO.BusLineStation).id);
                initTextBoxByCbInStations();
                cbBusLines.ItemsSource = bl.GetAllbusLines();
                cbBusLines.SelectedIndex = 0;
            }
            catch (Exception exc) { tblError.Text = exc.Message; return; }
        }

        #endregion

        #region lines
        /// <summary>
        /// calls the add line window, which adds a new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLine_click(object sender, RoutedEventArgs e)
        {
            addLine addWindow = new addLine();
            addWindow.ShowDialog();
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;
            initTextBoxByCbInStations();
            refreshLineTextboxes();

        }

        /// <summary>
        /// 
        /// </summary>
        private void refreshLineTextboxes()
        {
            if (lvStationOfLine.Items.Count == 0)
                return;
            tbLineFirstSta.Text = (lvStationOfLine.Items[0] as BO.BusLineStation).Name;
            tbLineLastSta.Text = (lvStationOfLine.Items[lvStationOfLine.Items.Count - 1] as BO.BusLineStation).Name;
            if(cbBusLines.SelectedItem as BO.BusLine!=null)
                 tbldriveTime.Text = (cbBusLines.SelectedItem as BO.BusLine).driveTime;
        }

        /// <summary>
        /// when a different line is selcted display data about that line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBusLines.SelectedItem != null)
                lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.BusLine).id);
            refreshLineTextboxes();

        }
        private void lvStationOfLine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void lvStationOfLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// deletes the line the mouse id hovering above
        /// </summary>
       
        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbBusLines.SelectedItem == null)
                return;

            try { bl.removeLine((cbBusLines.SelectedItem as BO.BusLine).id); }
            catch (Exception exc) { MessageBox.Show(exc.Message); return; }
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;


        }

        /// <summary>
        /// calls the update function on selected line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbBusLines.SelectedItem == null)
                return;
            addLine addWindow = new addLine(1, (cbBusLines.SelectedItem as BO.BusLine).id, (cbBusLines.SelectedItem as BO.BusLine).number);
            addWindow.ShowDialog();
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.Items.Refresh();
            cbBusLines.SelectedIndex = 0;
            refreshLineTextboxes();
        }

        #endregion

        #region utility
        /// <summary>
        /// initaizes list views and combo boxes
        /// </summary>
        private void initSource()
        { 
            dpLastMaintenance.DisplayDateEnd = DateTime.Now;
            dpRegiDate.DisplayDateEnd = DateTime.Now;
            tbiBuses.DataContext = bl.GetAllBuses(busOrder);
            lvBuses.SelectedIndex = 0;
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.SelectedIndex = 0;
            if(cbStations.SelectedItem!=null)
            { 
                lvLinesInStation.ItemsSource = bl.GetAllLinesInStation((cbStations.SelectedItem as BO.BusLineStation).id);
                lvFollowStation.ItemsSource = bl.GetAllFollowStationsAsStationsObj((cbStations.SelectedItem as BO.BusLineStation).id);
            }
            cbBusLines.ItemsSource = bl.GetAllbusLines();
            cbBusLines.SelectedIndex = 0;
            if(cbBusLines.SelectedItem!=null)
                  lvStationOfLine.ItemsSource = bl.GetAllStationInLine((cbBusLines.SelectedItem as BO.BusLine).id);
            refreshLineTextboxes();
        }
        /// <summary>
        /// intintailzes and.or clears textboxes text
        /// </summary>
        /// <param name="flasEnabled">true to enable textboxes, or false to disable textboxes </param>
        /// <param name="flagContent">true to clear the textboxes or false not to</param>
        /// <param name="tabItem">selects whose textboxes to intintailzes (lines=2,buese=1, else: stations) </param>
        private void initTextBoxes(bool flagEnabled, bool flagContent, int tabItem)
        {
            if (tabItem == 1)//buses
            {
                if (flagEnabled)
                {
                    tbId.IsEnabled = true;
                    tbFuel.IsEnabled = true;
                    tbDistance.IsEnabled = true;
                    tbTotalDist.IsEnabled = true;
                    dpRegiDate.IsEnabled = true;
                    dpLastMaintenance.IsEnabled = true;
                    tbDangerous.IsEnabled = false;
                }
                else
                {
                    tbId.IsEnabled = false;
                    tbFuel.IsEnabled = false;
                    tbDistance.IsEnabled = false;
                    tbTotalDist.IsEnabled = false;
                    dpRegiDate.IsEnabled = false;
                    dpLastMaintenance.IsEnabled = false;
                    tbDangerous.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbId.Clear();
                    tbFuel.Clear();
                    tbDistance.Clear();
                    tbTotalDist.Clear();
                    dpRegiDate.Text = DateTime.Now.ToString();
                    dpLastMaintenance.Text = DateTime.Now.ToString();
                    tbDangerous.Clear();//----------------------------------------------------------------------------------------------fix denger bindning
                }
            }
            else if (tabItem == 2)//lines
            {
                if (flagEnabled)
                {
                    tbLineNumber.IsEnabled = true;
                    tbLineArea.IsEnabled = true;
                    tbLineFirstSta.IsEnabled = true;
                    tbLineLastSta.IsEnabled = true;
                }
                else
                {
                    tbLineNumber.IsEnabled = false;
                    tbLineArea.IsEnabled = false;
                    tbLineFirstSta.IsEnabled = false;
                    tbLineLastSta.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbLineNumber.Clear();
                    tbLineArea.Clear();
                    tbldriveTime.Clear();
                    tbLineFirstSta.Clear();
                    tbLineLastSta.Clear();
                }
            }
            else//stations
            {
                if (flagEnabled)
                {
                    tbStationCode.IsEnabled = true;
                    tbStationName.IsEnabled = true;
                    tbStationAddress.IsEnabled = true;
                    tbStationLat.IsEnabled = true;
                    tbStationLong.IsEnabled = true;
                }
                else
                {
                    tbStationCode.IsEnabled = false;
                    tbStationName.IsEnabled = false;
                    tbStationAddress.IsEnabled = false;
                    tbStationLat.IsEnabled = false;
                    tbStationLong.IsEnabled = false;
                }
                if (flagContent)
                {
                    tbStationCode.Clear();
                    tbStationName.Clear();
                    tbStationAddress.Clear();
                    tbStationLat.Clear();
                    tbStationLong.Clear();
                }
            }
        }
        /// <summary>
        /// refresh any chosen station in combobox to the textboxes after changes.
        /// </summary>
        private void initTextBoxByCbInStations()
        {
            
            int index = 0;
            foreach (var item in cbStations.Items)
            {
                if ((item as BO.BusLineStation).id == (cbStations.SelectedItem as BO.BusLineStation).id)
                    break;
                index++;
            }
            cbStations.ItemsSource = bl.GetAllbusLineStation();
            cbStations.Items.Refresh();
            cbStations.SelectedIndex = index;
        }
        /// <summary>
        /// validates user input and assigns it to the out parameters, or throws exception in case of error
        /// </summary>
        /// <param name="fuel">amount of fuel</param>
        /// <param name="distance">amount of distance since last maintenance</param>
        /// <param name="totaldistance"> total milage in bus</param>
        private void validateInput(out int fuel, out int distance, out int totaldistance)
        {
            bool flag;
            tbId.IsEnabled = false;
            if ((tbId.Text == null) || (tbId.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: Id field cannot be empty");
            string temp = "";
            foreach (char element in tbId.Text)
                if (element != '-')
                    temp += element;
            if ((temp.Length != 8) && (temp.Length != 7))
                throw new InvalidUserInputExecption("Invalid input: id must be 7 or 8 digits");
            foreach (char latter in tbId.Text)
            {
                if (((latter > '9') || (latter < '0')) && (latter != '-'))
                    throw new InvalidUserInputExecption("Invalid input: id must be an integer");
            }
            if ((tbId.Text.Length == 8 && ((DateTime)dpRegiDate.SelectedDate).Year < 2018) || (tbId.Text.Length == 7 && ((DateTime)dpRegiDate.SelectedDate).Year >= 2018))
                throw new InvalidUserInputExecption("Invalid input: id format doesn't match registration date");
            tbFuel.IsEnabled = false;
            if ((tbFuel.Text == null) || (tbFuel.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: fuel field cannot be empty");
            flag = int.TryParse(tbFuel.Text, out fuel);
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
            tbTotalDist.IsEnabled = false;
            if ((tbTotalDist.Text == null) || (tbTotalDist.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: total distance field cannot be empty");
            flag = int.TryParse(tbTotalDist.Text, out totaldistance);
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
        #region convertExcel
        string path="";
        public void SaveExcelWorkBook()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "TasksExcel"; //default file name
            dlg.DefaultExt = ".xlsx"; //default file extension
            dlg.Filter = "XLSX Document (.xlsx)|*.xlsx"; //filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                path = dlg.FileName;
            }
        }
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            SaveExcelWorkBook();
            try
            {
                bl.ConvertToExcel("C:\\Users\\LENOVO\\source\\repos\\sheldorTheConqueror73\\dotNet5781_3169_8515\\dotNet5781_3169_8515\\xml\\Buses.xml", path);
                MessageBox.Show("Conversion Completed!");
            }
            catch (Exception exc) { }
        }
        #endregion
        #endregion


    }
}
