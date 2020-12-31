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
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for KukuWindow.xaml
    /// </summary>
    public partial class KukuWindow : Window
    {
        KukuViewModel viewModel = new KukuViewModel();
        public KukuWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            BLAPI.BLFactory.GetBL().addStation(viewModel.BusStationModel as BO.busLineStation);
        }
    }
}
