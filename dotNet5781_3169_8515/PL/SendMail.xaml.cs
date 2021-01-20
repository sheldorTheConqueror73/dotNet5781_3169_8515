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
    /// Interaction logic for SendMail.xaml
    /// </summary>
    public partial class SendMail : Window
    {
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        int idUser = 0;
        /// <summary>
        /// ctor of the class
        /// </summary>
        /// <param name="id">id of the user that sendig to him an email</param>
        public SendMail(int id)
        {
            InitializeComponent();
            idUser = id;
            var user = bl.GetUser(idUser);
            txtTo.Text = user.mail;
        }
        /// <summary>
        /// resset all fields (not include "To")
        /// </summary>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSubject.Text = "";
            txtContent.Text = "";

        }
        /// <summary>
        /// sending the email
        /// </summary>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            bl.sendMail(idUser, txtSubject.Text, txtContent.Text);
            MessageBox.Show("send successfully!");
            this.Close();
        }
    }
}
