﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class Bus:POobject
    {
        public static int FULL_TANK = 1200;
        public bool enabled { get; set; }
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        //public string id { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string status { get; set; }
        public Area area { get; set; }
        public string plateNumber { get; set; }




        public Bus()//ctor
        {
            plateNumber = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime();
            lastMaintenance = new DateTime();
            enabled = false;

        }
        //ctor
        public Bus(DateTime date, DateTime lm, string plateNumber = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready")//cotr
        {
            this.plateNumber = plateNumber;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
            this.status = _status;
            enabled = true;
        }
    }
}


