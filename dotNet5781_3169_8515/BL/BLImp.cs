using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;

namespace BL
{

    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        BO.Bus studentDoBoAdapter(DO.Bus busDO)
        {
            BO. Bus studentBO = new BO.Bus();
            DO.Bus personDO;
            string id = busDO.id;
            try
            {
                personDO = dl.GetBus(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Student ID is illegal", ex);
            }
            personDO.DeepCopyTo(studentBO);
           

            busDO.DeepCopyTo(studentBO);         
            
            return studentBO;
        }

        public void addBus(Bus bus)
        {
            //do input checks
            dl.addBus(DOBOConvertor<DO.Bus, BO.Bus>(bus));
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
                        select studentDoBoAdapter(item)).ToList();
            return default;

            //DOBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLine> GetAllbusLines()
        {
            var result = dl.GetAllbusLines();
                if(result!=null)
                return from item in result 
                       where item!=null && item.enabled == true
                   select DOBOConvertor<BO.busLine,DO.busLine>(item);
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
            throw new NotImplementedException();
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
        private T DOBOConvertor<T,S>(S line) where T: new ()
        {
            T output =new T();
            output.DeepCopyTo(line);
            return output;

        }
    }
}
