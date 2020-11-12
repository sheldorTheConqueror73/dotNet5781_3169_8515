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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_3169_8515;
namespace dotNet5781_03A_3169_8515
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        static busLines buses = new busLines();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void initBus()
        {
            for (int i = 0; i < 10; i++)
            {

                string id = (r.Next(1, 1000)).ToString();
                Areas a1 = (Areas)r.Next(0, 10);
                int size = r.Next(2, 11);
                busLineStation[] arr = busLines.tandom(size);
                try
                {
                    buses.add(new bus(arr.ToList<busLineStation>(), id, arr[0], arr[size - 1]));
                }
                catch (Exception e) { Console.WriteLine(e.Message); i--; }
                
            }

        }
    }
}
