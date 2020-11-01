﻿using System;
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
        protected double latitude, longitude;//set up accessors for these two
        internal string Id
        {
            get => id;
        }
        internal double Latitude
        {
            get => latitude;
            set
            {
                if (!((value < -90) || (value > 90)))
                    latitude = value;
            }
        }
        internal double Longitude
        {
            get => latitude;
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
        internal busStation() 
        {
            id = "";
            address = "";
            latitude = NULL;
            longitude = NULL;
        }
        internal busStation(string id, double lat, double lon, string address="")
        {
            this.id = id;
            this.address = address;
            this.latitude = lat;
            this.longitude = lon;
        }
        internal static string ReadId()//read id from the user and returns a string
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            if (idst.Length != 6)
                throw new ArgumentException("invalid input: id can only contain 6 digits");
            return idst;
        }
        public override string ToString()
        {
            return $"ID:{this.id},LAT:{this.latitude},LONG:{this.longitude},ADDR:{this.address}";
        }   
    }
}