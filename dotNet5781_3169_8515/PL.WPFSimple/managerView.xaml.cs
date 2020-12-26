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
        
      
        public managerView()
        {
            BLAPI.IBL bl = BLAPI.BLFactory.GetBL();          
            List<BO.Bus> lsit1= bl.GetAllBuses();
            var buses = bl.GetAllBuses();
            
            InitializeComponent();            
            tbiBuses.DataContext = buses;

        }
    }
}
