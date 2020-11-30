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
        internal const short FULL_TANK = 1200;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

      
        internal Timerclass Timer
        {
            get => timer;
            set { timer = value;
               NotifyPropertyChanged(timer.TimeNow);
            }
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
            if (dangerous)
                throw new CannotDriveExecption("selected bus is unable to drive: bus is dangerous");
            if (fuel < km)
                throw new CannotDriveExecption("selected bus is unable to drive: not enough fuel");
            if ((distance + km) >= 20000)
                throw new CannotDriveExecption("selected bus is unable to drive: distance after drive exceeds maintnace limit");
            return true;

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
        internal static bool save(ObservableCollectionPropertyNotify<buses> ls1, string path,bool show=false)//write buspool list to file 
        {
            
            List<string> output = new List<string>();
            foreach (buses bs1 in ls1)
            {
                output.Add($"{bs1.registrationDate.Year.ToString()},{bs1.registrationDate.Month.ToString()},{bs1.registrationDate.Day.ToString()},{bs1.lastMaintenance.Year.ToString()},{bs1.lastMaintenance.Month.ToString()},{bs1.lastMaintenance.Day.ToString()},{bs1.id},{(bs1.fuel).ToString()},{(bs1.distance).ToString()},{(bs1.dangerous).ToString()},{(bs1.totalDistance).ToString()},{bs1.status.ToString()}");//add timer storage?
            }
            try
            {
                File.WriteAllLines(path, output.ToArray());
                if (show)
                    MessageBox.Show($"your data was saved successfully, {output.Count} entries were saved.");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        internal static bool load(ref ObservableCollectionPropertyNotify<buses> ls1,string path, bool show=false)//overwrites busepool list and updates it from text file
        {
            string[] arr;
            try
            {
                 arr = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            List<string> input = arr.ToList();
                ls1 = new ObservableCollectionPropertyNotify<buses>();
                foreach (var line in input)
                {
                    string[] entries = line.Split(',');
                    int fuel, distance, total;
                    bool flag = true, danger;
                    DateTime d2, d1;
                    DateTime.TryParse($"{entries[0]},{entries[1]},{entries[2]}", out d1);
                    DateTime.TryParse($"{entries[3]},{entries[4]},{entries[5]}", out d2);
                    Int32.TryParse(entries[7], out fuel);
                    Int32.TryParse(entries[8], out distance);
                    bool.TryParse(entries[9], out danger);
                    Int32.TryParse(entries[10], out total);
                    ls1.Add(new buses(d1, d2, entries[6], fuel, distance, danger, total,entries[11]));//maybe add timer?
                }
                if (show)
                   MessageBox.Show($"data successfully fetched from files. {ls1.Count} entries were retrieved.");
                return true;
         }
           
     }


 }
