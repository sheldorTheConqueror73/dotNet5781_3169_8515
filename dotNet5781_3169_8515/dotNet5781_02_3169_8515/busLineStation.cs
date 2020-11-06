using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class busLineStation : busStation ,IComparable<busLineStation>
    {
        private int distance;
        private TimeSpan driveTime;

        public int Distance
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
        }

        internal busLineStation()
        {
            distance = -1;
            driveTime= new TimeSpan();
        }
        internal busLineStation(string _id):base(_id)
        {
            distance = -1;
            driveTime = new TimeSpan();
        }
        internal busLineStation(string id, float lat, float lon, string address = "") : base(id, lat, lon, address)
        {
            this.distance = 0;
            this.driveTime = new TimeSpan() ;
        }
        internal busLineStation(string id, float lat, float lon,int distance, TimeSpan time, string address= "") :base(id, lat, lon, address)
        {
            this.distance = distance;
            this.driveTime = time;
        }
        public static int readDistance()
        {
            int rdistance;
            Console.WriteLine("enter distance from last station:");
            bool sucsses = int.TryParse(Console.ReadLine(), out rdistance);
            if (rdistance <= 0)
                throw new ArgumentException("invalid input: distance must be bigger then 0.");
            return rdistance;
        }

        public static TimeSpan ReadTimeDrive()
        {
            Console.WriteLine(@"enter time travel from last station :");
             Console.WriteLine("enter hours: ");
            string inputHours = Console.ReadLine();
            for (int i = 0; i < inputHours.Length; i++)
                if (inputHours[i] > 57 || inputHours[i] < 48)
                    throw new ArgumentException("invalid input: can only be a positive number.");
            if(int.Parse(inputHours)<0)
                throw new ArgumentException("invalid input: can only be a positive number.");
            Console.WriteLine("enter minutes: ");
            string inputMinutes = Console.ReadLine();
            for (int i = 0; i < inputMinutes.Length; i++)
                if (inputMinutes[i] > 57 || inputMinutes[i] < 48)
                    throw new ArgumentException("invalid input: can only be a positive number.");
            if (int.Parse(inputMinutes) > 59|| int.Parse(inputMinutes) < 0)
                throw new ArgumentException("invalid input: can only be a between 1-59.");

            TimeSpan ts = new TimeSpan(int.Parse(inputHours), int.Parse(inputMinutes), 0);
            return ts;

        }

        public int CompareTo(busLineStation other)
        {
            if (other == null)
                return 1;
            else
                return this.id.CompareTo(other.Id);
        }

       
    }
}
