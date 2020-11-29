using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using dotNet5781_03B_3169_8515.utility;

namespace dotNet5781_03B_3169_8515
{
    public partial class buses : INotifyPropertyChanged
    {
        string status;
        int fuel;//how much fuel is left
        int distance;// distance since last maintenance
        int totalDistance;// total distance driven
        string id; //bus id number
        bool dangerous; //is this bus dangerous
        DateTime registrationDate, lastMaintenance;
        Timerclass timer;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


       internal Timerclass Timer
        {
            get => timer;
            set { timer = value; }
        }
        internal buses()//ctor
        {
            id = "";
            fuel = 0; 
            distance = 0; 
            totalDistance = 0; 
            dangerous = false;
            registrationDate = new DateTime(0,0,0);
            lastMaintenance = new DateTime(0, 0, 0);

        }
        internal buses(DateTime date, DateTime lm, string id="", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)//cotr
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        internal buses(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0,string _status="ready")//cotr
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
            this.status = _status;
        }
        internal buses(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready",Timerclass _timer=null)//cotr
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
            this.status = _status;
            this.timer = new Timerclass(0);
            this.timer= _timer;
            
        }
        internal buses(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready",string _timer="")//cotr
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
            this.status = _status;
            if (_timer != "")
                this.timer = new Timerclass(double.Parse(_timer));
            else
                this.timer = null;
        }


        private void setAll(DateTime date, DateTime lm, string id, int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0)//set all mebmers at once
        {
            this.id = id;
            this.fuel = fuel;
            this.distance = distance;
            this.dangerous = dangerous;
            this.registrationDate = date;
            this.lastMaintenance = lm;
            this.totalDistance = totalDistance;
        }
        //accessors
   

       internal string Id
        {
            get => id;
            set { id= value;  }//maybe add date format check?
        }

        internal int Fuel
        {
            get => fuel;
            set { this.fuel = value; }
        }

        internal int Distance
        {
            get => distance;
            set { distance = value; }
        }
        internal int TotalDistance
        {
            get => totalDistance;
            set { totalDistance  = value; }
        }

        internal bool Dangerous
        {
            get => dangerous;
            set { dangerous = value; }
        }
        internal DateTime LastMaintenance
        {
            get => lastMaintenance;
            set { lastMaintenance = value; }
        }
        internal DateTime RegistrationDate
        {
            get => registrationDate;
            set { registrationDate = value; }
        }
        public string Status
        {
            get => status;
            set
            {
                if (value == "ready" || value == "mid-ride" || value == "refueling" || value == "maintenance")
                {
                    status = value;
                    this.NotifyPropertyChanged("Status");
                }
            
            }
        }
       
      
        internal void printId()//prints id
        {
            if (this.registrationDate.Year < 2018)
            {
                Console.WriteLine("ID:\t{0}{1}-{2}{3}{4}-{5}{6}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6]);
                return;
            }
            Console.WriteLine("ID:\t{0}{1}{2}-{3}{4}-{5}{6}{7}", this.id[0], this.id[1], this.id[2], this.id[3], this.id[4], this.id[5], this.id[6], this.id[7]);
        }
        internal void print()//prints id and  mileage since last maintenance
        {
            this.printId();
            Console.WriteLine("mileage since last maintenance:\t{0}\n", distance);
        }
        internal void printMileage()//prints total distance
        {
            Console.WriteLine("total mileage :\t{0}", this.totalDistance);
        }
        internal static DateTime readDate()//reads date from user
        {
            Console.WriteLine("enter registration date:");
            DateTime d1;
            bool flag = DateTime.TryParse(Console.ReadLine(),out d1);
            if (!flag)
                throw new ArgumentException("invalid input: this is not a valid date");
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
        internal void UpdateDangerous()//updates dangerous status of selected bus
        {
            if ((distance >= 20000) || (this.passedYearNowAndThen() == true))
            {
                this.dangerous = true;
                return;
            }
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
        internal static string ReadId(int year, int mode)//read id from the user and returns a string
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
        internal static string formatId(string id)//converts a string id to XXX-XX-XX or XX-XXX-XX 
        {
            if (id.Length==7)
            {
                return $"{id[0]}{id[1]}-{ id[2]}{id[3]}{ id[4]}-{id[5]}{id[6]}";
            }
            return $"{id[0]}{id[1]}{id[2]}-{id[3]}{id[4]}-{id[5]}{id[6]}{id[7]}";
        }
        public override string ToString()
        {
            string st = "";
            if (this.timer != null)
                if (this.timer.TimeNow == "00:00:00")
                    st = "";
                 else
                    st = this.timer.TimeNow;
            return $"Id: {this.id}   Status: {this.status} {st}";
        }


      
    }
}