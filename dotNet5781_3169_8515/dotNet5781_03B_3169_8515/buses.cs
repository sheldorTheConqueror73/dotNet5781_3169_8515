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
using System.Windows.Media;
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
        string iconPath;
        string idFormat;
        public const short FULL_TANK = 1200;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

      

        public buses()//ctor
        {
            id = "";
            fuel = 0; 
            distance = 0; 
            totalDistance = 0; 
            dangerous = false;
            registrationDate = new DateTime(0,0,0);
            lastMaintenance = new DateTime(0, 0, 0);
            iconPath = "/src/pics/okIcon.png";
            idFormat = formatId(id);

        }
        //ctor
        public buses(DateTime date, DateTime lm, string id = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready",Timerclass _timer=null, string path = "/src/pics/okIcon.png")//cotr
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
            iconPath = path;
            idFormat = formatId(id);           

        }
     
        //accessors
   
        public string IdFormat
        {
            get => idFormat;
            set
            {
                idFormat = value;
            }
        }
       public string Id
        {
            get => id;
            set { id= value;  }//maybe add date format check?
        }

        public int Fuel
        {
            get => fuel;
            set { this.fuel = value; }
        }

        public int Distance
        {
            get => distance;
            set { distance = value; }
        }
        public int TotalDistance
        {
            get => totalDistance;
            set { totalDistance  = value; }
        }

        public bool Dangerous
        {
            get => dangerous;
            set { dangerous = value; }
        }
        public DateTime LastMaintenance
        {
            get => lastMaintenance;
            set { lastMaintenance = value; }
        }
        public DateTime RegistrationDate
        {
            get => registrationDate;
            set { registrationDate = value; }
        }

        public Timerclass Timer
        {
            get => timer;
            set
            {
                this.timer = value;
                this.NotifyPropertyChanged("Timer");
            }
        }
        public string Status
        {
            get => status;
            set
            {
                if (value == "ready" || value == "mid-ride" || value == "refueling" || value == "maintenance"||value=="dangerous")
                {
                    status = value;
                }
            
            }
        }
        public string IconPath
        {
            get => iconPath;
            set { iconPath = value; }
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
            bool flag = DateTime.TryParse(Console.ReadLine(),out d1);
            if (!flag)
                throw new ArgumentException("invalid input: this is not a valid date");
            return d1;
        }

    }
    partial class buses
    {
        public bool CanMakeDrive(int km,bool sound)//return true if sleceted bus can drive that far.
        {
           
            UpdateDangerous();
            if (dangerous)
                throw new CannotDriveExecption("selected bus is unable to drive: bus is dangerous", Properties.Resources.Sad_Trombon,sound);
            if (fuel < km)
                throw new CannotDriveExecption("selected bus is unable to drive: not enough fuel", Properties.Resources.Sad_Trombon, sound);
            if ((distance + km) >= 20000)
                throw new CannotDriveExecption("selected bus is unable to drive: distance after drive exceeds maintnace limit", Properties.Resources.Sad_Trombon, sound);
            return true;

        }
        public void UpdateDangerous()//updates dangerous status of selected bus
        {
            if ((distance >= 20000) || (this.passedYearNowAndThen() == true))
            {
                this.dangerous = true;
                return;
            }
            this.dangerous= false;
        }

        public void UpdateMaintenance()//update last maintenance date to current day.
        {
            distance = 0;
            dangerous = false;
            this.lastMaintenance=DateTime.Now;

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
            if(this.id==_id)
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
            if (id.Length==7)
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
                if (this.timer.TimeNow == "00:00:00")
                    st = "";
                 else
                    st = this.timer.TimeNow;
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
        public static bool save(List<buses> ls1, string path,bool show=false) 
        {
            
            List<string> output = new List<string>();
            if(ls1.Count==0)
            { 
                MessageBoxResult result = MessageBox.Show("the list you are attempting to save is empty, would you still like to save it?", "caution", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                    return false;
            }
            foreach (buses bs1 in ls1)
            {
                output.Add($"{bs1.registrationDate.Year.ToString()},{bs1.registrationDate.Month.ToString()},{bs1.registrationDate.Day.ToString()},{bs1.lastMaintenance.Year.ToString()},{bs1.lastMaintenance.Month.ToString()},{bs1.lastMaintenance.Day.ToString()},{bs1.id},{(bs1.fuel).ToString()},{(bs1.distance).ToString()},{(bs1.dangerous).ToString()},{(bs1.totalDistance).ToString()},{bs1.status.ToString()},{bs1.timer.TimeNow},{bs1.iconPath}");//add timer storage?
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
        /// <summary>
        /// loads data from file to bus list
        /// </summary>
        /// <param name="ls1">list of buses to load</param>
        /// <param name="path">path of file</param>
        /// <param name="show">if show is true function will push notifications to user</param>
        /// <returns>true for sucess, false if failed</returns>
        public static bool load(ref List<buses> ls1,string path, bool show=false)
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
                ls1 = new List<buses>();
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
                    ls1.Add(new buses(d1, d2,  entries[6], fuel, distance, danger, total,entries[11],new Timerclass(Timerclass.convert(entries[12])),entries[13]));
                }
                if (show)
                   MessageBox.Show($"data successfully fetched from files. {ls1.Count} entries were retrieved.");
                return true;
         }
        /// <summary>
        /// sorts list by time left
        /// </summary>
        /// <param name="x">first timer string</param>
        /// <param name="y">second timer string</param>
        /// <returns>if x>y returns false, else returns ture</returns>
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


 }
