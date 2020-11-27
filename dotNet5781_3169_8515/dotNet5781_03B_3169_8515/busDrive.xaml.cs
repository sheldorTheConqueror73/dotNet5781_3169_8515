﻿using System;
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
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using dotNet5781_03B_3169_8515.utility;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for busDrive.xaml
    /// </summary>
    public partial class busDrive : Window
    {

        MainWindow mainWindow1;
        buses lineData;       
        public busDrive(ref FrameworkElement fx)
        {
            InitializeComponent();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow1 = window as MainWindow;
                }
            }

            lineData = fx.DataContext as buses;
            labId.Content = "Bus Id: "+lineData.Id;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_sendDrive(Object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("enter pressed");
            }
        }
    }
}