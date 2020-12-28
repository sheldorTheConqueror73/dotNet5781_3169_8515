using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class POobject
    {
        protected  static int idCounter  = 0;
        public int id { get; set; }
        public POobject()
        {
            this.id = idCounter++;
        }

    }
}
