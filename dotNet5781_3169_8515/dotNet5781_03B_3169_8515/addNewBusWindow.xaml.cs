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
            buses bus1;

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
    }
}
