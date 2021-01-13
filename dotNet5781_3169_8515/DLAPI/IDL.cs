using System;
using System.Collections.Generic;


namespace DLAPI
{
    /// <summary>
    /// all comments appear in DLObject class
    /// </summary>
    public interface IDL
    {
        #region bus
        IEnumerable<DO.Bus> GetAllBuses();
        DO.Bus GetBus(int id);
        void addBus(DO.Bus bus);
        void removeBus(int id);
        void updateBus(DO.Bus bus);
        void refuel(int id);
        void maintain(int id);
        
        #endregion

        #region busLine
        IEnumerable<DO.BusLine> GetAllbusLines();
        DO.BusLine GetBusLine(int id);
        int GetBusLineID(string number);
        void addLine(DO.BusLine line);
        void removeLine(int id);
        void updateLine(DO.BusLine line);
        int countLines(string number);
        #endregion

        #region LineStation
        void addStation(DO.BusLineStation station);
        IEnumerable<DO.BusLineStation> GetAllbusLineStation();
        DO.BusLineStation GetbusLineStation(int id);
        void removebusLineStation(int id);
        void updatebusLineStation(DO.BusLineStation line);
     
        #endregion

        #region User
        IEnumerable<DO.User> GetAllbusUsers();
        DO.User GetUser(int id);
        void addUser(DO.User user);
        #endregion

        #region lineInStation
        IEnumerable<DO.LineInStation> GetAllLineInStation();
        void addLineInStation(DO.LineInStation lis);
        void removeLineInStation(int lineId);
        #endregion

        #region followStations
        IEnumerable<DO.FollowStations> GetAllFollowStation();
        void updateFollowStation(DO.FollowStations folStation);
        void addFollowStation(DO.FollowStations folStation);
        void removeFollowStation(int LineId);
        void removeFollowStationByIdOfFol(int Id);

        #endregion

    }
}
