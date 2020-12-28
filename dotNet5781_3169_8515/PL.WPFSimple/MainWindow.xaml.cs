using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using BLAPI;

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for managerView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
      //  Clearance clearance = Clearance.None;
        string userName = "";
        int userId=-1;
        IBL bL = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            managerView managerView = new managerView();
            string username = txbusername.Text;
            string password = txbupassword.Text;
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter user name AND password");
                return;
            }
            string str="";
            try { str = bl.authenticate(username, password, out userId); }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
         
            if (str == "Admin" || str == "Operator")
            {
                this.Hide();
                managerView.Show();
            }
        }

        private void newUser_click(object sender, RoutedEventArgs e)
        {
            addUser add = new addUser();
            add.Show();
        }

        private void TextBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
              login_Click(this, new RoutedEventArgs());
        }
    }
}
