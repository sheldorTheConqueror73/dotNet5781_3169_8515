using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DLAPI;
using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal

    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion


        #region implementation
        #region bus
        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.buses
                   where bus!=null 
                   select bus.Clone();
        }
        public Bus GetBus(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if ((result == null)||(result.enabled==true))
                throw new NoSuchEntryException($"No Bus Matches ID number {id}");
            return result;

        }
        public void addBus(Bus b1)
        {
            var result = DataSource.buses.Find(b => b.id == b1.id);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {b1.id} is already taken");
            DataSource.buses.Add(b1.Clone());
        }
        public void removeBus(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if ((result == null) || (result.enabled == false))
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        public void updateBus(Bus bus)
        {
            var result = DataSource.buses.Find(b => b.id == bus.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {bus.id}");
            DataSource.buses.Remove(result);
            DataSource.buses.Add(bus.Clone());

        }
        public void refuel(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.fuel = Bus.FULL_TANK;
        }
        public void maintain(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.lastMaintenance = DateTime.Now;
            result.status = "ready";
            result.dangerous = false;
            result.distance = 0;
        }
        #endregion




        #region busLine
        public IEnumerable<busLine> GetAllbusLines()
        {
            return from bus in DataSource.Lines
                   select bus.Clone();
        }
        public busLine GetBusLine(int id)
        {

            var result = DataSource.Lines.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        public void addLine(busLine line)
        {
            var result = DataSource.Lines.Find(b => b.id == line.id);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {line.id} is already taken");
            DataSource.Lines.Add(line.Clone());
        }
        public void removeLine(int id)
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
        public int countLines(string number)
        {
            var result = from line in DataSource.Lines
                         where line != null && line.enabled == true && line.number == number
                         select line;
            return result.Count();
        }

        #endregion


        #region LineStation
        public void addStation(DO.busLineStation station)
        {
            var result = DataSource.LineStations.Find(b => b.code == station.code);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {station.code} is already taken");
            DataSource.LineStations.Add(station.Clone());
        }
        public IEnumerable<busLineStation> GetAllbusLineStation()
        {
            return from bus in DataSource.LineStations
                   select bus.Clone();
        }
        public busLineStation GetbusLineStation(int id)
        {
            var result = DataSource.LineStations.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
      
        public void removebusLineStation(int id)
        {
            var result = DataSource.LineStations.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
            DataSource.lineInStations.RemoveAll(b => b.stationid == id);
        }
        public void updatebusLineStation(busLineStation station)
        {
            var result = DataSource.LineStations.Find(b => b.id == station.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {station.id}");
            DataSource.LineStations.Remove(result);
            DataSource.LineStations.Add(station.Clone());
        }
        public int GetBusLineID(string number)
        {
            return (from line in DataSource.Lines
                    where line != null && line.enabled == true && line.number == number
                    select line.id).First();
        }
        #endregion

        #region User
        public IEnumerable<User> GetAllbusUsers()
        {
            return from user in DataSource.users
                   select user.Clone();
        }
        public User GetUser(int id)
        {
            var result = DataSource.users.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        public void addUser(User user)
        {
            var result = DataSource.users.Find(b => b.id == user.id);
            if ((result != null) || (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {user.id} is already taken");
            DataSource.users.Add(user.Clone());
        }
        public void removebusUser(int id)
        {
            var result = DataSource.users.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        public void updatebusUser(User user)
        {
            var result = DataSource.users.Find(b => b.id == user.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {user.id}");
            result = user.Clone();
        }



        #endregion

        #region lineInStation
        public IEnumerable<lineInStation> GetAllLineInStation()
        {
            return from bus in DataSource.lineInStations
                   select bus.Clone();
        }
        public void addLineInStation(lineInStation lis)
        {
            var result = DataSource.lineInStations.Find(b => b.id == lis.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {lis.id} is already taken");
            DataSource.lineInStations.Add(lis.Clone());
        }
        public void removeLineInStation(int lineId)
        {    
            DataSource.lineInStations.RemoveAll(b => b.Lineid == lineId);
        }
       
        #endregion

        #region followStations
        public IEnumerable<followStations> GetAllFollowStation()
        {
            return from bus in DataSource.followStation
                   select bus.Clone();
        }
        public void updateFollowStation(DO.followStations folStation)
        {
            var result = DataSource.followStation.Find(b => b.id == folStation.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {folStation.id}");        
            int index= DataSource.followStation.IndexOf(result);
            DataSource.followStation[index] = folStation.Clone();
        }

        

       
        public void addFollowStation(followStations folStation)
        {
            var result = DataSource.followStation.Find(b => b.id == folStation.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {folStation.id} is already taken");
            DataSource.followStation.Add(folStation.Clone());
        }

        public void removeFollowStationByIdOfFol(int Id)
        {
            var folSta = DataSource.followStation.Find(x => x.id == Id);
            DataSource.followStation.Remove(folSta);
        }

        public void removeFollowStation(int LineId)
        {
            var reslut = (from item in DataSource.followStation
                         where item != null && item.enabled == true && item.lineId == LineId
                         let x = item.enabled = false
                         select item).ToList();
        }

        #endregion
        #endregion




    }
}