﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class busStation:DOobject
    {
        readonly static int NULL = -200;//not too sure about this - used to mark empty variable
       // public  string id { get; set; }
        protected string address;
        protected float latitude, longitude;//set up accessors for these two
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
                    latitude = value;
            }
        }
        public string Address
        {
            get => address;
            set { address = value; }
        }
        public busStation() //ctor
        {
            id = "";
            address = "";
            latitude = NULL;
            longitude = NULL;
            enabled = true;
        }
        public busStation(string _id)//ctor
        {
            this.id = _id;
            address = "";
            latitude = NULL;
            longitude = NULL;
            enabled = true;
        }
        public busStation(string id, float lat, float lon, string address = "")//ctor
        {
            this.id = id;
            if (address == "")
                address = "Unnamed station";

            this.address = address;
            this.latitude = lat;
            this.longitude = lon;
            enabled = true;
        }

        public busStation(busStation bs)
        {
            this.id = bs.id;
            this.address = bs.Address;
            this.latitude = bs.Latitude;
            this.longitude = bs.Longitude;
            this.enabled = bs.enabled;
        }
       
        public override string ToString()
        {
            string str = $"{this.Address}  ";
            if (this.Address == "")
                str = "";
            string lat = (Latitude > 0) ? "N" : "S";
            string lon = (Longitude > 0) ? "E" : "W";
            return $"{str} Bus Station Code: {id}  {Latitude}°{lat}  {Longitude}°{lon}";
        }

    }
}