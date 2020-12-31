using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using System.Reflection;
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


        public void updateLine(busLine line)
        {
            throw new NotImplementedException();
        }
        public void addLine(string number, int area, List<BO.busLineStation> path, List<int> distance, List<TimeSpan> time)
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
                dl.addLineInStation(new DO.lineInStation() { stationid=path[i].id, Lineid=line.id,Address=path[i].Address, placeOrder=i });
                if(i!=path.Count-1)
                {
                    dl.addFollowStation(new DO.followStations() { firstStationid=path[i].id, enabled=true,distance=distance[i], driveTime=time[i],lineId=line.id,secondStationid=path[i+1].id });
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
        public IEnumerable<busLine> GetAllLinesInStation(int id)
        {
            var lines = dl.GetAllbusLines();
            var resultStaInLine = dl.GetAllLineInStation();
            if (lines != null && resultStaInLine != null)
                return (from item in lines
                        from item2 in resultStaInLine
                        where item != null && item.enabled == true && item.id == item2.Lineid && item2.stationid == id
                        select Utility.DOtoBOConvertor<BO.busLine, DO.busLine>(item)).ToList();
            return default;
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
            var v1=from fl in dl.GetAllFollowStation()
                   where fl.firstStationid==id
                   select fl.lineId;
            int idsta1 = 0,idsta2=0,idfol=0,dist=0;
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
                            idfol = folsta.id;
                            dist += folsta.distance;
                            ts += folsta.driveTime;
                                flagFirst = true;
                        }
                        if (folsta.firstStationid == id && folsta.lineId == lin.id)
                        {
                            idsta2 = folsta.secondStationid;
                            dist += folsta.distance;
                            ts += folsta.driveTime;
                                flagSecond = true;
                        }
                    }
                    if(flagFirst&&flagSecond)
                    dl.updateFollowStation(new DO.followStations() { id = idfol, firstStationid = idsta1, secondStationid = idsta2, lineId = lin.id ,distance=dist,driveTime=ts,enabled=true});
                }
            }
            dl.removebusLineStation(id);

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
                return (from folSta in folllowStation where folSta != null && folSta.enabled == true
                        from sta in stations
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
            dl.addUser(Utility.BOtoDOConvertor<DO.User,BO.User>(user));
        }
        public void removeUser(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        
    }
}
