using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLAPI
{
    /// <summary>
    /// all comments appear in BLImp class
    /// </summary>
    public interface IBL
    {

        #region bus
        IEnumerable<Bus> GetAllBuses(int order=0);
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(int id);
        void addBus(Bus bus);
        void removeBus(int id);
        void updateBus(Bus bus);
        void refuel(int id);
        void maintain(int id);
        #endregion

        #region busLine
        IEnumerable<BusLine> GetAllbusLines();
        IEnumerable<BusLine> GetAllbusLinesBy(Predicate<BusLine> predicate);
        BusLine GetBusLine(int id);
        void addLine(string number, int area,List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time);
        void removeLine(int id);
        void updateLine(int id, string number, int area, List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time);
        #endregion

        #region Station
        void addStation(BusLineStation station);
        IEnumerable<BusStation> GetAllbusStations();
        IEnumerable<BusStation> GetAllbusStationsBy(Predicate<BusStation> predicate);
        BusStation GetbusStation(int id);
        void addLine(BusStation station);
        void removeStation(int id);
        void updateStation(BusLineStation station);
        IEnumerable<BusLine> GetAllLinesInStation(int id);
        IEnumerable<BusLineStation> GetAllStationInLine(int id);
        IEnumerable<BusLineStation> GetAllStationNotInLine(int id);
        #endregion

        #region LineStation
        IEnumerable<BusLineStation> GetAllbusLineStation();
  
        BusLineStation GetbusLineStation(int id);
        void addLine(BusLineStation line);
        void removebusLineStation(int id);
        void updatebusLineStation(BusLineStation line);
        void reconstructTimeAndDistance(int lineID, out List<double> distance, out List<TimeSpan> time);
        #endregion

        #region user
        IEnumerable<BO.User> GetAllUsers();
        BO.User GetUser(int id);
        void addUser(BO.User line);
        void removeUser(int id);
         string authenticate(string username, string password, out int id);
        #endregion
        #region followStations
        IEnumerable<BusLineStation> GetAllFollowStationsAsStationsObj(int id);
        void updateFollowStation(FollowStations folStation, TimeSpan sTs, TimeSpan eTs, TimeSpan cTs);
        int GetIdFollowStationBy(int idFirstSta, int idSecondSta, int idLine);
        #endregion

        void listToText();
    }
}
