using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for addLineUserPromt.xaml
    /// </summary>
    public partial class addLineUserPromt : Window
    {
        List<double> distance;
        List<TimeSpan> time;
        bool canExit;
        public addLineUserPromt(List<double>dist, List<TimeSpan> t1)
        {
            canExit = false;
            InitializeComponent();
            KeyDown += Window_KeyDown;
            distance = dist;
            time = t1;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try { submit(); }
                catch (Exception exc) { lblError.Content = exc.Message; return; }
                this.Close();

            }
        }
        private void submit()
        {
            bool flag;
            int temp1;
            TimeSpan temp2;
            flag = int.TryParse(txbDistance.Text.ToString(), out temp1);
            if (!flag)
                throw new InvalidUserInputExecption("Distance must be an integer");
            if(temp1<0)
                throw new InvalidUserInputExecption("Distance must be positive");
            flag = TimeSpan.TryParse(txbTime.Text.ToString(), out temp2);
            if (!flag)
                throw new InvalidUserInputExecption("Incorrect time format");
            if(temp2==TimeSpan.Zero)
                throw new InvalidUserInputExecption("Time must be grater than zero");
            time.Add(temp2);
            distance.Add(temp1);
            canExit = true;

        }
        private void onClose(object sender,CancelEventArgs e)
        {
            if (canExit)
                return;
            lblError.Content = "Please do not exit before entering distance and time";
            System.Media.SystemSounds.Exclamation.Play();
            e.Cancel = true;      
        }

        //private void txbDistance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex reggy = new Regex("[^0-9]");
        //    e.Handled =  reggy.IsMatch(e.Text);
        //}

        //private void txbTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex reggy = new Regex("[0-9:]");
        //    Regex reggy2 = new Regex("^[0-9]{1,2}:[0-9]{2}$");
        //    e.Handled = !(reggy.IsMatch(e.Text) && reggy2.IsMatch(txbTime.Text));

        //}
    }
}
