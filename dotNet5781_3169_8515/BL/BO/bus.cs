using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus:BOobject
    {
        public const int FULL_TANK = 1200;

        public TimeSpan time { get; set; }
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string plateNumber { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime lastMaintenance { get; set; }
        public string iconPath { get; set; }
        public string status { get; set; }
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
                status = "dangerous";
                return;
            }
            this.dangerous = false;
        }



    }


}
