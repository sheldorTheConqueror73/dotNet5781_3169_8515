using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class busLineStation : busStation
    {
        private double distance;
        private TimeSpan driveTime;
        //getters and setters:
        public double Distance
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
            set { driveTime = value; }
        }

        public busLineStation()//ctor
        {
            distance = -1;
            driveTime = new TimeSpan();
        }
        public busLineStation(string _id) : base(_id)//ctor
        {
            distance = -1;
            driveTime = new TimeSpan();
        }
        public busLineStation(string id, float lat, float lon, string address = "") : base(id, lat, lon, address)//ctor
        {
            this.distance = 0;
            this.driveTime = new TimeSpan();
        }
        public busLineStation(string id, float lat, float lon, int distance, TimeSpan time, string address = "") : base(id, lat, lon, address)//ctor
        {
            this.distance = distance;
            this.driveTime = time;
        }

        public busLineStation(busLineStation bs) : base(bs)
        {
            this.distance = bs.Distance;
            this.driveTime = bs.DriveTime;
        }

    }

}
