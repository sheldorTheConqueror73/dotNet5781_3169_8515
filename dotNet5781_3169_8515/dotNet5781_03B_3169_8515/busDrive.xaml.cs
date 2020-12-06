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
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using dotNet5781_03B_3169_8515.utility;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// get number of kilometer from the user and send the bus to drive.
    /// Interaction logic for busDrive.xaml
    /// </summary>
    ///
   
    public partial class busDrive : Window
    {
        bool sound;
        public event Action<double> tim;//property of timer to sync the time with the main window.
        MainWindow mainWindow1;
        buses lineData; 
        DispatcherTimer timer;//the timer Thread.
        private int counter = 0;//the timer number in seconds.
        Random r = new Random();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fx">getting the framworkelement of the clicked Button of specific bus</param>
        /// <param name="_dt">reference of timer in the main window to start the time when the bus sending to drive </param>
        /// <param name="sound"> bool arguments if to play to the user sound of driding bus or not</param>
        public busDrive(ref FrameworkElement fx,DispatcherTimer _dt,bool sound)
        {
            this.sound = sound;
            timer = _dt;
            InitializeComponent();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow1 = window as MainWindow;
                }
            }

            lineData = fx.DataContext as buses;
            labId.Content = "Bus Id: "+lineData.IdFormat;
            
        }
        /// <summary>
        /// allows the user to enter only digits to the textbox.
        /// </summary>      
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
        /// <summary>
        /// send the bus to drive , update status and starting the timer.
        /// </summary>
       
        private void TextBox_sendDrive(Object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Enter && tBoxDistance.Text.ToString() != "") 
            {
                try { lineData.CanMakeDrive(int.Parse(tBoxDistance.Text.ToString()),sound); }

                    catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return;
                }

                timer.Start();
                MessageBox.Show("selected bus is on its way");
                int speed = r.Next(20, 51);
                counter =(int) ((((int.Parse(tBoxDistance.Text))*(double)1000) / ((speed*1000)/(double)3600))/(double)10);
                if (tim != null)
                {
                    lineData.Status="mid-ride";
                    lineData.IconPath = "/src/pics/waitIcon.png";
                    tim(counter);
                    mainWindow1.busSort();
                    mainWindow1.bsDisplay.Items.Refresh();
                }
                this.Close();
            }
        }

      
    }
}
