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

        /// <summary>
        /// clears wpf form textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reset(object sender, RoutedEventArgs e)
        {
            txbFirstName.Clear();
            txbLastName.Clear();
            txbMail.Clear();
            txbUsername.Clear();
            psbConfirm.Clear();
            psbPassword.Clear();
        }

        /// <summary>
        /// calls BL.adduser() to create new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validate();
            }
            catch (Exception exc)
            {
                txbErrorMessage.Text = exc.Message;
            }
            try { bl.addUser(new BO.User() {password= psbPassword.Password,name= txbUsername.Text,accessLevel= "User",fullname= txbFirstName.Text + " " + txbLastName.Text,mail= txbMail.Text,enabled=true}); }
            catch (Exception ecx) { txbErrorMessage.Text = ecx.Message; return; }
            this.Close();
        }

        /// <summary>
        /// validates user input, throw exception in bnot vaild
        /// </summary>
        private void validate()
        {
            if (psbPassword.Password != psbConfirm.Password)
                throw new InvalidUserInputExecption("Password does not match password confirm");
            foreach (var chr in txbFirstName.Text)
                if ((chr < 'a' && chr > 'z') || (chr < 'A' && chr > 'Z'))
                    throw new InvalidUserInputExecption("First name may only contain latters");
            foreach (var chr in txbLastName.Text)
                if ((chr < 'a' && chr > 'z') || (chr < 'A' && chr > 'Z'))
                    throw new InvalidUserInputExecption("Last name may only contain latters");
            try
            {
                var addr = new System.Net.Mail.MailAddress(txbMail.Text);
            }
            catch
            {
                throw new InvalidUserInputExecption("Invalid mail address");
            }
        }
        
    }
}
