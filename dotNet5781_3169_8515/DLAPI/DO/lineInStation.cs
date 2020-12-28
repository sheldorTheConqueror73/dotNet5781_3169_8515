using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class lineInStation:DOobject
    {
        //public int lineInStationId { get; set; }
        public string stationCode { get; set; }
        public string LineNumber { get; set; }
        public string Address { get; set; }
        public int placeOrder { get; set; }
    }
}
