using System;
using System.Collections.Generic;

//using DO;

namespace DLAPI
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
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
        IEnumerable<DO.busLine> GetAllbusLines();
        DO.busLine GetBusLine(int id);
        void addLine(DO.busLine line);
        void removeLine(int id);
        void updateLine(DO.busLine line);
        #endregion

        #region LineStation
        void addStation(DO.busLineStation station);
        IEnumerable<DO.busLineStation> GetAllbusLineStation();
        DO.busLineStation GetbusLineStation(int id);
        void removebusLineStation(int id);
        void updatebusLineStation(DO.busLineStation line);
     
        #endregion

        #region User
        IEnumerable<DO.User> GetAllbusUsers();
        DO.User GetUser(int id);
        void addUser(DO.User user);
        #endregion

        #region lineInStation
        IEnumerable<DO.lineInStation> GetAllLineInStation();
        void addLineInStation(DO.lineInStation lis);

        #endregion

        #region followStations
        IEnumerable<DO.followStations> GetAllFollowStation();
        void updateFollowStation(DO.followStations folStation);
        void addFollowStation(DO.followStations folStation);

        #endregion
    }
}
