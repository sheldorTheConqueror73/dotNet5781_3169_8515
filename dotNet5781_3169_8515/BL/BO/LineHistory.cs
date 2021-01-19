using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineHistory : BOobject
    {
        public int LineId { get; set; }
        public string LineNumber { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public TimeSpan duration { get; set; }

        public string description { get; set; }

        public LineHistory()
        {
            LineId = -1;
            LineNumber = "";
            start = DateTime.MinValue;
            end= DateTime.MinValue;
            duration = TimeSpan.Zero;
            description = "";
        }

    }

}
