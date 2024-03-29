﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
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

namespace PL
{
    
    /// <summary>
    /// Interaction logic for managerView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player= new MediaPlayer();
        BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        string userName = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// calls BL.authenticate to check if user is registered and check user access level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Click(object sender, RoutedEventArgs e)
        {
            txbErrorMessage.Foreground = Brushes.Red;
            managerView managerView;
            string username = txbUsername.Text;
            string password = txbUPassword.Password;
            if (username == "" || password == "")
            {
                txbErrorMessage.Text="Please enter user name AND password";
                return;
            }
            BO.User user=null;
            try { user = bl.authenticate(username, password); }//check if user exists and return user access level
            catch (Exception exc) { txbErrorMessage.Text = exc.Message; return; }
            managerView = new managerView(user);
            if (user.accessLevel == "Admin" || user.accessLevel == "Operator")//if user is admin or manager
            {
                this.Hide();
                managerView.ShowDialog();
            }
            if (user.accessLevel == "User")//if user is a regular user
            {
                txbErrorMessage.Foreground = Brushes.Green;
                txbErrorMessage.Text = $"Hello {userName}, please contact yor supreviser to grant you login permission ";
            }
        }

        /// <summary>
        /// opens the user registrarion window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newUser_click(object sender, RoutedEventArgs e)
        {
            addUser add = new addUser();
            add.ShowDialog();
        }

        /// <summary>
        /// if user presses enter login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
              login_Click(this, new RoutedEventArgs());
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    managerView managerView= new managerView(new BO.User() { id= 70, accessLevel="Admin"});
        //    this.Hide();
        //    managerView.ShowDialog();
        //}

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
        }
    }
}
