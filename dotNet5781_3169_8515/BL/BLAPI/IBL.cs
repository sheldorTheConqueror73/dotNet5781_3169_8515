using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLAPI
{
    public interface IBL
    {

        #region bus
        List<Bus> GetAllBuses(int order=0);
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(int id);
        void addBus(Bus bus);
        void removeBus(int id);
        void updateBus(Bus bus);
        void refuel(int id);
        void maintain(int id);
        #endregion

        #region busLine
        IEnumerable<busLine> GetAllbusLines();
        IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate);
        busLine GetBusLine(int id);
        void addLine(string number, int area,List<BO.busLineStation> path, List<int> distance, List<TimeSpan> time);
        void removeLine(int id);
        void updateLine(int id, string number, int area, List<BO.busLineStation> path, List<int> distance, List<TimeSpan> time);
        #endregion

        #region Station
        void addStation(busLineStation station);
        IEnumerable<BusStation> GetAllbusStations();
        IEnumerable<BusStation> GetAllbusStationsBy(Predicate<BusStation> predicate);
        BusStation GetbusStation(int id);
        void addLine(BusStation station);
        void removeStation(int id);
        void updateStation(busLineStation station);
        IEnumerable<busLine> GetAllLinesInStation(int id);
        IEnumerable<busLineStation> GetAllStationInLine(int id);
        IEnumerable<busLineStation> GetAllStationNotInLine(int id);
        #endregion

        #region LineStation
        IEnumerable<busLineStation> GetAllbusLineStation();
  
        busLineStation GetbusLineStation(int id);
        void addLine(busLineStation line);
        void removebusLineStation(int id);
        void updatebusLineStation(busLineStation line);
        void reconstructTimeAndDistance(int lineID, out List<int> distance, out List<TimeSpan> time);
        #endregion

        #region user
        IEnumerable<BO.User> GetAllUsers();
        BO.User GetUser(int id);
        void addUser(BO.User line);
        void removeUser(int id);
         string authenticate(string username, string password, out int id);
        #endregion
        #region followStations
        IEnumerable<busLineStation> GetAllFollowStationsAsStationsObj(int id);
        void updateFollowStation(followStations folStation);
        int GetIdFollowStationBy(int idFirstSta, int idSecondSta, int idLine);
        #endregion

    }
}
