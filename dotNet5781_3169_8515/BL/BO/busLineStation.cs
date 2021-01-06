﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLineStation : BusStation, IComparable<BusLineStation>
    {
        private double distance;
        private TimeSpan driveTime;
        public ObservableCollection<BusLine> ListOfLines { get; } = new ObservableCollection<BusLine>();

        //getters and setters:
        public double Distance
        {
            get => distance;
            set
            {
                if (value > 0)
                    distance = value;
            }
        }
        public TimeSpan DriveTime
        {
            get => driveTime;
            set { driveTime = value; }
        }

        public BusLineStation()//ctor
        {
            distance = -1;
            driveTime = new TimeSpan();
        }
        public BusLineStation(string _id) : base(_id)//ctor
        {
            distance = -1;
            driveTime = new TimeSpan();
        }
        public BusLineStation(string id, float lat, float lon, string address = "") : base(id, lat, lon, address)//ctor
        {
            this.distance = 0;
            this.driveTime = new TimeSpan();
        }
        public BusLineStation(string id, float lat, float lon, int distance, TimeSpan time, string address = "") : base(id, lat, lon, address)//ctor
        {
            this.distance = distance;
            this.driveTime = time;
        }

        public BusLineStation(BusLineStation bs) : base(bs)
        {
            this.distance = bs.Distance;
            this.driveTime = bs.DriveTime;
        }

        public static int readDistance()//read the distance from the user.
        {
            int rdistance;
            Console.WriteLine("enter distance from last station:");
            bool sucsses = int.TryParse(Console.ReadLine(), out rdistance);
            if (rdistance <= 0)
                throw new ArgumentException("invalid input: distance must be a number greater than 0.");
            return rdistance;
        }

        public static TimeSpan ReadTimeDrive()//read the time from the user.
        {
            Console.WriteLine(@"enter time travel from last station :");
            Console.WriteLine("enter hours: ");
            string inputHours = Console.ReadLine();
            for (int i = 0; i < inputHours.Length; i++)
                if (inputHours[i] > 57 || inputHours[i] < 48)
                    throw new ArgumentException("invalid input: can only be an integer.");
            if (int.Parse(inputHours) < 0)
                throw new ArgumentException("invalid input: can only be an integer.");
            Console.WriteLine("enter minutes: ");
            string inputMinutes = Console.ReadLine();
            for (int i = 0; i < inputMinutes.Length; i++)
                if (inputMinutes[i] > 57 || inputMinutes[i] < 48)
                    throw new ArgumentException("invalid input: can only be an integer .");
            if (int.Parse(inputMinutes) > 59 || int.Parse(inputMinutes) < 0)
                throw new ArgumentException("invalid input: can only be a between 1-59.");

            TimeSpan ts = new TimeSpan(int.Parse(inputHours), int.Parse(inputMinutes), 0);
            return ts;

        }

        public int CompareTo(BusLineStation other)//compare between two objects of busLineStation by Id.
        {
            if (other == null)
                return 1;
            else
                return this.id.CompareTo(other.id);
        }

        /* public override string ToString()
         {
             return $"{base.ToString()} Time: {driveTime.ToString(@"hh\:mm")}";
         }*/
         public override string ToString()
         {
             return $" {base.code}, {base.Address}";
         }


    }

}