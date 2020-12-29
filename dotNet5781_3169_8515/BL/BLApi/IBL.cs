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
        List<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        Bus GetBus(int id);
        void addBus(Bus bus);
        void removeBus(int id);
        void updateBus(Bus bus);
        #endregion

        #region busLine
        IEnumerable<busLine> GetAllbusLines();
        IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate);
        busLine GetBusLine(int id);
        void addLine(busLine line);
        void removeLine(int id);
        void updateLine(busLine line);
        #endregion

        #region Station
        IEnumerable<busStation> GetAllbusStations();
        IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate);
        busStation GetbusStation(int id);
        void addLine(busStation station);
        void removeStation(int id);
        void updateStation(busStation station);
        IEnumerable<busLine> GetAllLinesInStation(int id);
        #endregion

        #region LineStation
        IEnumerable<busLineStation> GetAllbusLineStation();
  
        busLineStation GetbusLineStation(int id);
        void addLine(busLineStation line);
        void removebusLineStation(int id);
        void updatebusLineStation(busLineStation line);
      
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
        #endregion

    }
}
