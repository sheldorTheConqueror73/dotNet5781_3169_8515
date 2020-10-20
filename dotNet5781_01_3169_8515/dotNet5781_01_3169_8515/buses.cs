using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
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
                     throw new ArgumentException("invalid input: day cannot be greater than 31 or lesser than 1");
               
               
                Console.WriteLine("enter month: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out month);
                    if(month<1||month>12)
                     throw new ArgumentException("invalid input: month cannot be greater than 12 or lesser than 1");

                 Console.WriteLine("enter year: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out year);
                 if(year<2000||year>2020)
                     throw new ArgumentException("invalid input: year cannot be greater than 2020 or lesser than 2000");



        }
        public int GetDay() { return this.day; }
        public int GetMonth() { return this.month; }
        public int GetYear() { return this.year; }
        public void SetDay(int _day) { this.day = _day; }
        public void SetMonth(int _month) { this.month = _month; }
        public void SetYear(int _year) { this.year = _year; }
        
        public void set(DateTime d1)
        {
            this.day = d1.Day;
            this.month = d1.Month;
            this.year = d1.Year;
        }
        public static DateTimes current()
        {
            DateTime d1 = DateTime.Now;
            DateTimes d2 = new DateTimes();
            d2.set(d1);
            return d2;
        }


    }

    partial class buses
    {
        const int LIMIT = 1200;
        int fuel;//how much fuel is left
        int distance;// distance since last maintenance
        int totalDistance;// total distance driven
        int[] id; //bus id number
        bool dangerous; //is this bus dangerous
        DateTimes registrationDate, lastMaintenance;
          

        public buses()
        {
            id =  new int[8] { -1,-1,-1,-1,-1,-1,-1,-1 }; // bus id number
            fuel = 0; //how much fuel is left
            distance = 0; // distance since last maintenance
            totalDistance = 0; // total distance driven
            dangerous = false;  //is this bus dangerous
            registrationDate.day = 0;      
            registrationDate.month = 0;
            registrationDate.year = 0;
            lastMaintenance.day = 0;
            lastMaintenance.month = 0;
            lastMaintenance.year = 0;

        }
        public buses(DateTimes date, DateTimes lm, int[] id =null, int fuel=0, int distance=0, bool dangerous=false,int totalDistance=0 )
        {
            if (id != null)
            {
                this.id = new int[8];
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            }
            if(id==null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
         
            this.fuel = fuel; 
            this.distance = distance;
            this.dangerous = dangerous; 
            this.registrationDate = date;
            this.lastMaintenance= lm;
            this.totalDistance = totalDistance;
        }
       public void setAll(DateTimes date, DateTimes lm, int[] id = null, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
        {
            if (id != null)
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            if (id == null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        public int[] getId() { return this.id; }
        public void setId(int[] id)
        {
            if (id != null)
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            if (id == null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
        }
        public int getFuel() { return this.fuel; }
        public void setFuel(int fuel) { this.fuel = fuel; }
        public int getDistance() { return this.distance; }
        public void setDistance(int distance) { this.distance = distance; }
        public int getTotalDistance() { return this.totalDistance; }
        public void setTotalDistance(int totalDistance) { this.totalDistance = totalDistance; }
        public bool getDangerous() { return this.dangerous; }
        public void setDangerous(bool dangerous) { this.dangerous = dangerous; }
        public DateTimes getStartDate() { return this.registrationDate; }
        public void setDistance(DateTimes registrationDate) { this.registrationDate = registrationDate; }
        public DateTimes getLastMaintenance() { return this.lastMaintenance; }
        public void setLastMaintenance(DateTimes lm) { this.lastMaintenance = lm; }
        public void printId()
        {
            if(this.registrationDate.year<2018)
            {
                Console.WriteLine("ID:\t{0}{1}-{2}{3}{4}-{5}{6}",this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6]);
                return;
            }
            Console.WriteLine("ID:\t{0}{1}{2}-{3}{4}-{5}{6}{7}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6],this.id[7]);
        }
        public void print()
        {
            this.printId();
            Console.WriteLine("mileage:\t{0}",distance);
        }
        public void printDistance()
        {
            Console.WriteLine("mileage since last maintenance:\t{0}",this.distance);
        }
         /*
        public ref buses find(int[] id,ref List<buses> ls1)///need to ask mr gerbergergerger//work in progress
        {
            foreach (buses bs in ls1)
            {
                if(bs.EqualId(id))
                {
                    bs.
                }
            }
                throw new ArgumentException("return code 404: no match");
        }
         */
    }
    partial class buses
    {
        public bool CanMakeDrive(int km)//return true if sleceted bus can drive that far.
        {
            
           // if (this.dangerous == true)
              //  return false;
            if ((fuel >= km) && (distance + km <= 20000)&&(this.passedYearNowAndThen()==false))
                return true;
            return false;
        }

        public void UpdateMaintenance()//update last maintenance date to current day.
        {
            distance = 0;
            dangerous = false;
            this.lastMaintenance.set(DateTime.Now);

        }
        public bool passedYearNowAndThen()//return true if a year has passed since the last maintenance.
        {
            DateTime currentDate = DateTime.Now;
            if ((currentDate.Year - this.lastMaintenance.GetYear()) < 1)
                return false;
            if ((currentDate.Month - this.lastMaintenance.GetMonth()) < 0)
                return false;
            if ((currentDate.Day - this.lastMaintenance.GetDay()) < 0)
                return false;
            return true;
        } 

       public bool EqualId(int[]_id)//checks if two buses types have the same id
        {
            for (int i = 0; i < this.id.Length; i++)
            {
                if (id[i] != _id[i])
                    return false;
            }
            return true;
        }

        //moved from main class.
        public static int[] ReadId(int year, int mode)//read id from the user and return an int[]
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            if (idst.Length != 8 && idst.Length != 7)
                throw new ArgumentException("invalid input: id  must be 7 or 8 digits");
            for (int i = 0; i < idst.Length; i++)
                if (idst[i] > 57 || idst[i] < 48)
                    throw new ArgumentException("invalid input: id cannot be a letter");
            if (mode == 0)
            {
                if ((idst.Length == 8 && year < 2018) || (idst.Length == 7 && year >= 2018))
                    throw new ArgumentException("invalid input: id format doesn't match registration date");
            }
            /*else if (mode == 1)
            {
                return idst;
            }
            */
            // return idst;
            int[] arr=buses.ConvertStingIdToArr(idst);
            return arr;
        }
            
        public static int[] ConvertStingIdToArr(string idst)//converts the input of readid(string) to int[]
        {
            int[] id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            for (int i = 0; i < idst.Length; i++)
            {
                Int32.TryParse(idst[i].ToString(), out id[i]);
            }
            return id;
        }

        public static string IdToString(int[] arr)// turns an int[] to  a string
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != -1)
                {
                    str += (char)(arr[i] + (int)'0');//make sure this stands to regulations
                }
            }
            return str;
        }
    }
}   
