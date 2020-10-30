using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLines
    {
        List<bus> lines;

        internal busLines()
        {
            this.lines = new List<bus>();
        }
        internal busLines(bus b1)
        {
            this.lines = new List<bus>();
            lines.Add(b1);
        }
        internal busLines(bus[] b1)
        {
            this.lines = new List<bus>();
            foreach(var b2 in b1)
                lines.Add(b2);
        }
        internal int find(string id)
        {
            int i = 0;
            foreach(var b1 in this.lines)
            {
                if (b1.Id == id)
                    return i;
                i++;
            }
            return -1;

        }
        internal int count(string id)
        {
            int i = 0;
            foreach (var b1 in this.lines)
            {
                if (b1.Id == id)
                 i++;
            }
            return i;

        }
        internal void add(bus b1)
        {
            int count = this.count(b1.Id);
            if (count == 2)
                throw new ArgumentException("invalid input: there are already two buse lines with this id in the system. the limit is 2");
            if(count<2)
            {
                if (count == 0)
                    this.lines.Add(b1);
                if ((b1.FirstStation == this.lines[count].LastStation) && (b1.LastStation == this.lines[count].FirstStation))// make sure indexer is right
                    this.lines.Add(b1);
                throw new ArgumentException("invalid input: this bus can only do one route and said route in reverse ");//beeter garmmer needed
            }
        }
    }
}
