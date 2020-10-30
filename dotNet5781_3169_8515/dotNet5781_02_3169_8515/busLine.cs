using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLine
    { 
        List<busLineStation> path = new List<busLineStation>();
        private readonly string id;
        private string firstStation, lastStation;
        Areas area;
        public busLine(List<busLineStation> _path,string _id, string _firstStation, string _lastStation)
        {
            this.path = _path;
            this.id = _id;
            this.firstStation = _firstStation;
            this.lastStation = _lastStation;
        }
        internal string Id
        {
            get => id;
        }
        internal string FirstStation
        {
            get => firstStation;
            set
            {
                firstStation = value;
            }
        }
        internal string LastStation
        {
            get => lastStation;
            set
            {
                lastStation = value;
            }
        }
        internal Areas Area
        {
            get => area;
            set
            {
                area = value;
            }
        }
        internal List<busLineStation> Path
        {
            get => path;
            set
            {
                path = value;
            }
        }
        public override string ToString()
        {
            
        }

    }
}
