using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DOobject
    {
        protected static int idCounter =0;
        public int id { get; set; }
        public DOobject()
        {
            this.id = idCounter++;
        }
    }
}
