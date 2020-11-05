using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class bus:IComparable
    { 
        List<busLineStation> path = new List<busLineStation>();
        private busLineStation firstStation, lastStation;
        readonly string id;
        Areas area;
        public bus(List<busLineStation> _path,string _id, busLineStation _firstStation, busLineStation _lastStation)
        {
            this.path = _path;
            this.id = _id;
            this.firstStation = _firstStation;
            this.lastStation = _lastStation;
        }
        public bus(string _id)
        {
            this.path =new List<busLineStation>();
            this.id =_id;
            this.firstStation = null;
            this.lastStation = null;
        }
        internal string Id
        {
            get => id;
        }
        internal busLineStation FirstStation
        {
            get => firstStation;
            set
            {
                firstStation = value;
            }
        }
        internal busLineStation LastStation
        {
            get => lastStation;
            set
            {
                lastStation = value;
            }
        }
        internal Areas Area
        {
            get => area;
            set
            {
                area = value;
            }
        }
        internal List<busLineStation> Path
        {
            get => path;
            set
            {
                path = value;
            }
        }
        public override string ToString()
        {
            string str = $"ID:{this.id},AREA:{this.area},PATH:";
            int i = 0;
            foreach(busLineStation bs in path)
            {
                str += bs.Id;
                if (i != path.Count - 1)
                    str += " -> ";
                i++;
            }
         
            //  need add the reverse list
            return str;
        }
        public void addStationBefore(busLineStation bs,string _id)
        {
            utilityAddOrRemoveStation(bs, _id, 1);
        }
        public void addStationAfter(busLineStation bs, string _id)
        {
            utilityAddOrRemoveStation(bs, _id, 2);
        }
      
        public void deleteStation(string _id)
        {
            utilityAddOrRemoveStation(null, _id, 3);
        }
        public void utilityAddOrRemoveStation(busLineStation bs, string _id, int mode)
        {
            int i = 0;
            foreach (busLineStation bsl in path)
            {

                if (bsl.Id == _id)
                {
                    if(mode==1)
                        path.Insert(i, bs);
                    else if(mode==2)
                        path.Insert(i + 1, bs);
                    else if(mode==3)
                        path.RemoveAt(i);
                    break;
                }
                i++;
            }
            throw new ArgumentException("error: the line doesnt exist in this line.");

        }

        public bool existStation(string _id)
        {
            foreach (busLineStation bsl in path)
            {              
                if (bsl.Id == _id)
                {
                    return true;
                }
            }
            return false;
        }
        internal bool canIgetThere(string start, string end)//returns true if theres a path from start to end
        {
            bool found = false; ;
            foreach(var bst1 in path)
            {
                if(bst1.Id==start)
                {
                    found = true;
                }
                if ((found) && (bst1.Id == end))
                    return true;
            }
            return false;
        }

        public int distanceBetweenStations(string fStation, string secStation)
        {
            return utilityBetweenStations(fStation, secStation, 1);
        }

        public int timeBetweenStations(string fStation, string secStation)
        {
            return utilityBetweenStations(fStation, secStation, 2);
        }

        public bus subRoute(string fStation,string secStation)
        {
            bus tmp = new bus(this.id);
            List<busLineStation> tmpLineStations = new List<busLineStation>();
            int index = 0;
            string first = "";
            string last = "";
            foreach (busLineStation bsl in path)
            {
                if (bsl.Id == fStation)
                {
                    first = fStation;
                    last = secStation;
                    break;
                }
                else if (bsl.Id == secStation)
                {
                    first = secStation;
                    last = fStation;
                    break;
                }
                index++;
            }
            if (first == "" || last == "")
                throw new ArgumentException("error: the stations do not exists.");

            tmp.firstStation =new busLineStation(first);
            for (int i = index; i < path.Count; i++)
            {
                if (path[i].Id != last)
                {
                    tmpLineStations.Add(new busLineStation(path[i].Id, path[i].Latitude, path[i].Longitude, path[i].Distance, path[i].DriveTime, path[i].Address));
                }
                else
                {
                    tmpLineStations.Add(new busLineStation(path[i].Id, path[i].Latitude, path[i].Longitude, path[i].Distance, path[i].DriveTime, path[i].Address));
                    break;
                }

            }
            tmp.lastStation = new busLineStation(last);
            tmp.area = this.area;
            tmp.path = tmpLineStations;
            return tmp;
            throw new ArgumentException("error: the stations do not exists.");

        }

        public int utilityBetweenStations(string fStation, string secStaion, int mode)
        {
            int sum = 0;
            int index = 0;
            string first = "";
            string last = "";
            foreach (busLineStation bsl in path)
            {
                if (bsl.Id == fStation)
                {
                    first = fStation;
                    last = secStaion;
                    break;
                }
                else if (bsl.Id == secStaion)
                {
                    first = secStaion;
                    last = fStation;
                    break;
                }
                index++;
            }
            if(first==""||last=="")
                throw new ArgumentException("error: the stations do not exists.");

            for (int i = index; i < path.Count; i++)
            {
                if (path[i].Id != last)
                {
                    if (mode == 1)
                        sum += path[i].Distance;
                    else
                        sum += (path[i].DriveTime.Hours * 60) + (path[i].DriveTime.Minutes);
                }
                else
                    break;
            }
            return sum;
            
        }

        public int CompareTo(object obj)//צריך לבדוק מה בדיוק ההשואה בין איזה תחנות
        {
            string fStation, lStation;
            Console.WriteLine("enter Outlet Station:");
            fStation = Console.ReadLine();
            Console.WriteLine("enter Destination Station:");
            lStation = Console.ReadLine();
            return timeBetweenStations(fStation,lStation).CompareTo(((bus)obj).timeBetweenStations(fStation, lStation));
        }
    }
}
