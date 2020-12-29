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
        public bool enabled { get; set; }
        public int distance { get; set; }
        public TimeSpan  driveTime { get; set; }
    }
}
