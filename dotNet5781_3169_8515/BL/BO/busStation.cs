using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class busStation:BOobject
    {
        readonly static int NULL = -200;//not too sure about this - used to mark empty variable
       // protected readonly string id;
        protected string address;
        protected float latitude, longitude;//set up accessors for these two
        public string code { get; set; }
        public bool enabled { get; set; }
        //getters and setters:
        public float Latitude
        {
            get => latitude;
            set
            {
                if (!((value < -90) || (value > 90)))
                    latitude = value;
            }
        }
        public float Longitude
        {
            get => longitude;
            set
            {
                if (!((value < -180) || (value > 180)))
                    longitude = value;
            }
        }
        public string Address
        {
            get => address;
            set { address = value; }
        }
        public busStation() //ctor
        {
            code = "";
            address = "";
           // latitude = NULL;
           // longitude = NULL;
            enabled = true;
        }
        public busStation(string code)//ctor
        {
            this.code = code;
            address = "";
          //  latitude = NULL;
           // longitude = NULL;
            enabled = true;
        }
        public busStation(string code, float lat, float lon, string address = "")//ctor
        {
            this.code = code;
            if (address == "")
                address = "Unnamed station";

            this.address = address;
            this.latitude = lat;
            this.longitude = lon;
            enabled = true;
        }

        public busStation(busStation bs)
        {
            this.code = bs.code;
            this.id = bs.id;
            this.address = bs.Address;
            this.latitude = bs.Latitude;
            this.longitude = bs.Longitude;
            enabled = true;
        }
        public static string ReadId()//read id from the user and returns a string
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            for (int i = 0; i < idst.Length; i++)
                if (idst[i] > 57 || idst[i] < 48)
                    throw new ArgumentException("invalid input: id can only contain 1-6 digits");

            if (idst.Length > 6 || idst.Length < 1)
                throw new ArgumentException("invalid input: id can only contain 1-6 digits");
            return idst;
        }
        public override string ToString()
        {
            string str = $"{this.Address}  ";
            if (this.Address == "")
                str = "";
            string lat = (Latitude > 0) ? "N" : "S";
            string lon = (Longitude > 0) ? "E" : "W";
            return $"{str} Bus Station Code: {code}  {Latitude}°{lat}  {Longitude}°{lon}";
        }

    }
}