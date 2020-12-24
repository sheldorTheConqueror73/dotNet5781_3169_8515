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
        
        public IEnumerator<Bus> GetEnumerator()
        {
            return ((IEnumerable<Bus>)lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)lines).GetEnumerator();
        }

        
    }
}
