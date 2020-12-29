using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus:BOobject
    {
        public class timerclass { };
        public const int FULL_TANK = 1200;

        public timerclass timer { get; set; }
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string plateNumber { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string iconPath { get; set; }

        public string idFormat { get; set; }
        public string status { get; set; }
        public Area area;
        public bool enabled { get; set; }




        public Bus()//ctor
        {
            plateNumber = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime();
            lastMaintenance = new DateTime();
            iconPath = "/src/pics/okIcon.png";
            enabled = true;
           // idFormat = formatId(id);

        }
        //ctor
        public Bus(DateTime date, DateTime lm, string plateNumber = "", int fuel = 0, int distance = 0, bool dangerous = false, int totalDistance = 0, string _status = "ready", timerclass _timer = null, string path = "/src/pics/okIcon.png")//cotr
        {
            this.plateNumber = plateNumber;
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
            enabled = true;
            //idFormat = formatId(id);

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
            if (this.plateNumber.Length == 7)
                space = "  ";
            return $"Id: {this.id} {space}  Status: {this.status} {st}";
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
        public void UpdateDangerous()//updates dangerous status of selected bus
        {
            if ((distance >= 20000) || (this.passedYearNowAndThen() == true))
            {
                this.dangerous = true;
                return;
            }
            this.dangerous = false;
        }



    }


}
