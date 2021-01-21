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
        void startTimer(Bus bus, TimeSpan time, string status, string iconPath,int timeAcceleration,double distance=0,int lineId=0);
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
        void canMakeDrive(Bus bus, double distance);
        #endregion

        #region busLine
        IEnumerable<BusLine> GetAllbusLines();
        BusLine GetBusLine(int id);
        void addLine(string number, int area,List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time, out int id);
        int countLines(string number);
        void removeLine(int id);
        void updateLine(int id, string number, int area, List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time);
        TimeSpan calcDriveTime(List<TimeSpan> time);
        double GetTotalDistanceLine(int id);
        bool LineInBusAtDrive(int lineId);

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
        void addUser(BO.User user);
        void removeUser(int id);
        User authenticate(string username, string password);
        int indexOfCbByAccessLevel(int id);
        void sendMail(int id, string subject, string text);
        User checkMail(string userName, string mailAddress);
        void updateUser(User user);
        string resetPassword(User user);

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
