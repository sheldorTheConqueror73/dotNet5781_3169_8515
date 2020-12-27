using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using System.Reflection;

namespace BL
{

    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        public void addBus(Bus bus)
        {
            //do input checks
            dl.addBus(BOtoDOConvertor<DO.Bus, BO.Bus >(bus));
        }

        public void addLine(busLine line)
        {
            throw new NotImplementedException();
        }

        public void addLine(busStation station)
        {
            throw new NotImplementedException();
        }

        public void addLine(busLineStation line)
        {
            throw new NotImplementedException();
        }

        public List<Bus> GetAllBuses()
        {
            var result = dl.GetAllBuses();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        select DOtoBOConvertor<BO.Bus,DO.Bus>(item)).ToList();
            return default;

            //DOBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLine> GetAllbusLines()
        {
           /* var result = dl.GetAllbusLines();
                if(result!=null)
                return from item in result 
                       where item!=null && item.enabled == true
                   select DOtoBOConvertor<BO.busLine,DO.busLine>(item);*/
            return default;
            
        }

        public IEnumerable<busLine> GetAllbusLinesBy(Predicate<busLine> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLineStation> GetAllbusLineStation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLineStation> GetAllbusLineStationBy(Predicate<busLineStation> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busStation> GetAllbusStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busStation> GetAllbusStationsBy(Predicate<busStation> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string id)
        {
            throw new NotImplementedException();
        }

        public busLine GetBusLine(string id)
        {
            throw new NotImplementedException();
        }

        public busLineStation GetbusLineStation(string id)
        {
            throw new NotImplementedException();
        }

        public busStation GetbusStation(string id)
        {
            throw new NotImplementedException();
        }

        public void removeBus(string id)
        {
            throw new NotImplementedException();
        }

        public void removebusLineStation(string id)
        {
            throw new NotImplementedException();
        }

        public void removeLine(string id)
        {
            throw new NotImplementedException();
        }

        public void removeStation(string id)
        {
            throw new NotImplementedException();
        }

        public void updateBus(Bus bus)
        {
            dl.updateBus(BOtoDOConvertor<DO.Bus, BO.Bus>(bus));
        }

        public void updatebusLineStation(busLineStation line)
        {
            throw new NotImplementedException();
        }

        public void updateLine(busLine line)
        {
            throw new NotImplementedException();
        }

        public void updateStation(busStation station)
        {
            throw new NotImplementedException();
        }
        #region user
        public IEnumerable<BO.User> GetAllUsers()
        {
            var result = dl.GetAllbusUsers();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        select DOtoBOConvertor<BO.User, DO.User>(item)).ToList();
            return default;
        }
        public BO.User GetUser(string id)
        {
            try 
            {
                return DOtoBOConvertor<BO.User, DO.User>(dl.GetUser(id));            
            }
            catch (Exception e)
            {
                throw new noMatchExeption(e.Message);
            }
          
        }
        public string authenticate(string username, string password, out string id)
        {

            foreach (var user in this.GetAllUsers())
                if (user.enabled == true && user.name == username && user.password == password)
                {
                    id = user.id;
                    return user.accessLevel.ToString();
                }
            id = "";
            throw new credentialsIncorrectException("Inncorrect Credentials. please try again");
        }
        public void addUser(BO.User user)
        {
            throw new NotImplementedException();
        }
        public void removeUser(string id)
        {
            throw new NotImplementedException();
        }
        #endregion
        private T DOtoBOConvertor<T,S>(S line) where T  : BO.BOobject, new () where S : DO.DOobject, new()        {
            T output =new T();
            output.id = line.id;
            foreach (PropertyInfo propTo in output.GetType().GetProperties())
            {
                PropertyInfo propFrom = line.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                propTo.SetValue(output,propFrom.GetValue(line,null));
            }               
                return output;

        }

        private T BOtoDOConvertor<T, S>(S line) where T : DO.DOobject, new() where S :  BO.BOobject, new()
        {
            T output = new T();
            output.id = line.id;
            foreach (PropertyInfo propTo in output.GetType().GetProperties())
            {
                PropertyInfo propFrom = line.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                propTo.SetValue(output, propFrom.GetValue(line, null));
            }
            return output;

        }
    }
}
