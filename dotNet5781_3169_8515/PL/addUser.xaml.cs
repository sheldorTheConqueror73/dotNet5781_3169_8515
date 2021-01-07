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

namespace PL
{
    /// <summary>
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Window
    {
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        public addUser()
        {
            InitializeComponent();
        }

        private void reset(object sender, RoutedEventArgs e)
        {
            txbFirstName.Clear();
            txbLastName.Clear();
            txbmail.Clear();
            txbusername.Clear();
            psbconfirm.Clear();
            psbpassword.Clear();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //add input check/regex
            try { bl.addUser(new BO.User() {password= psbpassword.Password,name= txbusername.Text,accessLevel= "User",fullname= txbFirstName.Text + " " + txbLastName.Text,mail= txbmail.Text,enabled=true}); }
            catch (Exception ecx) { errormessage.Text = ecx.Message; return; }
            this.Close();
        }
        
    }
}
