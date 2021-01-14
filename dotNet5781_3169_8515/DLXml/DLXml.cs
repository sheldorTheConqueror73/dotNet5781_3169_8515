using System;
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
        /// <summary>
        /// update bus
        /// </summary>
        /// <param name="bus">the updated bus</param>
        public void updateBus(Bus bus)
        {
            var result = from b1 in Utility.load(typeof(Bus)).Elements()
                         select b1;
            foreach (var element in result)
                if (element.Element("id").Value == bus.id.ToString())
                    element.ReplaceWith(bus.ToXml());
            XElement root = new XElement("Buses");
            foreach (var element in result)
                root.Add(element);
            Utility.save(root, typeof(Bus));
        }
        /// <summary>
        /// retrun all buses
        /// </summary>
        public IEnumerable<Bus> GetAllBuses()
        {
            return   from element in Utility.load(typeof(Bus)).Elements()
                     where element != null
                     let obj = element.ToObject<Bus>()
                     where obj.enabled == true // change elemnt to obj
                     select obj;
        }
        /// <summary>
        /// add bus
        /// </summary>
        /// <param name="b1">the new bus</param>
        public void addBus(Bus bus)
        {
            var result = GetBus(bus.id);
            if(result!=null)
                throw new itemAlreadyExistsException($"ID number {bus.id} is already taken");
            var root = Utility.load(typeof(Bus));
                root.Add(bus.ToXml());
            Utility.save(root,typeof(Bus));
        }
        /// <summary>
        /// retrun bus by id
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
        public Bus GetBus(int id)
        {
            return (from element in Utility.load(typeof(Bus)).Elements()
                    where element != null
                    let obj = element.ToObject<Bus>()
                    where obj.enabled == true // change elemnt to obj
                    select obj).FirstOrDefault();
        }

        /// <summary>
        /// remove bus
        /// </summary>
        /// <param name="id">the id of the requested bus to remove</param>
        public void removeBus(int id)
        {
            var bus = GetBus(id);
            if (bus == null)
                throw new NoSuchEntryException($"No Bus Matches ID number {id}");
            bus.enabled = false;
            updateBus(bus);
        }
        /// <summary>
        /// maintenance
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
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
        /// <summary>
        /// refuel bus
        /// </summary>
        /// <param name="id">the id of the requested bus</param>
        public void refuel(int id)
        {
            var bus = GetBus(id);
            bus.fuel = Bus.FULL_TANK;
            updateBus(bus);
        }

        #endregion

        #region User
        /// <summary>
        /// add new user
        /// </summary>
        /// <param name="user">hte new user</param>
        public void addUser(User user)
        {
            var result = GetBus(user.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {user.id} is already taken");
            var root = Utility.load(typeof(User));
            root.Add(user.ToXml());
            Utility.save(root, typeof(User));
        }
        public IEnumerable<User> GetAllbusUsers()
        {
            return from element in Utility.load(typeof(User)).Elements()
                   where element != null
                   let obj = element.ToObject<User>()
                   where obj.enabled == true // change elemnt to obj
                   select obj;
        }
        /// <summary>
        /// return user by id
        /// </summary>
        /// <param name="id">id of the requested user</param>
        public User GetUser(int id)
        {
            return (from element in Utility.load(typeof(User)).Elements()
                    let obj = element.ToObject<User>()
                    where element != null && obj.enabled == true && obj.id == id // change elemnt to obj
                    select obj).FirstOrDefault();
        }

        #endregion

        #region Line
        /// <summary>
        /// add line
        /// </summary>
        /// <param name="line">the new line</param>
        public void addLine(BusLine line)
        {
            var result = GetBus(line.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {line.id} is already taken");
            var root = Utility.load(typeof(BusLine));
            root.Add(line.ToXml());
            Utility.save(root, typeof(BusLine));
        }
        /// <summary>
        /// return how many lines exists with this number
        /// </summary>
        /// <param name="number">the number of the line</param>
        public int countLines(string number)
        {
            return GetAllbusLines().ToList().Count;
        }
        /// <summary>
        /// retrun all lines
        /// </summary>
        public IEnumerable<BusLine> GetAllbusLines()
        {
            return from element in Utility.load(typeof(BusLine)).Elements()
                   where element != null
                   let obj = element.ToObject<BusLine>()
                   where obj.enabled == true // change elemnt to obj
                   select obj;
        }
        /// <summary>
        /// return lines
        /// </summary>
        /// <param name="id">the id of the requested line</param>
        public BusLine GetBusLine(int id)
        {
            return (from element in Utility.load(typeof(BusLine)).Elements()
                    where element != null
                    let obj = element.ToObject<BusLine>()
                    where obj.enabled == true // change elemnt to obj
                    select obj).FirstOrDefault();
        }
        /// <summary>
        /// return line id by number
        /// </summary>
        /// <param name="number">number of the requested line</param>
        public int GetBusLineID(string number)
        {
            var result = (from line in GetAllbusLines()
                          where line != null && line.number == number
                          select line.id).ToList();
            if (result.Count == 0)
                throw new noMatchExeption($"No line matches number {number}");
            return result[0];

        }

        public void removeLine(int id)
        {
            var line = GetBusLine(id);
            if (line == null)
                throw new NoSuchEntryException($"No Line Matches ID number {id}");
            line.enabled = false;
            updateLine(line);
        }

        public void updateLine(BusLine line)
        {
            var result = from b1 in Utility.load(typeof(BusLine)).Elements()
                         select b1;
            foreach (var element in result)
                if (element.Element("id").Value == line.id.ToString())
                    element.ReplaceWith(line.ToXml());
            XElement root = new XElement("Lines");
            foreach (var element in result)
                root.Add(element);
            Utility.save(root, typeof(BusLine));
        }
        #endregion

        #region LineInStation
        /// <summary>
        /// add line in station object
        /// </summary>
        /// <param name="lis">the new object</param>
        public void addLineInStation(LineInStation lis)
        {
            var result = GetLineInStation(lis.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {lis.id} is already taken");
            var root = Utility.load(typeof(LineInStation));
            root.Add(lis.ToXml());
            Utility.save(root, typeof(LineInStation));
        }
        /// <summary>
        /// return specific line in station object by id
        /// </summary>
        /// <param name="lisId">id of the requested line in station object</param>
        public DO.LineInStation GetLineInStation(int lisId)
        {
            return (from element in Utility.load(typeof(LineInStation)).Elements()
                    where element != null
                    let obj = element.ToObject<LineInStation>()
                    where obj.id == lisId
                    select obj).FirstOrDefault();
        }
        /// <summary>
        /// return all line in station
        /// </summary>
        public IEnumerable<LineInStation> GetAllLineInStation()
        {
            return from element in Utility.load(typeof(LineInStation)).Elements()
                   where element != null 
                   let obj = element.ToObject<LineInStation>()
                   select obj;
        }
        /// <summary>
        /// remove line in station object
        /// </summary>
        /// <param name="lineId">the removing object</param>
        public void removeLineInStation(int lineId)
        {
            var res = from element in Utility.load(typeof(LineInStation)).Elements()
                      select element;
            foreach (var linInSta in res)
                if (linInSta.Element("id").Value == lineId.ToString())
                    linInSta.Remove();
            XElement root = new XElement("LineInStations");
            foreach (var element in res)
                root.Add(element);
            Utility.save(root, typeof(LineInStation));

        }


        #endregion

        #region station 
        /// <summary>
        /// retrun specific station
        /// </summary>
        /// <param name="id">the id of the requested station</param>
        public BusLineStation GetbusLineStation(int id)
        {
            return (from element in Utility.load(typeof(BusLineStation)).Elements()
                    where element != null
                    let obj = element.ToObject<BusLineStation>()
                    where  obj.enabled == true && obj.id == id
                    select obj).FirstOrDefault();
        }
        /// <summary>
        /// add station
        /// </summary>
        /// <param name="station">the new station</param>
        public void addStation(BusLineStation station)
        {
            var result = GetbusLineStation(station.id);
            if(result!=null)
                throw new itemAlreadyExistsException($"ID number {station.code} is already taken");
            var root = Utility.load(typeof(BusLineStation));
            root.Add(station.ToXml());
            Utility.save(root, typeof(BusLineStation));
        }


        /// <summary>
        /// return all stations
        /// </summary>
        public IEnumerable<BusLineStation> GetAllbusLineStation()
        {
            return from element in Utility.load(typeof(BusLineStation)).Elements()
                   where element != null 
                   let obj = element.ToObject<BusLineStation>()
                   where obj.enabled == true // change elemnt to obj
                   select obj;
        }

        /// <summary>
        /// update station
        /// </summary>
        /// <param name="station">the updated station</param>
        public void updatebusLineStation(BusLineStation station)
        {
            var res = from element in Utility.load(typeof(BusLineStation)).Elements()
                      select element;
            foreach(var sta in res)
                if (sta.Element("id").Value == station.id.ToString())
                    sta.ReplaceWith(station.ToXml());
            XElement root = new XElement("Station");
            foreach (var element in res)
                root.Add(element);
            Utility.save(root, typeof(BusLineStation));
        }
        /// <summary>
        /// remove station
        /// </summary>
        /// <param name="id">the id of the requested station</param>
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
        /// <summary>
        /// add follow station
        /// </summary>
        /// <param name="folStation">the new follow statio object</param>
        public void addFollowStation(FollowStations folStation)
        {
            var result = GetFollowStation(folStation.id);
            if (result != null)
                throw new itemAlreadyExistsException($"ID number {folStation.id} is already taken");
            var root = Utility.load(typeof(FollowStations));
            root.Add(folStation.ToXml());
            Utility.save(root, typeof(FollowStations));
        }
        /// <summary>
        /// return specific follow station object by id
        /// </summary>
        /// <param name="id">id of the requested followstation object</param>
        public FollowStations GetFollowStation(int id)
        {
            return (from element in Utility.load(typeof(FollowStations)).Elements()
                    where element != null
                    let obj = element.ToObject<FollowStations>()
                    where obj.enabled == true && obj.id == id
                    select obj).FirstOrDefault();
        }
        /// <summary>
        /// return all follow stations lists
        /// </summary>
        public IEnumerable<FollowStations> GetAllFollowStation()
        {
            return from element in Utility.load(typeof(FollowStations)).Elements()
                   where element != null
                   let obj = element.ToObject<FollowStations>()
                   where obj.enabled == true // change elemnt to obj
                   select obj;
        }


        /// <summary>
        /// remove all follow station objects by line id
        /// </summary>
        /// <param name="LineId">id of line</param>
        public void removeFollowStation(int LineId)
        {    
            var res = from element in Utility.load(typeof(FollowStations)).Elements()
                      select element;
            foreach (var folSta in res)
                if (folSta.Element("lineId").Value == LineId.ToString())
                {
                    folSta.ToObject<FollowStations>().enabled = false;
                    folSta.ReplaceWith(folSta);
                }
            XElement root = new XElement("FollowStations");
            foreach (var element in res)
                root.Add(element);
            Utility.save(root, typeof(FollowStations));
        }
        /// <summary>
        /// remove follow station object
        /// </summary>
        /// <param name="Id">id of the requested object</param>
        public void removeFollowStationByIdOfFol(int Id)
        {
            var folSta = GetFollowStation(Id);
            if (folSta == null)
                throw new NoSuchEntryException($"No Station Mathces ID number {Id}");
            folSta.enabled = false;
            updateFollowStation(folSta);
        }


        /// <summary>
        /// update follow station object
        /// </summary>
        /// <param name="folStation">the updated object</param>
        public void updateFollowStation(FollowStations folStation)
        {
            var res = from element in Utility.load(typeof(FollowStations)).Elements()
                      select element;
            foreach (var sta in res)
                if (sta.Element("id").Value == folStation.id.ToString())
                    sta.ReplaceWith(folStation.ToXml());
            XElement root = new XElement("FollowStations");
            foreach (var element in res)
                root.Add(element);
            Utility.save(root, typeof(FollowStations));
        }



        #endregion
        #endregion

        #endregion
       
    }
}
