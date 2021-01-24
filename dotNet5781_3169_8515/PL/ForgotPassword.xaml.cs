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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        BO.User user=null;
        public ForgotPassword()
        {
            InitializeComponent();
        }
        private void resetPass(object sender, RoutedEventArgs e)
        {
            txbErrorMessage.Foreground = Brushes.Red;
            try
            {
                validate();
            }
            catch(Exception exc)
            {
                txbErrorMessage.Text = exc.Message;
                return;
            }
          
            string password=bl.resetPassword(user);
            string subject = "Your  Password has been reset";
            string text = $" hello there {user.fullname}"+ ", you seem to have misplaced your password. do not worry, we are here to save you!\nyour new password is:\n"+password+"\nmake sure to store it some safe.";
            bl.sendMail(user.id, subject, text);
            txbErrorMessage.Foreground = Brushes.GreenYellow;
            txbErrorMessage.Text = "An email has been sent to you account";
            return;

        }
        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                resetPass(this, new RoutedEventArgs());
        }
        private void validate()
        {
            txbErrorMessage.Foreground = Brushes.Red;
            foreach (var chr in txbUsername.Text)
                if (chr == '!' || chr == '@' || chr == '#' || chr == '$' || chr == '%' || chr == '^' || chr == '*' || chr == '&' || chr == '(' || chr == ')' || chr == ':' || chr == ';')
                    throw new InvalidUserInputExecption("User Name may not contain !@#$%^&*():;");
            if (txbMail.Text == "" || txbUsername.Text == "")
                throw new InvalidUserInputExecption("All Fileds must bne filled");
            try
            {
                var addr = new System.Net.Mail.MailAddress(txbMail.Text);
            }
            catch
            {
                throw new InvalidUserInputExecption("Invalid mail address");
            }
            BO.User user = bl.checkMail(txbUsername.Text, txbMail.Text);
            if (user == null)
            {
                throw new InvalidUserInputExecption( $"No user matches username {txbUsername.Text} and mail adrress {txbMail.Text}");
               
            }
            this.user = user;
        }
    }
}
