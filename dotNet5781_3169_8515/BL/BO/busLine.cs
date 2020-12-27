using DLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class busLine : BOobject
    {     

       // public string id { get; set; }
        public string number { get; set; }

        public bool enabled { get; set; }
        public Area area { get; set; }
        public busLineStation firstStation { get; set; }
        public busLineStation lastStation { get; set; }
        public IEnumerable<lineInStation> Path { get; set; }




    }
}
