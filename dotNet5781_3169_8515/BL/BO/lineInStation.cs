using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInStation:BOobject
    {
        //public int lineInStationId { get; set; }
        public string stationCode { get; set; }
        public string LineNumber { get; set; }
        public string lineNumber { get; set; }
        // public string Address { get; set; }
        public int placeOrder { get; set; }
    }
}
