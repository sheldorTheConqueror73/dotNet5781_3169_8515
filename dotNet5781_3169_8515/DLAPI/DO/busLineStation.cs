using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineStation : BusStation
    {
        private double distance;
        private TimeSpan driveTime;
        //getters and setters:
        public double Distance
        { get; set; }
        public TimeSpan DriveTime
        { get; set; }

        public BusLineStation() : base()//ctor
        {
            distance = 0;
            driveTime = new TimeSpan();
        }
    }

}
