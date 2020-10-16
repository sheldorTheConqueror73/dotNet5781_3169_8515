using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    public struct DateTimes
    {
        public int day;
        public int month;
        public int year;

        public DateTimes(int sum=0) 
        {

                Console.WriteLine("enter day: ");
                string input = Console.ReadLine();
                Int32.TryParse(input,out  day);
                //if (day < 1 || day > 31)
               
               
                Console.WriteLine("enter month: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out month);
                //if(month<1||month>12)

                Console.WriteLine("enter year: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out year);
               // if(year<2000||year>2020)
           
            

        }
        public int GetDay() { return this.day; }
        public int GetMonth() { return this.month; }
        public int GetYear() { return this.year; }
        public void SetDay(int _day) { this.day = _day; }
        public void SetMonth(int _month) { this.month = _month; }
        public void SetYear(int _year) { this.year = _year; }



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
