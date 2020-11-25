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

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for busDetailsByDoubleClick.xaml
    /// </summary>
    public partial class busDetailsByDoubleClick : Window
    {
        public event Action<int> fuel1;
        public event Action<DateTime> lmaintenance;


        public busDetailsByDoubleClick()
        {
            InitializeComponent();
           // MainWindow mainWindow = new MainWindow();
            //this.DataContext = mainWindow;
        }
        public busDetailsByDoubleClick(ref int _fuel,ref DateTime lmaintenance)
        {
            InitializeComponent();          
        }

        private void refuel_Button_Click(object sender, RoutedEventArgs e)
        {
            if(fuel1 != null)
                fuel1(1200);
            labfuel.Content = "1200";
            MessageBox.Show("sending to refuel...");
        }

        private void maintenance_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (lmaintenance != null)
                lmaintenance(date);
            labLMaintenance.Content = date.ToString();
            MessageBox.Show("sending to maintenance...");

        }
    }
}
