using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class followStations : BOobject
    {
        public int firstStationid { get; set; }
        public int secondStationid { get; set; }
        public bool enabled { get; set; }
        public int distance { get; set; }
        public TimeSpan driveTime { get; set; }
    }
}