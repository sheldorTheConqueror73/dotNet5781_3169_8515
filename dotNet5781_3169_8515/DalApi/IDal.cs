using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDal
    {
        #region bus
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(string id);
        void addBus(Bus bus);
        void removeBus(string id);
        void updateBus(Bus bus);
        #endregion

        #region busLine
        IEnumerable<busLine> GetAllbusLines();
        IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate);
        busLine GetBusLine(string id);
        void addLine(busLine line);
        void removeLine(string id);
        void updateLine(busLine line);
        #endregion

        #region Station
        IEnumerable<busStation> GetAllbusStations();
        IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate);
        busStation GetbusStation(string id);
        void addLine(busStation station);
        void removeStation(string id);
        void updateStation(busStation station);
        #endregion

        #region LineStation
        IEnumerable<busLineStation> GetAllbusLineStation();
        IEnumerable<busLineStation> GetAllbusLineStationBy(Predicate<busLineStation> predicate);
        busLineStation GetbusLineStation(string id);
        void addLine(busLineStation line);
        void removebusLineStation(string id);
        void updatebusLineStation(busLineStation line);
        #endregion

        #region User
        IEnumerable<User> GetAllbusUsers();
        IEnumerable<User> GetAllbusUsersBY(Predicate<User> predicate);
        User GetbusLineUser(string id);
        void addLine(User line);
        void removebusUser(string id);
        void updatebusUser(User line);
        #endregion


    }
}
