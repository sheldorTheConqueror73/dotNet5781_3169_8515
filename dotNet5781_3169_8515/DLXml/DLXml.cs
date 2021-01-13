using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void updateBus(Bus bus)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            return   from element in Utility.load(typeof(Bus)).Elements()
                         let obj = element.ToObject<Bus>()
                         where element != null && obj.enabled == true // change elemnt to obj
                         select obj;
        }

        public void addBus(Bus bus)
        {
            var result = GetBus(bus.id);
            if(result!=null)
                throw new itemAlreadyExistsException($"ID number {bus.id} is already taken");
            var root = Utility.load(typeof(Bus));
                root.Add(bus.ToXml());
            Utility.save(root,typeof(Bus));
        }

        public Bus GetBus(int id)
        {
            return (from element in Utility.load(typeof(Bus)).Elements()
                   let obj = element.ToObject<Bus>()
                   where element != null && obj.enabled == true && obj.id==id // change elemnt to obj
                   select obj).FirstOrDefault();
        }
        public void removeBus(int id)
        {
          
        }

        public void maintain(int id)
        {
            throw new NotImplementedException();
        }

        public void refuel(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region User
        public void addUser(User user)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllbusUsers()
        {
            throw new NotImplementedException();
        }
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Line
        public void addLine(BusLine line)
        {
            throw new NotImplementedException();
        }
        public int countLines(string number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusLine> GetAllbusLines()
        {
            throw new NotImplementedException();
        }

        public BusLine GetBusLine(int id)
        {
            throw new NotImplementedException();
        }

        public int GetBusLineID(string number)
        {
            throw new NotImplementedException();
        }

        public void removeLine(int id)
        {
            throw new NotImplementedException();
        }

        public void updateLine(BusLine line)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region LineInStation
        public void addLineInStation(LineInStation lis)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineInStation> GetAllLineInStation()
        {
            throw new NotImplementedException();
        }


        public BusLineStation GetbusLineStation(int id)
        {
            throw new NotImplementedException();
        }

        public void removeLineInStation(int lineId)
        {
            throw new NotImplementedException();
        }


        #endregion
        #region station
        public void addStation(BusLineStation station)
        {
            throw new NotImplementedException();
        }




        public IEnumerable<BusLineStation> GetAllbusLineStation()
        {
            throw new NotImplementedException();
        }


        public void updatebusLineStation(BusLineStation line)
        {
            throw new NotImplementedException();
        }

        public void removebusLineStation(int id)
        {
            throw new NotImplementedException();
        }


        #endregion
        #region FollowStation
        public void addFollowStation(FollowStations folStation)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<FollowStations> GetAllFollowStation()
        {
            throw new NotImplementedException();
        }



        public void removeFollowStation(int LineId)
        {
            throw new NotImplementedException();
        }

        public void removeFollowStationByIdOfFol(int Id)
        {
            throw new NotImplementedException();
        }



        public void updateFollowStation(FollowStations folStation)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion
        #endregion
    }
}
