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
using System.Text.RegularExpressions;
using System.Threading;
using dotNet5781_03B_3169_8515.utility;
namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for addBusWindow.xaml
    /// </summary>
    public partial class addBusWindow : Window
    {
        MainWindow mainWindow1;
        public addBusWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow1 = window as MainWindow;
                }
            }
            InitializeComponent();
            dplmiDate.DisplayDateEnd = DateTime.Now;
            dplmiDate.SelectedDate = DateTime.Now;
            dpRegiDate.DisplayDateEnd = DateTime.Now;
            dpRegiDate.SelectedDate = DateTime.Now;

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void readId(object sender, KeyEventArgs e)
        {

        }

        private void readRDate(object sender, KeyEventArgs e)
        {

        } 
        private void readMDate(object sender, KeyEventArgs e)
        {

        }

        private void insert(object sender, RoutedEventArgs e)
        {
            int fuel, dist, totalDist;
            try
            {
                validateInput(out fuel,out dist,out totalDist);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);//make custom msg box
                txbid.IsEnabled = true;
                txbDistance.IsEnabled = true;
                txbFuel.IsEnabled = true;
                txbTotalDistance.IsEnabled = true;
                return;
            }
            buses bs1 = new buses((DateTime)dpRegiDate.SelectedDate, (DateTime)dplmiDate.SelectedDate, txbid.Text, fuel, dist, false, totalDist, "ready","/src/pics/okIcon.png");
            bs1.UpdateDangerous();
            mainWindow1.BusPool.Add(bs1);
            mainWindow1.bsDisplay.Items.Refresh();
            MessageBox.Show("Huzzah! another bus has joined our (evil) ranks. World domination will soon be ours!");
            this.Close();
        }
        private void validateInput( out int fuel, out int distance, out int totaldistance)
        {
            bool flag;
            txbid.IsEnabled = false;
            if ((txbid.Text == null) || (txbid.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: Id field cannot be empty", Properties.Resources.Sad_Trombon);
            if ((txbid.Text.Length != 8) && (txbid.Text.Length != 7))
                throw new InvalidUserInputExecption("Invalid input: id must be 7 or 8 digits",Properties.Resources.Sad_Trombon);
            foreach(char latter in txbid.Text)
            {
                if((latter>'9')||(latter<'0'))
                    throw new InvalidUserInputExecption("Invalid input: id must contain digits only", Properties.Resources.Sad_Trombon);
            }
            if ((txbid.Text.Length == 8 && ((DateTime)dpRegiDate.SelectedDate).Year < 2018) || (txbid.Text.Length == 7 && ((DateTime)dpRegiDate.SelectedDate).Year >= 2018))
                throw new InvalidUserInputExecption("Invalid input: id format doesn't match registration date", Properties.Resources.Sad_Trombon);
            foreach(var bus in mainWindow1.BusPool)
            {
                if(txbid.Text==bus.Id)
                    throw new InvalidUserInputExecption("Invalid input: this id number is taken already", Properties.Resources.Sad_Trombon);
            }
            txbFuel.IsEnabled = false;
            if ((txbFuel.Text == null) || (txbFuel.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: fuel field cannot be empty", Properties.Resources.Sad_Trombon);
            flag = int.TryParse(txbFuel.Text, out fuel);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: fuel must contain digits only", Properties.Resources.Sad_Trombon);
            if ((fuel > 1200) || (fuel < 0))
                throw new InvalidUserInputExecption("Invalid input: fuel must be within the range of 0 to 1200", Properties.Resources.Sad_Trombon);
            txbDistance.IsEnabled = false;
            if ((txbDistance.Text == null) || (txbDistance.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: distance field cannot be empty", Properties.Resources.Sad_Trombon);
            flag = int.TryParse(txbDistance.Text, out distance);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: distance since last masdinasd must contain digits only", Properties.Resources.Sad_Trombon);
            if ((distance > 20000) || (distance < 0))
                throw new InvalidUserInputExecption("Invalid input: distance since last masdinasd must be within the range of 0 to 20000", Properties.Resources.Sad_Trombon);
            txbTotalDistance.IsEnabled = false;
            if ((txbTotalDistance.Text == null) || (txbTotalDistance.Text == ""))
                throw new InvalidUserInputExecption("Invalid input: total distance field cannot be empty", Properties.Resources.Sad_Trombon);
            flag = int.TryParse(txbTotalDistance.Text, out totaldistance);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: total distance must contain digits only", Properties.Resources.Sad_Trombon);
            if (totaldistance < 0)
                throw new InvalidUserInputExecption("Invalid input: total distance must not be lesser than 0", Properties.Resources.Sad_Trombon);
        }
       
    }
}
