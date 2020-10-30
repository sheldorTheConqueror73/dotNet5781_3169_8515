using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLineStation : busStation
    {
        private int distance;//maybe add readonly?
        private TimeSpan driveTime;

        public int Distance
        {
            get => distance;
            set
            {
                if (value > 0)
                    distance = value;
            }
        }
        public TimeSpan DriveTime
        {
            get => driveTime;
        }

        internal busLineStation()
        {
            distance = -1;
            driveTime= new TimeSpan();
        }
        internal busLineStation(string id, int lat, int lon, string address= "")
        {

        }
    }
}
