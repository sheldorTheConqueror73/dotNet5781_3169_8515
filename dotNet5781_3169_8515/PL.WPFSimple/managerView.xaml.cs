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
                  
            var buses = bl.GetAllBuses();
            
            InitializeComponent();            
            tbiBuses.DataContext = buses;
            busesView.SelectedIndex = 0;

        }

        private void busesView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            initTextBoxes(true,false);
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddBus.Content.Equals("Add"))
            {
                btnAddBus.Content = "Submit";
                initTextBoxes(true, true);
            }
            else
            {
                bl.addBus(PO.ConvertorUtility())
                btnAddBus.Content = "Add";
            }

        }
        private void initTextBoxes(bool flagEnabled, bool flagContent)
        {
            if (flagEnabled)
            {
                tbid.IsEnabled = true;
                tbfuel.IsEnabled = true;
                tbDistance.IsEnabled = true;
                tbtotalDist.IsEnabled = true;
                dpRegiDate.IsEnabled = true;
                dplmiDate.IsEnabled = true;
                tbDangerous.IsEnabled = true;
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
                tbDangerous.Text = "NO";
            }
        }
    }
}
