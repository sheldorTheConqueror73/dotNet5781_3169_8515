using DLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLine : BOobject
    {     

       // public string id { get; set; }
        public string number { get; set; }

        public bool enabled { get; set; }
        public Area area { get; set; }
        public BusLineStation firstStation { get; set; }
        public BusLineStation lastStation { get; set; }
        public IEnumerable<LineInStation> Path { get; set; }
        public string driveTime { get; set; }

        public double distance { get; set; }
        public override string ToString()
        {
            return $"{number}";
        }




    }
}
