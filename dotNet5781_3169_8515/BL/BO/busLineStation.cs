using System;
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
        public BusLineStation(string id, float lat, float lon, string address = "") : base(id, lat, lon, address)//ctor
        {
            this.distance = 0;
            this.driveTime = new TimeSpan();
        }


        public BusLineStation(BusLineStation bs) : base(bs)
        {
            this.distance = bs.Distance;
            this.driveTime = bs.DriveTime;
        }

  

       

        public int CompareTo(BusLineStation other)//compare between two objects of busLineStation by Id.
        {
            if (other == null)
                return 1;
            else
                return this.id.CompareTo(other.id);
        }

         public override string ToString()
         {
             return $" {base.code}, {base.Address}";
         }


    }

}