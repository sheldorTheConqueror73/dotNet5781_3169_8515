using System;
using System.IO;
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
        string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";

        private bus currentDisplayBusLine;
        public MainWindow()
        {
            /*
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = $"{appPath}rcs\\audio\\shadilay.wav";

            try
            {
                player.Play();
            }
            catch (Exception e) { };
            */
            InitializeComponent();
            initBus();
            scBusLines.ItemsSource = buses.lines;
            scBusLines.DisplayMemberPath ="Id";
            scBusLines.SelectedIndex = 0;
            lbl1.Content = (scBusLines.SelectedValue as bus).convert((scBusLines.SelectedValue as bus).timeBetweenStations((scBusLines.SelectedValue as bus).FirstStation.Id, (scBusLines.SelectedValue as bus).LastStation.Id)).Split(' ')[1];
            ShowBusLine((scBusLines.SelectedValue as bus).Id);
            
        }
        public void initBus()
        {
            for (int i = 0; i < 10; i++)
            {

                string id = (r.Next(1, 1000)).ToString();
                Areas a1 = (Areas)r.Next(0, 10);
                int size = r.Next(2, 11);
                busLineStation[] arr = new busLineStation[size];
                arr = busLines.tandom(size);
                try
                {
                    buses.add(new bus(arr.ToList<busLineStation>(),id, arr[0], arr[size - 1],a1));
                }
                catch (Exception e) { Console.WriteLine(e.Message); i--; }// remove error print
            }
        }
        public void ShowBusLine(string id)
        {
            currentDisplayBusLine =buses.lines[buses.indexof(id)];//add error check
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Path;

        }

        private void scBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((scBusLines.SelectedValue as bus).Id);
            lbl1.Content  = (scBusLines.SelectedValue as bus).convert((scBusLines.SelectedValue as bus).timeBetweenStations((scBusLines.SelectedValue as bus).FirstStation.Id, (scBusLines.SelectedValue as bus).LastStation.Id)).Split(' ')[1];
        }
    }
}
