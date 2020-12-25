using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
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
        public Area area;




        public Bus()//ctor
        {
            id = "";
            fuel = 0;
            distance = 0;
            totalDistance = 0;
            dangerous = false;
            registrationDate = new DateTime();
            lastMaintenance = new DateTime();
            iconPath = "/src/pics/okIcon.png";
           // idFormat = formatId(id);

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

 

    }


}
