using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    partial class buses
    {
        const int LIMIT = 1200;
        int fuel;//how much fuel is left
        int distance;// distance since last maintenance
        int totalDistance;// total distance driven
        int[] id; //bus id number
        bool dangerous; //is this bus dangerous
        DateTime registrationDate, lastMaintenance;


        internal buses()
        {
            id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 }; // bus id number
            fuel = 0; //how much fuel is left
            distance = 0; // distance since last maintenance
            totalDistance = 0; // total distance driven
            dangerous = false;  //is this bus dangerous
            registrationDate = new DateTime(0,0,0);
            lastMaintenance = new DateTime(0, 0, 0);

        }
        internal buses(DateTime date, DateTime lm, int[] id = null, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
        {
            if (id != null)
            {
                this.id = new int[8];
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            }
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
        internal void setAll(DateTime date, DateTime lm, int[] id = null, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
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
        internal int[] getId() { return this.id; }
        internal void setId(int[] id)
        {
            if (id != null)
                for (int i = 0; i < 8; i++)
                    this.id[i] = id[i];
            if (id == null)
            {
                id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }
        }
        internal int getFuel() { return this.fuel; }
        internal void setFuel(int fuel) { this.fuel = fuel; }
        internal int getDistance() { return this.distance; }
        internal void setDistance(int distance) { this.distance = distance; }
        internal int getTotalDistance() { return this.totalDistance; }
        internal void setTotalDistance(int totalDistance) { this.totalDistance = totalDistance; }
        internal bool getDangerous() { return this.dangerous; }
        internal void setDangerous(bool dangerous) { this.dangerous = dangerous; }
        internal DateTime getStartDate() { return this.registrationDate; }
        internal void setDistance(DateTime registrationDate) { this.registrationDate = registrationDate; }
        internal DateTime getLastMaintenance() { return this.lastMaintenance; }
        internal void setLastMaintenance(DateTime lm) { this.lastMaintenance = lm; }
        internal void printId()
        {
            if (this.registrationDate.Year < 2018)
            {
                Console.WriteLine("ID:\t{0}{1}-{2}{3}{4}-{5}{6}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6]);
                return;
            }
            Console.WriteLine("ID:\t{0}{1}{2}-{3}{4}-{5}{6}{7}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6], this.id[7]);
        }
        internal void print()
        {
            this.printId();
            Console.WriteLine("mileage:\t{0}", distance);
        }
        internal void printDistance()
        {
            Console.WriteLine("mileage since last maintenance:\t{0}", this.distance);
        }
        internal static DateTime readDate()
        {
            bool flag1,flag2,flag3;
            int day, year, month;
            Console.WriteLine("enter day:");
            flag1=Int32.TryParse(Console.ReadLine(), out day);
            Console.WriteLine("enter month:");
            flag2 = Int32.TryParse(Console.ReadLine(), out month);
            Console.WriteLine("enter year:");
            flag3 = Int32.TryParse(Console.ReadLine(), out year);
            if (!(flag1 && flag2 && flag3))
                throw new ArgumentException("error: failed to convert to int"); 
            ///=========================> add input vaildtion here
            DateTime d1 = new DateTime(year, month, day);
            return d1;
        }

    }
    partial class buses
    {
        internal bool CanMakeDrive(int km)//return true if sleceted bus can drive that far.
        {

            // if (this.dangerous == true)
            //  return false;
            if ((fuel >= km) && (distance + km <= 20000) && (this.passedYearNowAndThen() == false))
                return true;
            return false;
        }

        internal void UpdateMaintenance()//update last maintenance date to current day.
        {
            distance = 0;
            dangerous = false;
            this.lastMaintenance=DateTime.Now;

        }
        internal bool passedYearNowAndThen()//return true if a year has passed since the last maintenance.
        {
            DateTime currentDate = DateTime.Now;
            if ((currentDate.Year - this.lastMaintenance.Year) < 1)
                return false;
            if ((currentDate.Month - this.lastMaintenance.Month) < 0)
                return false;
            if ((currentDate.Day - this.lastMaintenance.Day) < 0)
                return false;
            return true;
        }

        internal bool EqualId(int[] _id)//checks if two buses types have the same id
        {
            for (int i = 0; i < this.id.Length; i++)
            {
                if (id[i] != _id[i])
                    return false;
            }
            return true;
        }

        //moved from main class.
        internal static int[] ReadId(int year, int mode)//read id from the user and return an int[]
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
            int[] arr = buses.ConvertStingIdToArr(idst);
            return arr;
        }

        internal static int[] ConvertStingIdToArr(string idst)//converts the input of readid(string) to int[]
        {
            int[] id = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            for (int i = 0; i < idst.Length; i++)
            {
                Int32.TryParse(idst[i].ToString(), out id[i]);
            }
            return id;
        }

        internal static string IdToString(int[] arr)// turns an int[] to  a string
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