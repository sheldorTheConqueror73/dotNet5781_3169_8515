using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        /// <summary>
        /// retrun all buses
        /// </summary>
        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.buses
                   where bus!=null 
                   select bus.Clone();
        }
        /// <summary>
        /// retrun bus by id
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
        public Bus GetBus(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if ((result == null)||(result.enabled==false))
                throw new NoSuchEntryException($"No Bus Matches ID number {id}");
            return result;

        }
        /// <summary>
        /// add bus
        /// </summary>
        /// <param name="b1">the new bus</param>
        public void addBus(Bus b1)
        {
            var result = DataSource.buses.Find(b => b.id == b1.id);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {b1.id} is already taken");
            DataSource.buses.Add(b1.Clone());
        }
        /// <summary>
        /// remove bus
        /// </summary>
        /// <param name="id">the id of the requested bus to remove</param>
        public void removeBus(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if ((result == null) || (result.enabled == false))
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        /// <summary>
        /// update bus
        /// </summary>
        /// <param name="bus">the updated bus</param>
        public void updateBus(Bus bus)
        {
            var result = DataSource.buses.Find(b => b.id == bus.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {bus.id}");
            DataSource.buses.Remove(result);
            DataSource.buses.Add(bus.Clone());

        }
        /// <summary>
        /// refuel bus
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
        public void refuel(int id)
        {
            var result = DataSource.buses.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.fuel = Bus.FULL_TANK;
        }
        /// <summary>
        /// maintenance
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
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
        /// <summary>
        /// retrun all lines
        /// </summary>
        public IEnumerable<BusLine> GetAllbusLines()
        {
            return from bus in DataSource.Lines
                   select bus.Clone();
        }
        /// <summary>
        /// return lines
        /// </summary>
        /// <param name="id">the id of the requested line</param>
        public BusLine GetBusLine(int id)
        {

            var result = DataSource.Lines.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        /// <summary>
        /// add line
        /// </summary>
        /// <param name="line">the new line</param>
        public void addLine(BusLine line)
        {
            var result = DataSource.Lines.Find(b => b.id == line.id);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {line.id} is already taken");
            DataSource.Lines.Add(line.Clone());
        }
        /// <summary>
        /// remobe line
        /// </summary>
        /// <param name="id">the id of the requested line</param>
        public void removeLine(int id)
        {
            var result = DataSource.Lines.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        /// <summary>
        /// update line
        /// </summary>
        /// <param name="line">the updated line</param>
        public void updateLine(BusLine line)
        {
            var result = DataSource.Lines.Find(b => b.id == line.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {line.id}");
            DataSource.Lines.Remove(result);
            DataSource.Lines.Add(line.Clone());
        }
        /// <summary>
        /// return how many lines exists with this number
        /// </summary>
        /// <param name="number">the number of the line</param>
        public int countLines(string number)
        {
            var result = from line in DataSource.Lines
                         where line != null && line.enabled == true && line.number == number
                         select line;
            return result.Count();
        }
        /// <summary>
        /// return line id by number
        /// </summary>
        /// <param name="number">number of the requested line</param>
        public int GetBusLineID(string number)
        {
            return (from line in DataSource.Lines
                    where line != null && line.enabled == true && line.number == number
                    select line.id).First();
        }
        #endregion


        #region LineStation
        /// <summary>
        /// add station
        /// </summary>
        /// <param name="station">the new station</param>
        public void addStation(BusLineStation station)
        {
            var result = DataSource.LineStations.Find(b => b.code == station.code);
            if ((result != null) && (result.enabled == true))
                throw new itemAlreadyExistsException($"ID number {station.code} is already taken");
            DataSource.LineStations.Add(station.Clone());
        }
        /// <summary>
        /// return all stations
        /// </summary>
        public IEnumerable<BusLineStation> GetAllbusLineStation()
        {
            return from station in DataSource.LineStations
                   select station.Clone();
        }
        /// <summary>
        /// retrun specific station
        /// </summary>
        /// <param name="id">the id of the requested station</param>
        public BusLineStation GetbusLineStation(int id)
        {
            var result = DataSource.LineStations.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        /// <summary>
        /// remove station
        /// </summary>
        /// <param name="id">the id of the requested station</param>
        public void removebusLineStation(int id)
        {
            var result = DataSource.LineStations.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
            DataSource.lineInStations.RemoveAll(b => b.stationid == id);
        }
        /// <summary>
        /// update station
        /// </summary>
        /// <param name="station">the updated station</param>
        public void updatebusLineStation(BusLineStation station)
        {
            var result = DataSource.LineStations.Find(b => b.id == station.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {station.id}");
            DataSource.LineStations.Remove(result);
            DataSource.LineStations.Add(station.Clone());
        }
       
        #endregion

        #region User
        /// <summary>
        /// retrun all users
        /// </summary>
        public IEnumerable<User> GetAllbusUsers()
        {
            return from user in DataSource.users
                   select user.Clone();
        }
        /// <summary>
        /// return user by id
        /// </summary>
        /// <param name="id">id of the requested user</param>
        public User GetUser(int id)
        {
            var result = DataSource.users.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            return result;
        }
        /// <summary>
        /// add new user
        /// </summary>
        /// <param name="user">hte new user</param>
        public void addUser(User user)
        {
            var result = DataSource.users.Find(b => (b.id == user.id||b.name==user.name));
            if ((result != null) && (result.enabled == true))
            {
                if(result.id==user.id)
                    throw new itemAlreadyExistsException($"ID number {user.id} is already taken");
                throw new itemAlreadyExistsException($"User name {user.name} is already taken");
            }
            DataSource.users.Add(user.Clone());
        }
        /// <summary>
        /// remove user
        /// </summary>
        /// <param name="id">id of the requested user</param>
        public void removebusUser(int id)
        {
            var result = DataSource.users.Find(b => b.id == id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {id}");
            result.enabled = false;
        }
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="user">the updated user</param>
        public void updatebusUser(User user)
        {
            var result = DataSource.users.Find(b => b.id == user.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {user.id}");
            result = user.Clone();
        }



        #endregion

        #region lineInStation
        /// <summary>
        /// return all line in station
        /// </summary>
        public IEnumerable<LineInStation> GetAllLineInStation()
        {
            return from bus in DataSource.lineInStations
                   select bus.Clone();
        }
        /// <summary>
        /// add line in station object
        /// </summary>
        /// <param name="lis">the new object</param>
        public void addLineInStation(LineInStation lis)
        {
            var result = DataSource.lineInStations.Find(b => b.id == lis.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {lis.id} is already taken");
            DataSource.lineInStations.Add(lis.Clone());
        }
        /// <summary>
        /// remove line in station object
        /// </summary>
        /// <param name="lineId">the removing object</param>
        public void removeLineInStation(int lineId)
        {    
            DataSource.lineInStations.RemoveAll(b => b.Lineid == lineId);
        }
        /// <summary>
        /// return specific line in station object by id
        /// </summary>
        /// <param name="lisId">id of the requested line in station object</param>
        public LineInStation GetLineInStation(int lisId)
        {
            var result = DataSource.lineInStations.Find(b => b.id == lisId);
            if (result == null)
                throw new NoSuchEntryException($"No FollowStation Matches ID number {lisId}");
            return result;
        }
        #endregion

        #region followStations
        /// <summary>
        /// return all follow stations lists
        /// </summary>
        public IEnumerable<FollowStations> GetAllFollowStation()
        {
            return from bus in DataSource.followStation
                   select bus.Clone();
        }
        /// <summary>
        /// update follow station object
        /// </summary>
        /// <param name="folStation">the updated object</param>
        public void updateFollowStation(FollowStations folStation)
        {
            var result = DataSource.followStation.Find(b => b.id == folStation.id);
            if (result == null)
                throw new NoSuchEntryException($"No entry Matches ID number {folStation.id}");        
            int index= DataSource.followStation.IndexOf(result);
            DataSource.followStation[index] = folStation.Clone();
        }

        

       /// <summary>
       /// add follow station
       /// </summary>
       /// <param name="folStation">the new follow statio object</param>
        public void addFollowStation(FollowStations folStation)
        {
            var result = DataSource.followStation.Find(b => b.id == folStation.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {folStation.id} is already taken");
            DataSource.followStation.Add(folStation.Clone());
        }
        /// <summary>
        /// remove follow station object
        /// </summary>
        /// <param name="Id">id of the requested object</param>
        public void removeFollowStationByIdOfFol(int Id)
        {
            var folSta = DataSource.followStation.Find(x => x.id == Id);
            DataSource.followStation.Remove(folSta);
        }
        /// <summary>
        /// remove all follow station objects by line id
        /// </summary>
        /// <param name="LineId">id of line</param>
        public void removeFollowStation(int LineId)
        {
            var reslut = (from item in DataSource.followStation
                         where item != null && item.enabled == true && item.lineId == LineId
                         let x = item.enabled = false
                         select item).ToList();
        }
        /// <summary>
        /// return specific follow station object by id
        /// </summary>
        /// <param name="id">id of the requested followstation object</param>
        public FollowStations GetFollowStation(int id)
        {
            var result = DataSource.followStation.Find(b => b.id == id);
            if ((result == null) || (result.enabled == false))
                throw new NoSuchEntryException($"No FollowStation Matches ID number {id}");
            return result;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void updateTime(int id, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public void updateStatus(int id, string status)
        {
            throw new NotImplementedException();
        }



        #endregion
        #endregion



    }
}