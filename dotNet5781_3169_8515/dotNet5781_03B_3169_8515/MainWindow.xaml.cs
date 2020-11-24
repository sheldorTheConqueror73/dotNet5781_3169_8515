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
using dotNet5781_01_3169_8515;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<buses> busPool;
        Random r = new Random();
        string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
        public MainWindow()
        {
            InitializeComponent();
            initBus();
        }
        public void initBus()
        {
            int month, day, year;
            year = r.Next(1980, DateTime.Now.Year + 1);
            if(year=DateTime.Now.Year)

            DateTime rd = new DateTime(r.Next(1980,DateTime.Now.Year+1),)
            for (int i = 0; i < 13; i++)
            {
                busPool[i] = new buses();
            }
        }
    }

}
