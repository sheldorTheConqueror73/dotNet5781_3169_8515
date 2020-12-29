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
            throw new NotImplementedException();
        }

        public List<Bus> GetAllBuses()
        {
            var result = dl.GetAllBuses();
            if (result != null)
                return (from item in result
                        where (item != null && item.enabled == true )
                        orderby item.plateNumber ascending
                        select Utility.DOtoBOConvertor<BO.Bus,DO.Bus>(item)).ToList();
            return default;

            //DOBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
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
                    select Utility.DOtoBOConvertor<BO.busLine,DO.busLine>(item);
            return default;

        }

        public IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate)
        {
            throw new NotImplementedException();
        }

        public void removeLine(int id)
        {
            throw new NotImplementedException();
        }


        public void updateLine(busLine line)
        {
            throw new NotImplementedException();
        }
        public void addLine(busLine line)
        {
            throw new NotImplementedException();
        }

        public void addLine(busStation station)
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



        public IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate)
        {
            throw new NotImplementedException();
        }

        #region station
        public IEnumerable<busLineStation> GetAllbusLineStation()
        {
            var result = dl.GetAllbusLineStation();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
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
        public IEnumerable<busStation> GetAllbusStations()
        {
            throw new NotImplementedException();

        }
        public busLineStation GetbusLineStation(int id)
        {
            throw new NotImplementedException();
        }

        public busStation GetbusStation(int id)
        {
            throw new NotImplementedException();
        }

        public void removebusLineStation(int id)
        {
            throw new NotImplementedException();
        }
        public void removeStation(int id)
        {
            dl.removebusLineStation(id);
        }



        public void updatebusLineStation(busLineStation line)
        {
            throw new NotImplementedException();
        }

        public void updateStation(busStation station)
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
                        select (DOtoBOConvertor<BO.busLineStation, DO.busLineStation>(sta))).ToList();
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
            throw new NotImplementedException();
        }
        public void removeUser(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        
    }
}
