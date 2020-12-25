
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus 
    {
        public static int FULL_TANK = 1200;
        public bool enabled { get; set; }
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string id { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string status { get; set; }
        public  Area area { get; set; }




        public Bus()//ctor
        {
            id = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime();
            lastMaintenance = new DateTime();
            enabled = false;

        }
        //ctor
        public Bus(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready")//cotr
        {
            this.id = id;
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

