using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class followStations:DOobject
    {
        public int firstStationid { get; set; }
        public int secondStationid { get; set; }
        public int lineId { get; set; }
        public string lineNumber { get; set; }
        public bool enabled { get; set; }
        public double distance { get; set; }
        public TimeSpan  driveTime { get; set; }
    }
}
