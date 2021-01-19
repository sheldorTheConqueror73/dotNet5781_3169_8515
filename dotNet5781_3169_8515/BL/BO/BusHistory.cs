using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusHistory : BOobject
    {
        public int BusId { get; set; }
        public string PlateNumber { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public TimeSpan duration { get; set; }

        public string description { get; set; }

        public BusHistory()
        {
            BusId = -1;
            PlateNumber = "";
            start = DateTime.MinValue;
            end = DateTime.MinValue;
            duration = TimeSpan.Zero;
            description = "";
        }

    }
}
