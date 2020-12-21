using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int fuel { get; set; }
        public int distance { get; set; }
        public int totalDistance { get; set; }
        public bool dangerous { get; set; }
        public string id { get; set; }
        public DateTime registrationDate { get; set; }
        public string lastMaintenance { get; set; }
        public string iconPath { get; set; }

        public string idFormat { get; set; }

        public class bus : IComparable
        {
            List<busLineStation> path = new List<busLineStation>();
            private busLineStation firstStation, lastStation;
            readonly string id;
            Area area;
            public bus(List<busLineStation> _path, string _id, busLineStation _firstStation, busLineStation _lastStation)//ctor
            {
                this.path = _path;
                this.id = _id;
                this.firstStation = _firstStation;
                this.lastStation = _lastStation;
            }
            public bus(List<busLineStation> _path, string _id, busLineStation _firstStation, busLineStation _lastStation, Area _area)//ctor
            {
                this.area = _area;
                this.path = _path;
                this.id = _id;
                this.firstStation = _firstStation;
                this.lastStation = _lastStation;
            }
            public bus(string _id)//ctor
            {
                this.path = new List<busLineStation>();
                this.id = _id;
                this.firstStation = null;
                this.lastStation = null;
            }

            public bus(bus bs)
            {
                this.area = bs.area;
                this.path = bs.path;
                this.id = bs.id;
                this.firstStation = bs.firstStation;
                this.lastStation = bs.lastStation;
            }



            public override string ToString()//print the bus details
            {
                string str = $"id:{this.id},AREA:{this.area},PATH:";
                int i = 0;
                foreach (busLineStation bs in path)
                {
                    str += bs.Id;
                    if (i != path.Count - 1)
                        str += " -> ";
                    i++;
                }

                //  need add the reverse list
                return str;
            }
            public void addStationBefore(busLineStation st, string _id)//add station beffore another station
            {
                utilityAddOrRemoveStation(st, _id, 1);
            }
            public void addStationAfter(busLineStation st, string _id)//add station after another station
            {
                utilityAddOrRemoveStation(st, _id, 2);
            }

            public void deleteStation(string _id)//delete station form the path
            {
                utilityAddOrRemoveStation(null, _id, 3);
            }
            public void utilityAddOrRemoveStation(busLineStation bs, string _id, int mode)//insert or remove station to the path list.
            {
                int i = 0;
                foreach (busLineStation station in path)
                {

                    if (station.Id == _id)
                    {
                        if (((path[0].Id == station.Id) && (mode == 1)) || ((path[path.Count - 1].Id == station.Id) && (mode == 2)))
                            throw new ArgumentException("station can added only between the outlet station to destination station.");
                        if (mode == 1)
                            path.Insert(i, bs);
                        else if (mode == 2)
                            path.Insert(i + 1, bs);
                        else if (mode == 3)
                            path.RemoveAt(i);
                        return;
                    }
                    i++;
                }
                throw new ArgumentException("error: the line doesnt exist in this line.");

            }

            public bool existStation(string _id)//check if station it's exist in the path list.
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
            public bool canIgetThere(string start, string end)//returns true if theres a path from start to end
            {
                bool found = false; ;
                foreach (var bst1 in path)
                {
                    if (bst1.Id == start)
                    {
                        found = true;
                    }
                    if ((found) && (bst1.Id == end))
                        return true;
                }
                return false;
            }

            public int distanceBetweenStations(string fStation, string secStation)//retun the distance between two station.
            {
                return utilityBetweenStations(fStation, secStation, 1);
            }

            public string convert(int num)//convert the time form minutes to HH:MM.
            {
                int hr = 0, min = num;
                hr = num / 60;
                min -= hr * 60;
                return $"Time: {hr}:{min}";

            }
            public int timeBetweenStations(string fStation, string secStation)//return the time between two station(in minutes).
            {
                return utilityBetweenStations(fStation, secStation, 2);
            }

            public bus subRoute(string fStation, string secStation)//return a bus object that contain the path from one station to another.
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

                tmp.firstStation = new busLineStation(first);
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

            public int utilityBetweenStations(string fStation, string secStaion, int mode)//the implementation of time/distance between stations. 
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
                if (first == "" || last == "")
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
                    {
                        sum += (path[i].DriveTime.Hours * 60) + (path[i].DriveTime.Minutes);
                        break;
                    }

                }
                return sum;

            }

            public int CompareTo(object obj)//compare between two objects of bus by there time.
            {
                string fStation, lStation;
                Console.WriteLine("enter Outlet Station:");
                fStation = Console.ReadLine();
                Console.WriteLine("enter Destination Station:");
                lStation = Console.ReadLine();
                return timeBetweenStations(fStation, lStation).CompareTo(((bus)obj).timeBetweenStations(fStation, lStation));
            }
        }



        //Timerclass timer;
    }
}
