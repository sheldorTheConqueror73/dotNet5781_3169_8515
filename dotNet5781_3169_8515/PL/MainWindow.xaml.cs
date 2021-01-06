﻿using System;
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
        int userId=-1;
        IBL bL = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            managerView managerView = new managerView();
            string username = txbUsername.Text;
            string password = txbUPassword.Password;
            if (username == "" || password == "")
            {
                errormessage.Text="Please enter user name AND password";
                return;
            }
            string str="";
            try { str = bl.authenticate(username, password, out userId); }
            catch (Exception exc) { errormessage.Text = exc.Message; }
         
            if (str == "Admin" || str == "Operator")
            {
                this.Hide();
                player.Open(new Uri(@"C:\Users\Chuck\source\repos\dotNet5781_3169_8515\dotNet5781_3169_8515\PL\Resources\Startup.mp3"));
                player.Play();
                managerView.ShowDialog();
            }
            if (str == "User")
            {
                errormessage.Text = "Welcom user";
            }
            else
            {
                player.Open(new Uri(@"C:\Users\Chuck\source\repos\dotNet5781_3169_8515\dotNet5781_3169_8515\PL\Resources\AccessDenied.mp3"));
                player.Play();
            }
        }

        private void newUser_click(object sender, RoutedEventArgs e)
        {
            addUser add = new addUser();
            add.ShowDialog();
        }

        private void TextBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
              login_Click(this, new RoutedEventArgs());
        }
    }
}