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
        { get; set; }
        public TimeSpan DriveTime
        { get; set; }

        public BusLineStation():base()//ctor
        {
            distance = 0;
            driveTime = new TimeSpan();
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