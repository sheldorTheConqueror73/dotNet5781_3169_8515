using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   
    class BLImp : IBL
    {
        IDal dl = DLFactory.GetDL();

        public void addBus(Bus bus)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<busLine> GetAllbusLines()
        {
            throw new NotImplementedException();
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
    }
}
