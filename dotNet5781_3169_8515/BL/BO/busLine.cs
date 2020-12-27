using DLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class busLine : IEnumerable<Bus>
    {
        public IEnumerable<Bus> lines;
        public busLine()
        {
            this.id = "";
            this.number = "";
            this.enabled = true;
        }
        public busLine(string id, string number, bool enabled, IEnumerable<busLineStation> path)
        {
            this.id = id;
            this.number = number;
            this.enabled = enabled;
            Path = path;
        }

        public IEnumerator<Bus> GetEnumerator()
        {
            return ((IEnumerable<Bus>)lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)lines).GetEnumerator();
        }

        public string id { get; set; }
        public string number { get; set; }

        public bool enabled { get; set; }
        
        public IEnumerable<busLineStation> Path { get; set; }




    }
}
