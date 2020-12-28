using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOobject
    {
        protected static int idCounter = 0;
        public int id { get; set; }
        public BOobject()
        {
            this.id = idCounter++;
        }
    }
}
