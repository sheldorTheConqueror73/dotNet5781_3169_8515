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
        DO.Bus GetBus(string id);
        void addBus(DO.Bus bus);
        void removeBus(string id);
        void updateBus(DO.Bus bus);
        #endregion

        #region busLine
        IEnumerable<DO.busLine> GetAllbusLines();
        DO.busLine GetBusLine(string id);
        void addLine(DO.busLine line);
        void removeLine(string id);
        void updateLine(DO.busLine line);
        #endregion

        #region LineStation
        IEnumerable<DO.busLineStation> GetAllbusLineStation();
        DO.busLineStation GetbusLineStation(string id);
        void addLine(DO.busLineStation line);
        void removebusLineStation(string id);
        void updatebusLineStation(DO.busLineStation line);
        #endregion

        #region User
        IEnumerable<DO.User> GetAllbusUsers();
        DO.User GetbusLineUser(string id);
        void addLine(DO.User line);
        #endregion
    }
}
