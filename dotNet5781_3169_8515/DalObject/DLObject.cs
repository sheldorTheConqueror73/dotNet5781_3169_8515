using DalApi;
using System;
using System.Collections.Generic;
using DS;
using DO;
using System.Linq;

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

        #region implementation
        #region bus
        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.buses
                   select bus.Clone();
        }
        public Bus GetBus(string id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No Bus Matches ID number {id}");
            return result;
            
        }
        public void addBus(Bus b1)
        {

            var result = DataSource.buses.Find(b => b.id == b1.id);
            if ((result != null) || (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {b1.id} is already taken");
            DataSource.buses.Add(b1.Clone());
        }
        void removeBus(string id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if ((result == null) || (result.enabled == false))
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        void updateBus(Bus bus)
        {
            var result = DataSource.buses.Find(b => b.id == bus.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {bus.id}");
            result = bus.Clone();
        }
        #endregion




        #region busLine
        public IEnumerable<busLine> GetAllbusLines()
        {
            return from bus in DataSource.Lines
                   select bus.Clone();
        }
        public busLine GetBusLine(string id)
        {

            var result = DataSource.Lines.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        public void addLine(busLine line)
        {
            var result = DataSource.Lines.Find(b => b.id == line.id);
            if ((result != null) || (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {line.id} is already taken");
            DataSource.Lines.Add(line.Clone());
        }
        public void removeLine(string id)
        {
            var result = DataSource.Lines.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        public void updateLine(busLine line)
        {
            var result = DataSource.Lines.Find(b => b.id == line.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {line.id}");
            result = line.Clone();
        }
        #endregion

        #region Station
        public IEnumerable<busStation> GetAllbusStations();
        public IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate);
        public busStation GetbusStation(string id);
        public void addLine(busStation station);
        public void removeStation(string id);
        public void updateStation(busStation station);
        #endregion

        #region LineStation
        public IEnumerable<busLineStation> GetAllbusLineStation();
        public IEnumerable<busLineStation> GetAllbusLineStationBy(Predicate<busLineStation> predicate);
        public busLineStation GetbusLineStation(string id);
        public  void addLine(busLineStation line);
        public void removebusLineStation(string id);
        public void updatebusLineStation(busLineStation line);
        #endregion

        #region User
        IEnumerable<User> GetAllbusUsers();
        IEnumerable<User> GetAllbusUsersBY(Predicate<User> predicate);
        User GetbusLineUser(string id);
        void addLine(User line);
        void removebusUser(string id);
        void updatebusUser(User line);
        #endregion

        #endregion








    }
}
