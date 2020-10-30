using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLine
    {
        enum Areas   {General,North,South,Center,Jerusalem,JurdenVally }
        List<busLineStation> path = new List<busLineStation>();

        private readonly string id;
        private string firstStation, lastStation;
        Areas area;

        public busLine()
        {
            
        }
    }
}
