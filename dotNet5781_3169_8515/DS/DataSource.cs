﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> buses;
        public static List<busStation> stations;
        public static List<busLineStation> LineStations;
        public static List<busLine> Lines;
        static DataSource()
        {
            InitAllLists();

        }

        private static void InitAllLists()
        {
            Random r = new Random();
            for (int i = 0; i < 13; i++)
            {
                bool flag = true;
                string id = "";
                DateTime rd = randomDate();
                while (flag)
                {
                    if (rd.Year < 2018)
                        id = r.Next(1000000, 10000000).ToString();//make sure id format matches MD 
                    else
                        id = r.Next(10000000, 100000000).ToString();
                    flag = false;
                    foreach (var bus in buses)
                        if (id == bus.id)
                        {
                            flag = true;
                            break;
                        }
                }
                DateTime lastM = randomDate(1);
                buses.Add(new Bus(rd, lastM, id, r.Next(0, Bus.FULL_TANK), r.Next(0, 20001), false, r.Next(0, 120000), "ready", new Bus.timerclass(), "/src/pics/okIcon.png"));
            }
            buses[0].lastMaintenance = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            buses[0].status = "dangerous";
            buses[0].dangerous = true;
            buses[0].iconPath = "/src/pics/warningIcon.png";
            buses[1].distance = 19999;
            buses[2].fuel = 0;

        }

        private static DateTime randomDate(int mode = 0)
        {
            Random r = new Random();
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
            try
            {
                return new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                return randomDate(mode);
            }
        }
    }
}
