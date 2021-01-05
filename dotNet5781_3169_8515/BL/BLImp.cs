using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using System.Reflection;
using System.IO;

namespace BL
{

    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();   

       
        #region bus
        public void addBus(Bus bus)
        {

            bus.formatPlateNumber();
            dl.addBus(Utility.BOtoDOConvertor<DO.Bus, BO.Bus>(bus));
        }

        public void updateBus(Bus bus)
        {
            bus.UpdateDangerous();
            bus.formatPlateNumber();
            dl.updateBus(Utility.BOtoDOConvertor<DO.Bus, BO.Bus>(bus));
        }
        public void removeBus(int id)
        {
            dl.removeBus(id);
        }

        public Bus GetBus(int id)
        {
            return Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(dl.GetBus(id));
        }

        public List<Bus> GetAllBuses(int order=0)
        {
            var result = dl.GetAllBuses();
            if (result != null)
            {
                if(order==0)
                return (from item in result
                        where (item != null && item.enabled == true)
                        orderby item.plateNumber ascending
                        select Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
                if (order == 1)
                    return (from item in result
                            where (item != null && item.enabled == true)
                            orderby item.status ascending, item.plateNumber ascending
                            select Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
            }
            return default;
            //DOBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public void refuel(int id)
        {
            var bus = this.GetBus(id);
            if (bus.status != "ready" && bus.status != "dangerous")
                throw new BusBusyException("Bus is currently Busy");
            dl.refuel(id);
        }
        public void maintain(int id)
        {
            
            var bus = this.GetBus(id);
            if (bus.status != "ready" && bus.status != "dangerous")
                throw new BusBusyException("Bus is currently Busy");
            dl.maintain(id);
        }
        #endregion

        #region lines
        public busLine GetBusLine(int id)
        {
            throw new NotImplementedException();
        }
        
        
        public IEnumerable<busLine> GetAllbusLines()
        {
             var result = dl.GetAllbusLines();
                 if(result!=null)
                 return from item in result 
                        where item!=null && item.enabled == true
                        orderby int .Parse(item.number) ascending
                    select (Utility.DOtoBOConvertor<BO.busLine,DO.busLine>(item));
            return default;

        }

        public IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate)
        {
            throw new NotImplementedException();
        }

        public void removeLine(int id)
        {
            dl.removeFollowStation(id);
            dl.removeLineInStation(id);
            dl.removeLine(id);
        }


        public void updateLine(int id,string number, int area, List<BO.busLineStation> path, List<double> distance, List<TimeSpan> time)
        {
            this.removeLine(id);
            this.addLine(number, area, path, distance, time);
        }
        public void addLine(string number, int area, List<BO.busLineStation> path, List<double> distance, List<TimeSpan> time)
        {
            int count = dl.countLines(number);
            if (count == 2)
                throw new BusLimitExceededExecption("There are already two bus with that number");
            if(count==1)
            {
                int id = dl.GetBusLineID(number);
                var result = (from lis in dl.GetAllLineInStation()
                             where lis.Lineid==id
                             orderby lis.placeOrder ascending
                             select lis).ToList();
                if(result[0].id!=path[path.Count-1].id || result[result.Count-1].id!=path[0].id)
                    throw new BusLimitExceededExecption($"The second {number} line bust be going in the oppesite diraction");
            }
            busLine line = new busLine() { number = number, area = (Area)area, enabled=true };   
            dl.addLine(Utility.BOtoDOConvertor<DO.busLine, BO.busLine>(line));
            for(int i=0;i<path.Count;i++)
            {
                dl.addLineInStation(new DO.lineInStation() { stationid=path[i].id, Lineid=line.id, placeOrder=i,lineNumber=line.number });
                if(i!=path.Count-1)
                {
                    dl.addFollowStation(new DO.followStations() { firstStationid=path[i].id, enabled=true,distance=distance[i], driveTime=time[i],lineId=line.id,secondStationid=path[i+1].id ,lineNumber=line.number});
                }
            }
        
        }

        public void addLine(BusStation station)
        {
            throw new NotImplementedException();
        }

        public void addLine(busLineStation line)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region lineInStation
        public IEnumerable<string> GetAllLinesInStation(int id)
        {
            int[] arr = new int[2000];
            for (int i = 0; i < 2000; i++)
                arr[i] = 0;
            List<string> bla = new List<string>();
            int g = 0,r=0;
            var rt = GetAllFollowStationsAsStationsObj(id);
            foreach (var st in rt)
            {;
                arr[int.Parse(st.code)]++;
                foreach (var item in arr)
                {
                    if (item == 2)
                        r = 9;
                }
                g++;
            }
            List<string> linInSta = new List<string>();
            foreach (var sta in GetAllFollowStationsAsStationsObj(id))
                foreach (var folSta in dl.GetAllFollowStation())
                    if (folSta != null && folSta.enabled == true && folSta.firstStationid == id && folSta.secondStationid == sta.id)
                    {
                        linInSta.Add(folSta.lineNumber);
                        break;
                    }
            foreach (var linSta in dl.GetAllLineInStation())
                if (!linInSta.Any(x => x == linSta.lineNumber)&&linSta.stationid==id)
                    linInSta.Add(linSta.lineNumber);
            return linInSta.GroupBy(x => x).Select(y => y.First());
        }
       public void reconstructTimeAndDistance(int lineID, out List<double> distance, out List<TimeSpan> time)
        {
            distance = new List<double>();
            time = new List<TimeSpan>();
            var result = from station in dl.GetAllLineInStation()
                         where station != null && station.Lineid == lineID
                         orderby station.placeOrder ascending
                         select station;
            var result2 = from item in dl.GetAllFollowStation()
                          where item != null && item.enabled == true && item.lineId == lineID
                          select item;
            for(int i=1;i<result.Count();i++)
            {
                foreach (var element2 in result2)
                {
                    if(result.ToList()[i].stationid==element2.secondStationid)
                    {
                        time.Add(element2.driveTime);
                        distance.Add(element2.distance);
                        break;
                    }
                }
            }


        }
        #endregion

        #region station
        public void addStation(busLineStation station)
        {            
            dl.addStation(Utility.BOtoDOConvertor<DO.busLineStation, BO.busLineStation>(station));
        }
        public IEnumerable<busLineStation> GetAllbusLineStation()
        {
            var result = dl.GetAllbusLineStation();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        orderby item.code ascending
                        select Utility.DOtoBOConvertor<BO.busLineStation, DO.busLineStation>(item)).ToList();
            return default;
        }

        public IEnumerable<busLine> GetAllbusLineStationBy(Predicate<busLineStation> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<busLineStation> GetAllStationInLine(int id)
        {
            //var line=DOtoBOConvertor<BO.busLine, DO.busLine>(dl.GetBusLine(id));
            if(dl.GetAllbusLineStation()!=null&& dl.GetAllLineInStation()!=null)
            return (from sta in dl.GetAllbusLineStation() where sta != null && sta.enabled == true 
                         from linInSta in dl.GetAllLineInStation() where linInSta != null && linInSta.Lineid == id && linInSta.stationid == sta.id
                         orderby linInSta.placeOrder ascending
                        select Utility.DOtoBOConvertor<BO.busLineStation, DO.busLineStation>(sta)).ToList();
            return default;
        }
        public IEnumerable<BusStation> GetAllbusStations()
        {
            throw new NotImplementedException();

        }
        public busLineStation GetbusLineStation(int id)
        {
            throw new NotImplementedException();
        }

        public BusStation GetbusStation(int id)
        {
            throw new NotImplementedException();
        }

        public void removebusLineStation(int id)
        {
            throw new NotImplementedException();
        }
        public void removeStation(int id)
        {
            var v1=(from fl in dl.GetAllFollowStation()
                   where fl.firstStationid==id
                   select fl.lineId);
            int idsta1 = 0,idsta2=0,idfol1=0,idfol2=0;
            double dist = 0;
            bool flagFirst = false, flagSecond=false;
            TimeSpan ts=new TimeSpan();
            if(v1.Count()!=0)
            foreach(var lin in dl.GetAllbusLines())
            {
                if (v1.Any(b => b == lin.id))
                {
                     flagFirst = false; flagSecond = false;
                    foreach (var folsta in dl.GetAllFollowStation())
                    {
                        if (folsta.secondStationid == id && folsta.lineId == lin.id)
                        {
                            idsta1 = folsta.firstStationid;
                            idfol1 = folsta.id;
                            dist += folsta.distance;
                            ts += folsta.driveTime;
                            flagFirst = true;
                        }
                        if (folsta.firstStationid == id && folsta.lineId == lin.id)
                        {
                             idsta2 = folsta.secondStationid;
                             idfol2 = folsta.id;
                             dist += folsta.distance;
                             ts += folsta.driveTime;
                             flagSecond = true;
                        }
                    }
                        if (flagFirst && flagSecond)
                        {
                            dl.updateFollowStation(new DO.followStations() { id = idfol1, firstStationid = idsta1, secondStationid = idsta2, lineId = lin.id, distance = dist, driveTime = ts, enabled = true,lineNumber=lin.number });
                            dl.removeFollowStationByIdOfFol(idfol2);

                        }
                    }
            }
            dl.removebusLineStation(id);

        }


        public void listToText()
        {
            string LineInsta="", lin="", folSta = "";
            lin = " Lines = new List<busLine>{";
            int cnt = 0;
            foreach (var v1 in dl.GetAllbusLines())
            {
                lin += "new busLine(){"+$"number=\"{v1.number}\",id={v1.id},area=Area.{v1.area},driveTime=\"{v1.driveTime}\",enabled=true"+"}";
                if (cnt != dl.GetAllbusLines().Count()-1)
                    lin += ",\n";
                cnt++;
            }
            cnt = 0;
            lin += "};\n\n\n";
            lin += "####################################################################################\n\n";
            LineInsta = "lineInStations = new List<lineInStation>{";
            foreach (var v1 in dl.GetAllLineInStation())
            {
                LineInsta += "new lineInStation(){" + $"id={v1.id},stationid={v1.stationid},Lineid={v1.Lineid},placeOrder={v1.placeOrder}" + "}";
                if (cnt != dl.GetAllLineInStation().Count() - 1)
                    LineInsta += ",\n";
                cnt++;
            }
            LineInsta += "};\n\n";
            LineInsta += "####################################################################################\n\n";

            cnt = 0;
            folSta = "followStation = new List<followStations>{";
            foreach (var v1 in dl.GetAllFollowStation())
            {
                folSta += "new followStations(){" + $"id={v1.id},lineId={v1.lineId},secondStationid={v1.secondStationid},firstStationid={v1.firstStationid},enabled=true,driveTime=TimeSpan.Parse(\"{v1.driveTime}\"),distance={v1.distance}" + "}";
                if (cnt != dl.GetAllFollowStation().Count() - 1)
                    folSta += ",\n";
                cnt++;
            }
            folSta += "};";
            File.WriteAllText( "C:\\Users\\LENOVO\\source\\repos\\sheldorTheConqueror73\\dotNet5781_3169_8515\\dotNet5781_3169_8515\\initList.txt",lin + LineInsta + folSta);
        }
        public void updatebusLineStation(busLineStation line)
        {
            throw new NotImplementedException();
        }

        public void updateStation(busLineStation station)
        {
            dl.updatebusLineStation(Utility.BOtoDOConvertor<DO.busLineStation, BO.busLineStation>(station));
        }
        public IEnumerable<BusStation> GetAllbusStationsBy(Predicate<BusStation> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<busLineStation> GetAllStationNotInLine(int id)
        {
            if(dl.GetAllbusLineStation() != null && dl.GetAllLineInStation() != null)
            return (from sta in dl.GetAllbusLineStation()
                    where sta != null && sta.enabled == true
                    from linInSta in dl.GetAllLineInStation()
                    where linInSta != null && linInSta.Lineid != id && linInSta.stationid == sta.id
                    orderby linInSta.placeOrder ascending
                    select Utility.DOtoBOConvertor<BO.busLineStation, DO.busLineStation>(sta)).ToList();
            return default;
        }
        #endregion


        #region followStations
        private IEnumerable<followStations> GetAllFollowStations(int id)
        {
            var folllowStation = dl.GetAllFollowStation();
            if (folllowStation != null)
                return (from folSta in folllowStation
                        where folSta != null && folSta.enabled == true && folSta.firstStationid==id
                        select Utility.DOtoBOConvertor<BO.followStations, DO.followStations>(folSta)).ToList();
            return default;
        }
        public IEnumerable<busLineStation> GetAllFollowStationsAsStationsObj(int id)
        {
            var folllowStation = GetAllFollowStations(id);
            var stations = dl.GetAllbusLineStation();
            if (folllowStation != null&& stations!=null)
                return (from sta in stations
                        from folSta in folllowStation where folSta != null && folSta.enabled == true                    
                        where sta!=null&&sta.enabled==true && folSta.firstStationid == id &&folSta.secondStationid==sta.id
                        let x=sta.Distance=folSta.distance
                        let y=sta.DriveTime=folSta.driveTime
                        select (Utility.DOtoBOConvertor<BO.busLineStation, DO.busLineStation>(sta))).ToList();
            return default;
        }

        public void updateFollowStation(followStations folStation)
        {
            dl.updateFollowStation(Utility.BOtoDOConvertor<DO.followStations, BO.followStations>(folStation));
        }

        public int GetIdFollowStationBy(int idFirstSta, int idSecondSta,int idLine)
        {
            foreach (var folSta in dl.GetAllFollowStation())
            {
                if (folSta.firstStationid == idFirstSta && folSta.secondStationid == idSecondSta && folSta.lineId == idLine)
                    return folSta.id;
            }
            return default;
        }
        #endregion
        #region user
        public IEnumerable<BO.User> GetAllUsers()
        {
            var result = dl.GetAllbusUsers();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        select Utility.DOtoBOConvertor<BO.User, DO.User>(item)).ToList();
            return default;
        }
        public BO.User GetUser(int id)
        {
            try 
            {
                return Utility.DOtoBOConvertor<BO.User, DO.User>(dl.GetUser(id));            
            }
            catch (Exception e)
            {
                throw new noMatchExeption(e.Message);
            }
          
        }
        public string authenticate(string username, string password, out int id)
        {

            foreach (var user in this.GetAllUsers())
                if (user.enabled == true && user.name == username && user.password == password)
                {
                    id = user.id;
                    return user.accessLevel.ToString();
                }
            id =-1;
            throw new credentialsIncorrectException("Inncorrect Credentials. please try again");
        }
        public void addUser(BO.User user)
        {
            user.accessLevel ="User";
            dl.addUser(Utility.BOtoDOConvertor<DO.User,BO.User>(user));
        }
        public void removeUser(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
      
    }
}
