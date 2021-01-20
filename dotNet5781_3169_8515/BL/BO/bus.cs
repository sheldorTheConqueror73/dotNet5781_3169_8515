using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus:BOobject
    {
        public const int FULL_TANK = 1200;

        public TimeSpan time { get; set; }
        public double fuel { get; set; }
        public double distance { get; set; }
        public double totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string plateNumber { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string iconPath { get; set; }
        public string status { get; set; }
        public bool enabled { get; set; }
        public int  lineId { get; set; }




        public Bus()//ctor
        {
            plateNumber = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime();
            lastMaintenance = new DateTime();
            iconPath = "/src/pics/okIcon.png";
            enabled = true;
           // idFormat = formatId(id);

        }
        //ctor
       


        public override string ToString()
        {
            return this.plateNumber;
        }


    }


}
