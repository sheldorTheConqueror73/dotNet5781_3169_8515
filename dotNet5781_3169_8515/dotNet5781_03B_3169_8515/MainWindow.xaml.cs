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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_01_3169_8515;

namespace dotNet5781_03B_3169_8515
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<buses> busPool = new List<buses>();
        Random r = new Random();
        readonly string appPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
        const short FULL_TANK = 1200;
        private buses currentBus;
        public MainWindow()//add mini payer to menu
        {
            InitializeComponent();
            initBus();
            bsDisplay.ItemsSource = busPool;
            showBuses(busPool[0].Id);
        }
        public void initBus()
        {
            for (int i = 0; i < 13; i++)
            {
                bool flag = true;
                string id = "";
                while (flag)
                {
                    id = r.Next(100000, 1000000).ToString();//make sure id format matches MD 
                    flag = false;
                    foreach (var bus in busPool)
                        if (id == bus.Id)//need to change accessers
                        {
                            flag = true;
                            break;
                        }
                }
                //updatedanr
                DateTime rd = randomDate();
                DateTime lastM = randomDate(1);
                busPool.Add(new buses(rd, lastM, id, r.Next(0, FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000)));
            }
            //set 3 buses to match requirments
            busPool[0].LastMaintenance=new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            busPool[1].Distance=19999;
            busPool[2].Fuel = 0;
        }
        private DateTime randomDate(int mode = 0)
        {
            int month, day, year;
            year = r.Next(1980, DateTime.Now.Year + 1);
            if (year == DateTime.Now.Year)
            {
                month = r.Next(1, DateTime.Now.Month + 1);
                if (month == DateTime.Now.Month)
                    day = r.Next(1, DateTime.Now.Day + 1);
                else
                    day = r.Next(1, 32);
            }
            else
            {
                month = r.Next(1, 13);
                day = r.Next(1, 32);
            }
            if (mode == 1)
                year = DateTime.Now.Year;
            try { 
            return new DateTime(year, month, day);
            }
            catch(Exception e)
            {
                return randomDate(mode);
            }
        }
        private void showBuses(string id)
        {
            currentBus = busPool[find(id)];
            busesgrid.DataContext = currentBus;
            bsDisplay.DataContext = currentBus.ToString();
        }
        private int find(string id)
        {
            int i = 0;
            foreach (var bus in busPool)
            {
                if (bus.Id == id)
                    return i;
                i++;
            }
            throw new Exception("no Match");//change
        }
    }

}
