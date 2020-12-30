using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class busLine:DOobject
    {
     //   public string id { get; set; }
        public string number { get; set; }
        public Area area { get; set; }
        public bool enabled { get; set; }
        public DateTime driveTime { get; set; }
    }
}
