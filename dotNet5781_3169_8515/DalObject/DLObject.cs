using DalApi;
using System;
using System.Collections.Generic;
using DS;
using DO;

namespace DL
{
    sealed class DLObject : IDal
    {
      #region singelton
      static readonly DLObject instance = new DLObject();
      static DLObject() { }
      DLObject() { } 
      public static DLObject Instance { get => instance; }
      #endregion

        public void addBus(Bus b1)
        {

            var result = DataSource.buses.Find(b => b.id == b1.id);
            if ((result != null) || (result.enabled == true))
                throw new NoSuchEntryException($"ID number {b1.id} is already taken");
            DataSource.buses.Add(b1.Clone());
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

        public void addLine(User line)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLine> GetAllbusLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLineStation> GetAllbusLineStation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLineStation> GetAllbusLineStationBy(Predicate<busLineStation> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busStation> GetAllbusStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllbusUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllbusUsersBY(Predicate<User> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string id)
        {
            throw new NotImplementedException();
        }

        public busLine GetBusLine(string id)
        {
            throw new NotImplementedException();
        }

        public busLineStation GetbusLineStation(string id)
        {
            throw new NotImplementedException();
        }

        public User GetbusLineUser(string id)
        {
            throw new NotImplementedException();
        }

        public busStation GetbusStation(string id)
        {
            throw new NotImplementedException();
        }

        public void removeBus(string id)
        {
            throw new NotImplementedException();
        }

        public void removebusLineStation(string id)
        {
            throw new NotImplementedException();
        }

        public void removebusUser(string id)
        {
            throw new NotImplementedException();
        }

        public void removeLine(string id)
        {
            throw new NotImplementedException();
        }

        public void removeStation(string id)
        {
            throw new NotImplementedException();
        }

        public void updateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void updatebusLineStation(busLineStation line)
        {
            throw new NotImplementedException();
        }

        public void updatebusUser(User line)
        {
            throw new NotImplementedException();
        }

        public void updateLine(busLine line)
        {
            throw new NotImplementedException();
        }

        public void updateStation(busStation station)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region implementation

        #endregion
    }
}
