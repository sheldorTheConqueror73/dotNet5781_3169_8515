using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busStation
    {
        readonly static int NULL = -200;//not too sure about this - used to mark empty variable
        protected string id,address;
        protected int latitude, longitude;//set up accessors for these two

        internal busStation() 
        {
            id = "";
            address = "";
            latitude = NULL;
            longitude = NULL;
        }
        internal busStation(string id, int lat, int lon, string address="")
        {
            this.id = id;
            this.address = address;
            this.latitude = lat;
            this.longitude = lon;
        }


    }
}
