using DalApi.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus 
    {
        public class timerclass { };
        public static int FULL_TANK = 1200;

        public timerclass timer { get; set; }
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string id { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string iconPath { get; set; }

        public string idFormat { get; set; }
        public string status { get; set; }
        public  Area area;




        public Bus()//ctor
        {
            id = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime(0, 0, 0);
            lastMaintenance = new DateTime(0, 0, 0);
            iconPath = "/src/pics/okIcon.png";
            idFormat = formatId(id);

        }
        //ctor
        public Bus(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready", timerclass _timer = null, string path = "/src/pics/okIcon.png")//cotr
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
            this.status = _status;
            this.timer = new timerclass();
            this.timer = _timer;
            iconPath = path;
            idFormat = formatId(id);

        }


        public void printId()//prints id
        {
            if (this.registrationDate.Year < 2018)
            {
                Console.WriteLine("ID:\t{0}{1}-{2}{3}{4}-{5}{6}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6]);
                return;
            }
            Console.WriteLine("ID:\t{0}{1}{2}-{3}{4}-{5}{6}{7}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6], this.id[7]);
        }
        public void print()//prints id and  mileage since last maintenance
        {
            this.printId();
            Console.WriteLine("mileage since last maintenance:\t{0}\n", distance);
        }
        public void printMileage()//prints total distance
        {
            Console.WriteLine("total mileage :\t{0}", this.totalDistance);
        }
        public static DateTime readDate()//reads date from user
        {
            Console.WriteLine("enter registration date:");
            DateTime d1;
            bool flag = DateTime.TryParse(Console.ReadLine(), out d1);
            if (!flag)
                throw new ArgumentException("invalid input: this is not a valid date");
            return d1;
        }

    
        public bool CanMakeDrive(int km, bool sound)//return true if sleceted bus can drive that far.
        {

            UpdateDangerous();
            if (dangerous)
                throw new CannotDriveExecption("selected bus is unable to drive: bus is dangerous");
            if (fuel < km)
                throw new CannotDriveExecption("selected bus is unable to drive: not enough fuel");
            if ((distance + km) >= 20000)
                throw new CannotDriveExecption("selected bus is unable to drive: distance after drive exceeds maintnace limit");
            return true;

        }
        public void UpdateDangerous()//updates dangerous status of selected bus
        {
            if ((distance >= 20000) || (this.passedYearNowAndThen() == true))
            {
                this.dangerous = true;
                return;
            }
            this.dangerous = false;
        }

        public void UpdateMaintenance()//update last maintenance date to current day.
        {
            distance = 0;
            dangerous = false;
            this.lastMaintenance = DateTime.Now;

        }
        public bool passedYearNowAndThen()//return true if a year has passed since the last maintenance.
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
        public bool EqualId(string _id)//checks if two buses types have the same id
        {
            if (this.id == _id)
                return true;
            return false;
        }
        //moved from main class.
        public static string ReadId(int year, int mode)//read id from the user and returns a string
        {
            Console.WriteLine("enter id: ");
            string idst = Console.ReadLine();
            if (idst.Length != 8 && idst.Length != 7)
                throw new ArgumentException("invalid input: id can only contain 7 or 8 digits");
            for (int i = 0; i < idst.Length; i++)
                if (idst[i] > 57 || idst[i] < 48)
                    throw new ArgumentException("invalid input: id can only contain 7 or 8 digits");
            if (mode == 0)
            {
                if ((idst.Length == 8 && year < 2018) || (idst.Length == 7 && year >= 2018))
                    throw new ArgumentException("invalid input: id format doesn't match registration date");
            }
            return idst;
        }
        public static string formatId(string id)//converts a string id to XXX-XX-XX or XX-XXX-XX 
        {
            if (id.Length == 7)
            {
                return $"{id[0]}{id[1]}-{ id[2]}{id[3]}{ id[4]}-{id[5]}{id[6]}";
            }
            return $"{id[0]}{id[1]}{id[2]}-{id[3]}{id[4]}-{id[5]}{id[6]}{id[7]}";
        }
        //to string function
        public override string ToString()
        {
            string st = "";
            if (this.timer != null)
                if (this.timer == null)//change back
                    st = "";
                else
                    st = null;
            string space = "";
            if (this.id.Length == 7)
                space = "  ";
            return $"Id: {this.id} {space}  Status: {this.status} {st}";
        }
        /// <summary>
        /// saves  alist of buses to file
        /// </summary>
        /// <param name="ls1">list of buses to save</param>
        /// <param name="path">path of file</param>
        /// <param name="show">if show is true function will push notifications to user</param>
        /// <returns>true for sucess, false if failed</returns>
       
        public static bool sortTime(string x, string y)
        {
            if (x != null && y == null)
                return true;
            if (y != null && x == null)
                return false;
            string[] data = x.Split(':');
            double comp1 = ((double.Parse(data[0]) * 3600) + (double.Parse(data[1]) * 60) + (double.Parse(data[2])));
            data = y.Split(':');
            double comp2 = ((double.Parse(data[0]) * 3600) + (double.Parse(data[1]) * 60) + (double.Parse(data[2])));
            if (comp1 > comp2)
                return false;
            else
                return true;
        }
        /// <summary>
        /// sorts list by status
        public static bool sortStatus(string x, string y)
        {
            if (x == "ready" && (y == "mid-ride" || y == "refueling" || y == "maintenance" || y == "dangerous"))
                return false;
            if (x == "mid-ride" && (y == "refueling" || y == "maintenance" || y == "dangerous"))
                return false;
            if ((x == "mid-ride" || x == "refueling" || x == "maintenance") && y == "dangerous")
                return false;
            if (x == "ready" && y == "ready")
                return false;
            return true;
        }

    }




    //Timerclass timer;
}

