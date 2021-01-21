using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineInStation:DOobject
    {
        public int stationid{ get; set; }
        public int Lineid { get; set; }
        public string lineNumber { get; set; }
        public int placeOrder { get; set; }
    }
}
