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

        int id, fuel, distance;
        bool Dangerous;
        DateTimes startDate;

        public buses()
        {
            id = -1;
            fuel = 0;
            distance = 0;
            Dangerous = false;
            startDate.day = 0;
            startDate.month = 0;
            startDate.year = 0;
        }

       public void Maintenance()
        {
            
        }
        public void BusDangerous()
        {
            if (distance > 20000)
                Dangerous = true;

        }
    }
    partial class buses
    {

    }
}
