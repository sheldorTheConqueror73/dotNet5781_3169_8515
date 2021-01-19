using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BO;


namespace BLAPI
{
    /// <summary>
    /// all comments appear in BLImp class
    /// </summary>
    public interface IBL
    {
        #region Timer
        void Tick(int id);
        void setTimer(ProgressChangedEventHandler doWork);
        ProgressChangedEventHandler getTimer();
        void startTimer(Bus bus, TimeSpan time, string status, string iconPath,int timeAcceleration);
        void stopTimer(int id);
        void setTimeAcceleration(int timeAcceleration);
        #endregion
        #region bus
        IEnumerable<Bus> GetAllBuses(int order=0);
        IEnumerable<Bus> GetAllFreeBuses();
        Bus GetBus(int id);
        void addBus(Bus bus);
        void removeBus(int id);
        void updateBus(Bus bus);
        void refuel(int id);
        void maintain(int id);
        #endregion

        #region busLine
        IEnumerable<BusLine> GetAllbusLines();
        BusLine GetBusLine(int id);
        void addLine(string number, int area,List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time);
        int countLines(string number);
        void removeLine(int id);
        void updateLine(int id, string number, int area, List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time);
        TimeSpan calcDriveTime(List<TimeSpan> time);

        #endregion

        #region Station
        void addStation(BusLineStation station);    
        void removeStation(int id);
        void updateStation(BusLineStation station);
        IEnumerable<BusLine> GetAllLinesInStation(int id);
        IEnumerable<BusLineStation> GetAllStationInLine(int id);
        IEnumerable<BusLineStation> GetAllStationNotInLine(int id);
        #endregion

        #region LineStation
        IEnumerable<BusLineStation> GetAllbusLineStation();  
        void reconstructTimeAndDistance(int lineID, out List<double> distance, out List<TimeSpan> time);
        #endregion

        #region user
        IEnumerable<BO.User> GetAllUsers();
        BO.User GetUser(int id);
        void addUser(BO.User line);
         string authenticate(string username, string password, out int id);
        #endregion
        #region followStations
        IEnumerable<BusLineStation> GetAllFollowStationsAsStationsObj(int id);
        void updateFollowStation(FollowStations folStation, TimeSpan sTs, TimeSpan eTs, TimeSpan cTs);
        int GetIdFollowStationBy(int idFirstSta, int idSecondSta, int idLine);
        #endregion

        #region History
        IEnumerable<BO.LineHistory> GetLineHistory();

        IEnumerable<BO.BusHistory> getBusHistory();
        void addLineHistory(BO.LineHistory history);
        void addBusHistory(BO.BusHistory history);

        #endregion
        void listToText();
        void ConvertToExcel(string fileXmlPath, string fileXlName);
    }
}
