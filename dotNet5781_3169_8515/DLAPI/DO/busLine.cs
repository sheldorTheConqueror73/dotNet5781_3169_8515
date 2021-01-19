using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine:DOobject
    {
        public string number { get; set; }
        public Area area { get; set; }
        public bool enabled { get; set; }
        public string driveTime { get; set; }
        public double distance { get; set; }
    }
}
