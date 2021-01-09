using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStation:BOobject
    {
        readonly static int NULL = -200;//not too sure about this - used to mark empty variable
       // protected readonly string id;
        protected string address;
        protected double latitude, longitude;//set up accessors for these two
        public string code { get; set; }
        public bool enabled { get; set; }
        public string  Name { get; set; }
        //getters and setters:
        public double Latitude
        { get; set; }
        public double Longitude
        { get; set; }
        public string Address
        { get; set; }
        public BusStation() //ctor
        {
            code = "";
            address = "";
           
            enabled = true;
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