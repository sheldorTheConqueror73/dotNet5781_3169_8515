using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3169_8515
{
    class bus
    { 
        List<busLineStation> path = new List<busLineStation>();
        private readonly string id;
        private string firstStation, lastStation;
        Areas area;
        public bus(List<busLineStation> _path,string _id, string _firstStation, string _lastStation)
        {
            this.path = _path;
            this.id = _id;
            this.firstStation = _firstStation;
            this.lastStation = _lastStation;
        }
        public bus(string _id)
        {
            this.path =null;
            this.id =_id;
            this.firstStation = null;
            this.lastStation = null;
        }
        internal string Id
        {
            get => id;
        }
        internal string FirstStation
        {
            get => firstStation;
            set
            {
                firstStation = value;
            }
        }
        internal string LastStation
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
            foreach(busLineStation bs in path)
            {
                str += bs.Id+"!";
            }
         
            //  need add the reverse list
            return str;
        }
        public void addStationBefore(busLineStation bs,string _id)
        {
            int i = 0;
            foreach(busLineStation bsl in path)
            {
                i++;
                if (bsl.Id == _id)
                {                 
                    path.Insert(i, bs);
                    break;
                }
            }
        }
        public void addStationAfter(busLineStation bs, string _id)
        {
            int i = 0;
            foreach (busLineStation bsl in path)
            {
               
                if (bsl.Id == _id)
                {
                    path.Insert(i+1, bs);
                    break;
                }
                i++;
            }
        }
        public void deleteStation(string _id)
        {
            int i = 0;
            foreach (busLineStation bsl in path)
            {
                
                if (bsl.Id == _id)
                {
                    path.RemoveAt(i);
                    break;
                }
                i++;
            }
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

        public int distanceBetweenStations(string id1,string id2)
        {
            return utilityBetweenStations(id1, id2, 1);
        }

        public int timeBetweenStations(string id1, string id2)
        {
            return utilityBetweenStations(id1, id2, 2);
        }

        public bus subRoute(string id1,string id2)
        {
            bus tmp = new bus(this.id);
            List<busLineStation> tmpLineStations = new List<busLineStation>();
            int index = 0;
            string first = "";
            string last = "";
            foreach (busLineStation bsl in path)
            {
                if (bsl.Id == id1)
                {
                    first = id1;
                    last = id2;
                    break;
                }
                else if (bsl.Id == id2)
                {
                    first = id2;
                    last = id1;
                    break;
                }
                index++;
            }
            if (first == "" || last == "")
                throw new ArgumentException("error: the stations are not exists.");

            tmp.firstStation = first;
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
            tmp.lastStation = last;
            tmp.area = this.area;
            tmp.path = tmpLineStations;
            return tmp;
            throw new ArgumentException("error: the stations are not exists.");

        }

        public int utilityBetweenStations(string id1, string id2, int mode)
        {
            int sum = 0;
            int index = 0;
            string first = "";
            string last = "";
            foreach (busLineStation bsl in path)
            {
                if (bsl.Id == id1)
                {
                    first = id1;
                    last = id2;
                    break;
                }
                else if (bsl.Id == id2)
                {
                    first = id2;
                    last = id1;
                    break;
                }
                index++;
            }
            if(first==""||last=="")
                throw new ArgumentException("error: the stations are not exists.");

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




    }
}
