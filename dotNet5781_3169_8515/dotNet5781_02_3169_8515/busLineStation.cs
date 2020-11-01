using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLineStation : busStation
    {
        private int distance;
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
        internal busLineStation(string id, double lat, double lon,int distance, TimeSpan time, string address= "") :base(id, lat, lon, address)
        {
            this.distance = distance;
            this.driveTime = time;
        }
        public override string ToString()
        {
             return $"ID:{this.id},LAT:{this.latitude},LONG:{this.longitude},ADDR:{this.address},DIST:{this.distance},TIME:{this.DriveTime.ToString()}";
        }
    }
}
