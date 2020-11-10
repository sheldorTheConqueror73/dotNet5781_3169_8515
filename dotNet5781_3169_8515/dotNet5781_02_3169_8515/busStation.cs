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
        protected readonly string id;
        protected string address;
        protected float latitude, longitude;//set up accessors for these two
        //getters and setters:
        internal string Id
        {
            get => id;
        }
        internal float Latitude
        {
            get => latitude;
            set
            {
                if (!((value < -90) || (value > 90)))
                    latitude = value;
            }
        }
        internal float Longitude
        {
            get => longitude;
            set
            {
                if (!((value < -180) || (value > 180)))
                    latitude = value;
            }   
        }
        internal string Address
        {
            get => address;
            set { address = value; }
        }
        internal busStation() //ctor
        {
            id = "";
            address = "";
            latitude = NULL;
            longitude = NULL;
        }
        internal busStation(string _id)//ctor
        {
            this.id = _id;
            address = "";
            latitude = NULL;
            longitude = NULL;
        }
        internal busStation(string id, float lat, float lon, string address="")//ctor
        {
            this.id = id;
            if (address == "")
                address = "Unnamed station";

            this.address = address;
            this.latitude = lat;
            this.longitude = lon;
        }
        internal static string ReadId()//read id from the user and returns a string
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            for (int i = 0; i < idst.Length; i++)
                if (idst[i] > 57 || idst[i] < 48)
                    throw new ArgumentException("invalid input: id can only contain 1-6 digits");

            if (idst.Length > 6|| idst.Length <1)
                throw new ArgumentException("invalid input: id can only contain 1-6 digits");
            return idst;
        }
        public override string ToString()
        {
            string str = $"{this.Address} ,";
            if (this.Address == "")
                str = "";
            string lat=(Latitude>0)?"N":"S";
            string lon = (Longitude > 0) ? "E" : "W";
            return $"{str}Bus Station Code: {Id}, {Latitude}°{lat}  {Longitude}°{lon}"; 
        }

    }
}
