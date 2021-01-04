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
            try { bl.addUser(new BO.User(psbpassword.Password, txbusername.Text, BO.Clearance.User, txbFirstName.Text + " " + txbLastName.Text, txbmail.Text)); }
            catch (Exception ecx) { errormessage.Text = ecx.Message; return; }
            this.Close();
        }
        
    }
}
