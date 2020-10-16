using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    public struct DateTime
    {
        public int day;
        public int month;
        public int year;

        public void Date()
        {
           
            Console.WriteLine("enter day: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out day);
            Console.WriteLine("enter month: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out month);
            Console.WriteLine("enter year: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out year);

        }

    }

    partial class buses
    {
        const int LIMIT = 1200;
        int id, fuel, distance,totalDistance;
        bool dangerous;
        DateTime startDate, lastMaintenance;
          

        public buses()
        {
            id = -1;
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            startDate.day = 0;
            startDate.month = 0;
            startDate.year = 0;
            lastMaintenance.day = 0;
            lastMaintenance.month = 0;
            lastMaintenance.year = 0;

        }
        public buses(int id,int fuel, int distance, bool dangerous,int totalDistance, DateTime date, DateTime lm)
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.startDate = date;
            this.lastMaintenance= lm;
            this.totalDistance = totalDistance;
        }
        void setAllint(int id, int fuel, int distance, bool dangerous, int totalDistance, DateTime date, DateTime lm)
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.startDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        int getId() { return this.id; }
        void setId(int id) { this.id = id; }
        int getFuel() { return this.fuel; }
        void setFuel(int fuel) { this.fuel = fuel; }
        int getDistance() { return this.distance; }
        void setDistance(int distance) { this.distance = distance; }
        int getTotalDistance() { return this.totalDistance; }
        void setTotalDistance(int totalDistance) { this.totalDistance = totalDistance; }
        bool getDangerous() { return this.dangerous; }
        void setDangerous(bool dangerous) { this.dangerous = dangerous; }
        DateTime getStartDate() { return this.startDate; }
        void setDistance(DateTime startDate) { this.startDate = startDate; }
        DateTime getLastMaintenance() { return this.lastMaintenance; }
        void setLastMaintenance(DateTime lm) { this.lastMaintenance = lm; }
        
      

    }
    partial class buses
    {

    }
}
