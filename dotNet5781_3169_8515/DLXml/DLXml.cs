﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
namespace DL
{
        sealed class DLXml : IDL
         {
        #region singelton
        static readonly DLXml instance = new DLXml();
        static DLXml() { }
        DLXml() { } 
        public static DLXml Instance { get => instance; }



        #region implemention

        #region Bus

        public void updateBus(Bus bus)
        {
            var result = from b1 in Utility.load(typeof(Bus)).Elements()
                         select b1;
            foreach (var element in result)
                if (element.Attribute("id").Value == bus.id.ToString())
                    element.ReplaceWith(bus.ToXml());
            XElement root = new XElement("Buses");
            foreach (var element in result)
                root.Add(element);
            Utility.save(root, typeof(Bus));
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            return   from element in Utility.load(typeof(Bus)).Elements()
                         let obj = element.ToObject<Bus>()
                         where element != null && obj.enabled == true // change elemnt to obj
                         select obj;
        }

        public void addBus(Bus bus)
        {
            var result = GetBus(bus.id);
            if(result!=null)
                throw new itemAlreadyExistsException($"ID number {bus.id} is already taken");
            var root = Utility.load(typeof(Bus));
                root.Add(bus.ToXml());
            Utility.save(root,typeof(Bus));
        }

        public Bus GetBus(int id)
        {
            return (from element in Utility.load(typeof(Bus)).Elements()
                   let obj = element.ToObject<Bus>()
                   where element != null && obj.enabled == true && obj.id==id // change elemnt to obj
                   select obj).FirstOrDefault();
        }
     

        public void removeBus(int id)
        {
            var bus = GetBus(id);
            if (bus == null)
                throw new NoSuchEntryException($"No Bus Matches ID number {id}");
            bus.enabled = false;
            updateBus(bus);
        }

        public void maintain(int id)
        {
            var bus = GetBus(id);
            bus.lastMaintenance = DateTime.Now;
            bus.status = "ready";
            bus.dangerous = false;
            bus.fuel = Bus.FULL_TANK;
            bus.distance = 0;
            updateBus(bus);
        }

        public void refuel(int id)
        {
            var bus = GetBus(id);
            bus.fuel = Bus.FULL_TANK;
            updateBus(bus);
        }

        #endregion

        #region User
        public void addUser(User user)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllbusUsers()
        {
            throw new NotImplementedException();
        }
        public User GetUser(int id)
        {
            return (from element in Utility.load(typeof(User)).Elements()
                    let obj = element.ToObject<User>()
                    where element != null && obj.enabled == true && obj.id == id // change elemnt to obj
                    select obj).FirstOrDefault();
        }

        #endregion

        #region Line
        public void addLine(BusLine line)
        {
            throw new NotImplementedException();
        }
        public int countLines(string number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusLine> GetAllbusLines()
        {
            throw new NotImplementedException();
        }

        public BusLine GetBusLine(int id)
        {
            throw new NotImplementedException();
        }

        public int GetBusLineID(string number)
        {
            throw new NotImplementedException();
        }

        public void removeLine(int id)
        {
            throw new NotImplementedException();
        }

        public void updateLine(BusLine line)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LineInStation
        public void addLineInStation(LineInStation lis)
        {
            var result = GetLineInStation(lis.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {lis.id} is already taken");
            var root = Utility.load(typeof(LineInStation));
            root.Add(lis.ToXml());
            Utility.save(root, typeof(LineInStation));
        }
        public DO.LineInStation GetLineInStation(int lisId)
        {
            return (from element in Utility.load(typeof(LineInStation)).Elements()
                    let obj = element.ToObject<LineInStation>()
                    where element != null  && obj.id == lisId
                    select obj).FirstOrDefault();
        }
        public IEnumerable<LineInStation> GetAllLineInStation()
        {
            return from element in Utility.load(typeof(LineInStation)).Elements()
                   let obj = element.ToObject<LineInStation>()
                   where element != null // change elemnt to obj
                   select obj;
        }

        public void removeLineInStation(int lineId)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region station   
        public BusLineStation GetbusLineStation(int id)
        {
            return (from element in Utility.load(typeof(BusLineStation)).Elements()
                    let obj = element.ToObject<BusLineStation>()
                    where element != null && obj.enabled == true && obj.id == id
                    select obj).FirstOrDefault();
        }
        public void addStation(BusLineStation station)
        {
            var result = GetbusLineStation(station.id);
            if(result!=null)
                throw new itemAlreadyExistsException($"ID number {station.code} is already taken");
            var root = Utility.load(typeof(BusLineStation));
            root.Add(station.ToXml());
            Utility.save(root, typeof(BusLineStation));
        }




        public IEnumerable<BusLineStation> GetAllbusLineStation()
        {
            return from element in Utility.load(typeof(BusLineStation)).Elements()
                   let obj = element.ToObject<BusLineStation>()
                   where element != null && obj.enabled == true // change elemnt to obj
                   select obj;
        }


        public void updatebusLineStation(BusLineStation station)
        {
            var res = from element in Utility.load(typeof(BusLineStation)).Elements()
                      select element;
            foreach(var sta in res)
                if (sta.Attribute("id").Value == station.id.ToString())
                    sta.ReplaceWith(station.ToXml());
            
        }

        public void removebusLineStation(int id)
        {
            var station = GetbusLineStation(id);
            if (station == null)
                throw new NoSuchEntryException($"No Station Mathces ID number {id}");
            station.enabled = false;
            updatebusLineStation(station);
        }


        #endregion

        #region FollowStation
        public void addFollowStation(FollowStations folStation)
        {
            var result = GetFollowStation(folStation.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {folStation.id} is already taken");
            var root = Utility.load(typeof(FollowStations));
            root.Add(folStation.ToXml());
            Utility.save(root, typeof(FollowStations));
        }

        public FollowStations GetFollowStation(int id)
        {
            return (from element in Utility.load(typeof(FollowStations)).Elements()
                    let obj = element.ToObject<FollowStations>()
                    where element != null && obj.enabled == true && obj.id == id
                    select obj).FirstOrDefault();
        }

        public IEnumerable<FollowStations> GetAllFollowStation()
        {
            return from element in Utility.load(typeof(FollowStations)).Elements()
                   let obj = element.ToObject<FollowStations>()
                   where element != null && obj.enabled == true // change elemnt to obj
                   select obj;
        }



        public void removeFollowStation(int LineId)
        {
            throw new NotImplementedException();
        }

        public void removeFollowStationByIdOfFol(int Id)
        {
            throw new NotImplementedException();
        }



        public void updateFollowStation(FollowStations folStation)
        {
            throw new NotImplementedException();
        }



        #endregion
        #endregion

        #endregion
       
    }
}
