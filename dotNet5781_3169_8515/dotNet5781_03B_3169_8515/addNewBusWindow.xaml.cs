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
        public addBusWindow()
        {
            InitializeComponent();
            dplmiDate.DisplayDateEnd = DateTime.Now;
         
            dpRegiDate.DisplayDateEnd = DateTime.Now;


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
            try
            {
                validateInput();
            }
           

        }
        private void validateInput()
        {
            bool flag;
            int num;
            txbid.IsEnabled = false;
            if ((txbid.Text.Length != 6) && (txbid.Text.Length != 7))
                throw new InvalidUserInputExecption("Invalid input: id must be 6 or 7 digits");
            foreach(char latter in txbid.Text)
            {
                if((latter>'9')||(latter<'0'))
                    throw new InvalidUserInputExecption("Invalid input: id must contain digits only");
            }
            
            txbFuel.IsEnabled = false;
            flag = int.TryParse(txbFuel.Text, out num);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: fuel must contain digits only");
            if ((num > 1200) || (num < 0))
                throw new InvalidUserInputExecption("Invalid input: fuel must be within the range of 0 to 1200");
            txbDistance.IsEnabled = false;
            flag = int.TryParse(txbDistance.Text, out num);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: distance since last masdinasd must contain digits only");
            if ((num > 20000) || (num < 0))
                throw new InvalidUserInputExecption("Invalid input: distance since last masdinasd must be within the range of 0 to 20000");
            txbTotalDistance.IsEnabled = false;
            flag = int.TryParse(txbTotalDistance.Text, out num);
                if(!flag)
                throw new InvalidUserInputExecption("Invalid input: total distance must contain digits only");
            if (num < 0)
                throw new InvalidUserInputExecption("Invalid input: total distance must not be lesser than 0");
        }
    }
}
