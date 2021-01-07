using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class FollowStations : BOobject
    {
        public FollowStations(int firstStationid, int secondStationid,  double distance, TimeSpan driveTime)
        {
            this.firstStationid = firstStationid;
            this.secondStationid = secondStationid;
            this.enabled = true;
            this.distance = distance;
            this.driveTime = driveTime;
        }
        public FollowStations(int firstStationid, int secondStationid,int lineid, double distance, TimeSpan driveTime,string lineNUmber)
        {
            this.firstStationid = firstStationid;
            this.secondStationid = secondStationid;
            this.lineId = lineid;
            this.enabled = true;
            this.distance = distance;
            this.driveTime = driveTime;
            this.lineNumber = lineNUmber;
        }
        public FollowStations() { enabled = true; }
        public int firstStationid { get; set; }
        public int secondStationid { get; set; }
        public int lineId { get; set; }
        public string lineNumber { get; set; }
        public bool enabled { get; set; }
        public double distance { get; set; }
        public TimeSpan driveTime { get; set; }
    }
}