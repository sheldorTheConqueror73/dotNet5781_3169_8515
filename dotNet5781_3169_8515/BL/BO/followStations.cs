using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class followStations : BOobject
    {
        public followStations(int firstStationid, int secondStationid,  int distance, TimeSpan driveTime)
        {
            this.firstStationid = firstStationid;
            this.secondStationid = secondStationid;
            this.enabled = true;
            this.distance = distance;
            this.driveTime = driveTime;
        }
        public followStations() { enabled = true; }
        public int firstStationid { get; set; }
        public int secondStationid { get; set; }
        public bool enabled { get; set; }
        public int distance { get; set; }
        public TimeSpan driveTime { get; set; }
    }
}