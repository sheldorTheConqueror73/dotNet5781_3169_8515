using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3169_8515
{
    partial class buses
    {
        int fuel;//how much fuel is left
        int distance;// distance since last maintenance
        int totalDistance;// total distance driven
        string id; //bus id number
        bool dangerous; //is this bus dangerous
        DateTime registrationDate, lastMaintenance;


        internal buses()
        {
            id = ""; // bus id number
            fuel = 0; //how much fuel is left
            distance = 0; // distance since last maintenance
            totalDistance = 0; // total distance driven
            dangerous = false;  //is this bus dangerous
            registrationDate = new DateTime(0,0,0);
            lastMaintenance = new DateTime(0, 0, 0);

        }
        internal buses(DateTime date, DateTime lm, string id="", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        internal void setAll(DateTime date, DateTime lm, string id, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        internal string getId() { return this.id; }
        internal void setId(string id) {  this.id = id; }
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
            Console.WriteLine("enter registration date:");
            DateTime d1;
            bool flag = DateTime.TryParse(Console.ReadLine(),out d1);
            if (!flag)
                throw new ArgumentException("invalid input: no such date");
            return d1;
        }

    }
    partial class buses
    {
        internal bool CanMakeDrive(int km)//return true if sleceted bus can drive that far.
        {
            UpdateDangerous();
            if (!dangerous)
                if ((fuel >= km) && ((distance + km) < 20000))
                    return true;
            return false;
        }
        internal void UpdateDangerous()
        {
            if ((distance >= 20000) || (this.passedYearNowAndThen() == false))
                this.dangerous= true;
            this.dangerous= false;
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
        internal bool EqualId(string _id)//checks if two buses types have the same id
        {
            if(this.id==_id)
            return true;
            return false;
        }
        //moved from main class.
        internal static string ReadId(int year, int mode)//read id from the user and return an int[]
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
            return idst;
        }
        internal static bool save(List<buses> ls1)//write buspool list to file 
        {
            string path = Environment.CurrentDirectory + "\\data.txt";
            List<string> output = new List<string>();
            foreach(buses bs1 in ls1 )
            {
                output.Add($"{bs1.registrationDate.Year.ToString()},{bs1.registrationDate.Month.ToString()},{bs1.registrationDate.Day.ToString()},{bs1.lastMaintenance.Year.ToString()},{bs1.lastMaintenance.Month.ToString()},{bs1.lastMaintenance.Day.ToString()},{bs1.id},{(bs1.fuel).ToString()},{(bs1.distance).ToString()},{(bs1.dangerous).ToString()},{(bs1.totalDistance).ToString()}");
            }
            try
            {
                File.WriteAllLines(path, output.ToArray());
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        internal static bool load(List<buses> ls1)//overwrites busepool list and updates it from text file
        {
            string path = Environment.CurrentDirectory + "\\data.txt";

            try
            { 
                string[] arr =File.ReadAllLines(path);
                List<string> input = arr.ToList();
                ls1 = new List<buses>();
                foreach(var line in input)
                {
                    string[] entries = line.Split(',');
                    int fuel,distance,total;
                    bool flag=true, danger;
                    DateTime d2,d1;
                    DateTime.TryParse($"{entries[0]},{entries[1]},{entries[2]}",out d1);
                    DateTime.TryParse($"{entries[3]},{entries[4]},{entries[5]}", out d2);
                    Int32.TryParse(entries[7], out fuel);
                    Int32.TryParse(entries[8], out distance);
                    bool.TryParse(entries[9], out danger);
                    Int32.TryParse(entries[10], out total);
                    ls1.Add(new buses(d1, d2, entries[6], fuel,distance, danger, total));
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}