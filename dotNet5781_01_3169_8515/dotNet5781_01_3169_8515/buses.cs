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
                 if (day < 1 || day > 31)
                     throw new ArgumentException();
               
               
                Console.WriteLine("enter month: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out month);
                    if(month<1||month>12)
                     throw new ArgumentException();

                 Console.WriteLine("enter year: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out year);
                 if(year<2000||year>2020)
                     throw new ArgumentException();



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
        DateTimes startDate, lastMaintenance;
          

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
        public buses(int id,int fuel, int distance, bool dangerous,int totalDistance, DateTimes date, DateTimes lm)
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.startDate = date;
            this.lastMaintenance= lm;
            this.totalDistance = totalDistance;
        }
        void setAllint(int id, int fuel, int distance, bool dangerous, int totalDistance, DateTimes date, DateTimes lm)
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
        DateTimes getStartDate() { return this.startDate; }
        void setDistance(DateTimes startDate) { this.startDate = startDate; }
        DateTimes getLastMaintenance() { return this.lastMaintenance; }
        void setLastMaintenance(DateTimes lm) { this.lastMaintenance = lm; }
        
    

    }
    partial class buses
    {
        //bool needRefuel=false;
        int refuelDistance=0;


        /* public void NeedRefueling()
         {
             if (refuelDistance > 1200)
                 needRefuel = true;
         }

         public void BusDangerous()
         {
             if (distance > 20000||passedYearNowAndThen())
                 dangerous = true;
         }*/

        // 'על פי התוכנית הראשית בקטע של 'אינו יכול לבצע נסיעה ועל פי סעיף ג

        public bool CanMakeDrive()
        {
            if (refuelDistance > 1200 || distance > 20000 || passedYearNowAndThen())
                return false;
            return true;
        }

        public void UpdateMaintenance()
        {
            distance = 0;
            DateTime currentDate = DateTime.Now;
            lastMaintenance.SetDay(currentDate.Day);
            lastMaintenance.SetMonth(currentDate.Month);
            lastMaintenance.SetYear(currentDate.Year);

        }
        public bool passedYearNowAndThen()
        {
            DateTime currentDate = DateTime.Now;
            if ((currentDate.Year - lastMaintenance.GetYear()) < 1)
                return false;
            if ((currentDate.Month - lastMaintenance.GetMonth()) < 0)
                return false;
            if ((currentDate.Day - lastMaintenance.GetDay()) < 0)
                return false;
            return true;
        } 

       
    }
}
