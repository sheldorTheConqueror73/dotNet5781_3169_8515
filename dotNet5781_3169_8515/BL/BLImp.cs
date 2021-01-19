using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data;
using System.ComponentModel;

namespace BL
{

    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();
        static Timer TimerInstance = null;
        ProgressChangedEventHandler handler = null;
        int timeAcceleration = 1;

        #region Timer
       public void setTimeAcceleration(int timeAcceleration)
        {
            this.timeAcceleration = timeAcceleration;
        }

        public void setTimer(ProgressChangedEventHandler doWork)
        {
            handler = doWork;
        }
        public ProgressChangedEventHandler getTimer()
        {
            return handler;
        }
        public void startTimer(Bus bus, TimeSpan time, string status, string iconPath,int timeAcceleration=1)
        {
            this.timeAcceleration = timeAcceleration;
            if (TimerInstance == null)
                TimerInstance = new Timer();
            dl.updateTime(bus.id, time);
            dl.updateStatus(bus.id, status, iconPath);
            Timer.add(bus.id);
        }
        public void stopTimer(int id)
        {
            if (TimerInstance == null)
                return;
            Timer.remove(id);

        }
        public void Tick(int id)
        {
            DO.Bus bus = dl.GetBus(id);
            if (bus.time == TimeSpan.Zero)
            {
                stopTimer(bus.id);
                if (bus.dangerous == true && bus.status == "refuling")
                    dl.updateStatus(id, "dangerous", "Resources/warningIcon .png");
                else
                    dl.updateStatus(id, "ready", "Resources/okIcon.png");
                return;
            }
            bus.time += TimeSpan.FromSeconds(-1*timeAcceleration);
            if (bus.time < TimeSpan.Zero)
                bus.time = TimeSpan.Zero;
            dl.updateBus(bus);
        }
        #endregion
        #region bus 
       public void canMakeDrive(Bus bus, double distance)
        {
            if (bus.dangerous)
                throw new BusCanntoMakeDriveException("bus is dangerous");
            if(bus.distance+distance>=20000)
                throw new BusCanntoMakeDriveException("not eought distance untill maintenance");
            if(bus.fuel-distance<0)
                throw new BusCanntoMakeDriveException("not eought fuel");
        }

        public IEnumerable<Bus> GetAllFreeBuses()
        {
           return from bus in dl.GetAllBuses()
            where bus != null && bus.enabled && bus.status == "ready" && bus.time == TimeSpan.Zero
            orderby bus.plateNumber
            select Utility.DOtoBOConvertor<BO.Bus,DO.Bus>(bus);
        }
        

        /// <summary>
        /// add new bus to data
        /// </summary>
        /// <param name="bus">the new bus </param>
        public void addBus(Bus bus)
        {

            bus.formatPlateNumber();
            bus.UpdateDangerous();
            dl.addBus(Utility.BOtoDOConvertor<DO.Bus, BO.Bus>(bus));
        }
        /// <summary>
        /// update specific bus
        /// </summary>
        /// <param name="bus">the updated bus</param>
        public void updateBus(Bus bus)
        {
            bus.UpdateDangerous();
            bus.formatPlateNumber();
            dl.updateBus(Utility.BOtoDOConvertor<DO.Bus, BO.Bus>(bus));
        }
        /// <summary>
        /// remove specific bus
        /// </summary>
        /// <param name="id">id of the removing bus</param>
        public void removeBus(int id)
        {
            var bs = GetBus(id);
            if(bs.status!="ready"&& bs.status != "dangerous")
                throw new BusBusyException("Error: can't delete a bus that it's busy!");
            dl.removeBus(id);
        }
        /// <summary>
        /// return bus by id
        /// </summary>
        /// <param name="id">id of the requested bus</param>
        public Bus GetBus(int id)
        {
            return Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(dl.GetBus(id));
        }

        
        public IEnumerable<Bus> GetAllBuses(int order=1)
        {
            var result = dl.GetAllBuses();
            if (result != null)
            {
                if(order==1||order==0)
                return (from item in result
                        where (item != null && item.enabled == true)
                        orderby item.plateNumber ascending
                        select Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
                if (order == 2)
                    return (from item in result
                            where (item != null && item.enabled == true)
                            orderby item.iconPath ascending, item.plateNumber ascending
                            select Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
                if(order==3)
                    return (from item in result
                            where (item != null && item.enabled == true)
                            orderby item.time.TotalSeconds descending, item.plateNumber ascending
                            select Utility.DOtoBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
            }
            return default;
            //DOBOConvertor<BO.Bus, DO.Bus>(item)).ToList();
        }

       
        /// <summary>
        /// send bus to refuel
        /// </summary>
        /// <param name="id">id of the bus that sending to refuel</param>
        public void refuel(int id)
        {
            var bus = this.GetBus(id);
            dl.refuel(id);
        }
        /// <summary>
        /// send bus to maintenance
        /// </summary>
        /// <param name="id">id of the bus that sending to maintenance</param>
        public void maintain(int id)
        {
            var bus = this.GetBus(id);
            dl.maintain(id);
        }


        #endregion

        #region lines
        /// <summary>
        /// return specific line
        /// </summary>
        /// <param name="id">id of the requested line</param>
        public BusLine GetBusLine(int id)
        {
           return Utility.DOtoBOConvertor<BO.BusLine, DO.BusLine>(dl.GetBusLine(id));
        }
        
        /// <summary>
        /// return list of all the lines.
        /// </summary>
        public IEnumerable<BusLine> GetAllbusLines()
        {
             var result = dl.GetAllbusLines();
                 if(result!=null)
                 return from item in result 
                        where item!=null && item.enabled == true
                        orderby int.Parse(item.number) ascending
                    select (Utility.DOtoBOConvertor<BO.BusLine,DO.BusLine>(item));
            return default;

        }

       
        /// <summary>
        /// remove line
        /// </summary>
        /// <param name="id">id of the removing line</param>
        public void removeLine(int id)
        {
            dl.removeFollowStation(id);
            dl.removeLineInStation(id);
            dl.removeLine(id);
        }

        /// <summary>
        /// get data of line and id of line and update it.
        /// </summary>
        public void updateLine(int id,string number, int area, List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time)
        {
            TimeSpan drivetime = this.calcDriveTime(time);
            if (drivetime.Days > 0)
                throw new InvalidUserInputExecption("Bus line route must be less than a day");
            this.removeLine(id);
            this.addLine(number, area, path, distance, time);
        }
        /// <summary>
        /// add line to the database.
        /// </summary>
        public void addLine(string number, int area, List<BO.BusLineStation> path, List<double> distance, List<TimeSpan> time)
        {
            int count = countLines(number);
            TimeSpan drivetime=this.calcDriveTime(time);
            if (count == 2)
                throw new BusLimitExceededExecption("There are already two bus with that number");
            if(count==1)
            {
                int id = dl.GetBusLineID(number);
                var result = (from lis in dl.GetAllLineInStation()
                             where lis.Lineid==id
                             orderby lis.placeOrder ascending
                             select lis).ToList();
                if(result[0].stationid!=path[path.Count-1].id || result[result.Count-1].stationid!=path[0].id)
                    throw new BusLimitExceededExecption($"The second {number} line bust be going in the oppesite diraction");
            }
            BusLine line = new BusLine() { number = number, area = (Area)area, driveTime = drivetime.ToString(), enabled=true };   
            dl.addLine(Utility.BOtoDOConvertor<DO.BusLine, BO.BusLine>(line));
            for(int i=0;i<path.Count;i++)
            {
                dl.addLineInStation(new DO.LineInStation() { stationid=path[i].id, Lineid=line.id, placeOrder=i,lineNumber=line.number });
                if(i!=path.Count-1)
                {
                    dl.addFollowStation(new DO.FollowStations() { firstStationid=path[i].id, enabled=true,distance=distance[i], driveTime=time[i],lineId=line.id,secondStationid=path[i+1].id ,lineNumber=line.number});
                }
            }
        
        }
        /// <summary>
        /// return how many lines exists with this number
        /// </summary>
        /// <param name="number">the number of the line</param>
        public int countLines(string number)
        {
            if (dl.GetAllbusLines() != null)
                return (from line in dl.GetAllbusLines()
                        where line != null && line.enabled == true && line.number == number
                        select line).ToList().Count;
            return 0;
        }
        /// <summary>
        /// calculates the sum of all timespans in time parameter
        /// </summary>
        /// <param name="time">list of time spans to add</param>
        /// <returns>sum of all timespans</returns>
        public TimeSpan calcDriveTime(List<TimeSpan> time)
        {
            TimeSpan drivetime = new TimeSpan();
            foreach (var element in time)
            {
                drivetime += element;
            }
            return drivetime;
        }

        public double GetTotalDistanceLine(int id)
        {
            var pathLine = GetAllStationInLine(id);
            double sumDis = 0;
            foreach(var sta in pathLine)
            {
                if (sta.Distance.ToString().IndexOf('.') != -1)
                    sumDis += sta.Distance;
                else
                    sumDis += (sta.Distance / 1000);
            }
            return sumDis;
        }

        #endregion


        #region lineInStation
        /// <summary>
        ///  return all the lines in specific station
        /// </summary>
        /// <param name="id"> id of station</param>
        public IEnumerable<BusLine> GetAllLinesInStation(int id)
        { 
            List<string> linInSta = new List<string>();
            var r = GetAllFollowStationsAsStationsObj(id);
            foreach (var sta in GetAllFollowStationsAsStationsObj(id))
                foreach (var folSta in dl.GetAllFollowStation())
                    if (folSta != null && folSta.enabled == true && folSta.firstStationid == id && folSta.secondStationid == sta.id)
                        linInSta.Add(folSta.lineNumber);

            foreach (var linSta in dl.GetAllLineInStation())
                if (!linInSta.Any(x => x == linSta.lineNumber)&&linSta.stationid==id)
                    linInSta.Add(linSta.lineNumber);
            var res= linInSta.GroupBy(x => x).Select(y => y.First());
            List<BusLine> bs = new List<BusLine>();
            foreach(var v in res)
            {
                foreach(var v2 in dl.GetAllbusLines())
                    if(v==v2.number)
                    {
                        bs.Add(Utility.DOtoBOConvertor<BO.BusLine, DO.BusLine>(v2));
                        break;
                    }
            }
            return bs;
        }

       public void reconstructTimeAndDistance(int lineID, out List<double> distance, out List<TimeSpan> time)
        {
            distance = new List<double>();
            time = new List<TimeSpan>();
            var result = from station in dl.GetAllLineInStation()
                         where station != null && station.Lineid == lineID
                         orderby station.placeOrder ascending
                         select station;
            var result2 = from item in dl.GetAllFollowStation()
                          where item != null && item.enabled == true && item.lineId == lineID
                          select item;
            for(int i=1;i<result.Count();i++)
            {
                foreach (var element2 in result2)
                {
                    if(result.ToList()[i].stationid==element2.secondStationid)
                    {
                        time.Add(element2.driveTime);
                        distance.Add(element2.distance);
                        break;
                    }
                }
            }


        }
        #endregion

        #region station
        /// <summary>
        /// get station and add it to the database
        /// </summary>
        /// <param name="station">the new station</param>
        public void addStation(BusLineStation station)
        {
            var res = (from sta in dl.GetAllbusLineStation()
                      where sta != null && sta.code == station.code
                      select sta).FirstOrDefault();
           if(res!=null&&res.enabled==true)
                    throw new itemAlreadyExistsException($"Code number {station.code} is already taken");
            dl.addStation(Utility.BOtoDOConvertor<DO.BusLineStation, BO.BusLineStation>(station));
        }
        /// <summary>
        /// return IEnumerable of all stations
        /// </summary>
        public IEnumerable<BusLineStation> GetAllbusLineStation()
        {
            var result = dl.GetAllbusLineStation();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        orderby item.code ascending
                        select Utility.DOtoBOConvertor<BO.BusLineStation, DO.BusLineStation>(item)).ToList();
            return default;
        }

        
        /// <summary>
        /// return the path of specific line
        /// </summary>
        /// <param name="id">id of line</param>
        public IEnumerable<BusLineStation> GetAllStationInLine(int id)
        {
            //var line=DOtoBOConvertor<BO.busLine, DO.busLine>(dl.GetBusLine(id));
            if (dl.GetAllbusLineStation() != null && dl.GetAllLineInStation() != null)
            {
                var station = (from sta in dl.GetAllbusLineStation()
                               where sta != null && sta.enabled == true
                               from linInSta in dl.GetAllLineInStation()
                               where linInSta != null && linInSta.Lineid == id && linInSta.stationid == sta.id
                               orderby linInSta.placeOrder ascending
                               select Utility.DOtoBOConvertor<BO.BusLineStation, DO.BusLineStation>(sta)).ToList();
                bool flag = true;
                if(dl.GetAllFollowStation()!=null)
                    foreach(var sta in station)
                    {
                        if (flag)
                        {
                            flag = false;
                            continue;
                        }
                        sta.Distance = dl.GetAllFollowStation().First(x => x.secondStationid == sta.id && x.lineId == id).distance;
                        sta.DriveTime = dl.GetAllFollowStation().First(x => x.secondStationid == sta.id && x.lineId == id).driveTime;
                    }
                return station;
            }           
                return default;
        }
        
        /// <summary>
        /// remove station
        /// </summary>
        /// <param name="id">id of station</param>
        public void removeStation(int id)
        {
            var v1=(from fl in dl.GetAllFollowStation()
                   where fl.firstStationid==id
                   select fl.lineId);
            int idsta1 = 0,idsta2=0,idfol1=0,idfol2=0;
            double dist = 0;
            bool flagFirst = false, flagSecond=false;
            TimeSpan ts=new TimeSpan();
            if(v1.Count()!=0)
            foreach(var lin in dl.GetAllbusLines())
            {
                if (v1.Any(b => b == lin.id))
                {
                     flagFirst = false; flagSecond = false;
                    foreach (var folsta in dl.GetAllFollowStation())
                    {
                        if (folsta.secondStationid == id && folsta.lineId == lin.id)
                        {
                            idsta1 = folsta.firstStationid;
                            idfol1 = folsta.id;
                            dist += folsta.distance;
                            ts += folsta.driveTime;
                            flagFirst = true;
                        }
                        if (folsta.firstStationid == id && folsta.lineId == lin.id)
                        {
                             idsta2 = folsta.secondStationid;
                             idfol2 = folsta.id;
                             dist += folsta.distance;
                             ts += folsta.driveTime;
                             flagSecond = true;
                        }
                    }
                        if (flagFirst && flagSecond)
                        {
                            dl.updateFollowStation(new DO.FollowStations() { id = idfol1, firstStationid = idsta1, secondStationid = idsta2, lineId = lin.id, distance = dist, driveTime = ts, enabled = true,lineNumber=lin.number });
                            dl.removeFollowStationByIdOfFol(idfol2);

                        }
                    }
            }
            dl.removebusLineStation(id);

        }

        /// <summary>
        /// insert the data to file
        /// </summary>
        public void listToText()
        {
            string LineInsta="", lin="", folSta = "";
            lin = " Lines = new List<busLine>{";
            int cnt = 0;
            foreach (var v1 in dl.GetAllbusLines())
            {
                lin += "new busLine(){"+$"number=\"{v1.number}\",id={v1.id},area=Area.{v1.area},driveTime=\"{v1.driveTime}\",enabled=true"+"}";
                if (cnt != dl.GetAllbusLines().Count()-1)
                    lin += ",\n";
                cnt++;
            }
            cnt = 0;
            lin += "};\n\n\n";
            lin += "####################################################################################\n\n";
            LineInsta = "lineInStations = new List<lineInStation>{";
            foreach (var v1 in dl.GetAllLineInStation())
            {
                LineInsta += "new lineInStation(){" + $"id={v1.id},stationid={v1.stationid},Lineid={v1.Lineid},placeOrder={v1.placeOrder}" + "}";
                if (cnt != dl.GetAllLineInStation().Count() - 1)
                    LineInsta += ",\n";
                cnt++;
            }
            LineInsta += "};\n\n";
            LineInsta += "####################################################################################\n\n";

            cnt = 0;
            folSta = "followStation = new List<followStations>{";
            foreach (var v1 in dl.GetAllFollowStation())
            {
                folSta += "new followStations(){" + $"id={v1.id},lineId={v1.lineId},secondStationid={v1.secondStationid},firstStationid={v1.firstStationid},enabled=true,driveTime=TimeSpan.Parse(\"{v1.driveTime}\"),distance={v1.distance}" + "}";
                if (cnt != dl.GetAllFollowStation().Count() - 1)
                    folSta += ",\n";
                cnt++;
            }
            folSta += "};";
            File.WriteAllText( "C:\\Users\\LENOVO\\source\\repos\\sheldorTheConqueror73\\dotNet5781_3169_8515\\dotNet5781_3169_8515\\initList.txt",lin + LineInsta + folSta);
        }
        
        /// <summary>
        /// update specific station
        /// </summary>
        /// <param name="station">the updated station</param>
        public void updateStation(BusLineStation station)
        {
            dl.updatebusLineStation(Utility.BOtoDOConvertor<DO.BusLineStation, BO.BusLineStation>(station));
        }
       
        /// <summary>
        /// return all the stations that not exist in path of line.
        /// </summary>
        /// <param name="id"></param>
        public IEnumerable<BusLineStation> GetAllStationNotInLine(int id)
        {
            /* if (dl.GetAllbusLineStation() != null && dl.GetAllLineInStation() != null)
             {
                 return (from sta in dl.GetAllbusLineStation()
                          where sta != null && sta.enabled == true
                          from linInSta in dl.GetAllLineInStation()
                          where linInSta != null && linInSta.Lineid != id && linInSta.stationid == sta.id
                          orderby linInSta.placeOrder ascending
                          select Utility.DOtoBOConvertor<BO.BusLineStation, DO.BusLineStation>(sta)).ToList().GroupBy(x => x.id).Select(y => y.FirstOrDefault());
             }*/
            List<BusLineStation> ls = new List<BusLineStation>();
             if (dl.GetAllbusLineStation() != null && dl.GetAllLineInStation() != null)
            {
                var linSta = from linInSta in dl.GetAllLineInStation()
                             where linInSta != null && linInSta.Lineid == id
                             select linInSta;
               foreach(var sta in dl.GetAllbusLineStation())
                {
                    if (!linSta.Any(x => x.stationid == sta.id))
                        ls.Add(Utility.DOtoBOConvertor<BO.BusLineStation, DO.BusLineStation>(sta));
                }
                return ls;
            }
            return default;
        }
        #endregion


        #region followStations
        /// <summary>
        /// return list of all followstations objects
        /// </summary>
        /// <param name="id"></param>
        private IEnumerable<FollowStations> GetAllFollowStations(int id)
        {
            var folllowStation = dl.GetAllFollowStation();
            if (folllowStation != null)
                return (from folSta in folllowStation
                        where folSta != null && folSta.enabled == true && folSta.firstStationid==id
                        select Utility.DOtoBOConvertor<BO.FollowStations, DO.FollowStations>(folSta)).ToList();
            return default;
        }
        /// <summary>
        /// return the all the follow stations of specific one as station object
        /// </summary>
        /// <param name="id">id of station</param>
        public IEnumerable<BusLineStation> GetAllFollowStationsAsStationsObj(int id)
        {
            var folllowStation = GetAllFollowStations(id);
            var stations = dl.GetAllbusLineStation();
            if (folllowStation != null&& stations!=null)
                return (from sta in stations
                        from folSta in folllowStation where folSta != null && folSta.enabled == true                    
                        where sta!=null&&sta.enabled==true && folSta.firstStationid == id &&folSta.secondStationid==sta.id
                        let x=sta.Distance=folSta.distance
                        let y=sta.DriveTime=folSta.driveTime
                        select (Utility.DOtoBOConvertor<BO.BusLineStation, DO.BusLineStation>(sta))).ToList();
            return default;
        }


        /// <summary>
        /// update follow station and drive time of line.
        /// </summary>
        /// <param name="folStation">the updated follow station</param>
        /// <param name="tTs">the total drive time</param>
        /// <param name="bTs">the time before the changing</param>
        /// <param name="aTs">the time after the changing</param>
        public void updateFollowStation(FollowStations folStation,TimeSpan tTs,TimeSpan bTs,TimeSpan aTs)
        {
            tTs -= bTs;
            tTs += aTs;
            dl.updateFollowStation(Utility.BOtoDOConvertor<DO.FollowStations, BO.FollowStations>(folStation));
            var line = GetBusLine(folStation.lineId);
            line.driveTime = tTs.ToString();
            dl.updateLine(Utility.BOtoDOConvertor<DO.BusLine, BO.BusLine>(line));
        }
        /// <summary>
        /// return the id of follow station object by firest station+second station+line id
        /// </summary>
        /// <param name="idFirstSta">id of the first station in object follow station</param>
        /// <param name="idSecondSta">id of the second station in object follow station</param>
        /// <param name="idLine">id of the line in object follow station</param>
        /// <returns></returns>
        public int GetIdFollowStationBy(int idFirstSta, int idSecondSta,int idLine)
        {
            foreach (var folSta in dl.GetAllFollowStation())
            {
                if (folSta.firstStationid == idFirstSta && folSta.secondStationid == idSecondSta && folSta.lineId == idLine)
                    return folSta.id;
            }
            return default;
        }
        #endregion
        #region user
        public IEnumerable<BO.User> GetAllUsers()
        {
            var result = dl.GetAllbusUsers();
            if (result != null)
                return (from item in result
                        where item != null && item.enabled == true
                        select Utility.DOtoBOConvertor<BO.User, DO.User>(item)).ToList();
            return default;
        }
        public BO.User GetUser(int id)
        {
            try 
            {
                return Utility.DOtoBOConvertor<BO.User, DO.User>(dl.GetUser(id));            
            }
            catch (Exception e)
            {
                throw new noMatchExeption(e.Message);
            }
          
        }
        public string authenticate(string username, string password, out int id)
        {

            foreach (var user in this.GetAllUsers())
                if (user.enabled == true && user.name == username && user.password == password)
                {
                    id = user.id;
                    return user.accessLevel.ToString();
                }
            id =-1;
            throw new credentialsIncorrectException("Inncorrect Credentials. please try again");
        }
        public void addUser(BO.User user)
        {
            user.accessLevel ="User";
            dl.addUser(Utility.BOtoDOConvertor<DO.User,BO.User>(user));
        }

        #endregion

        #region exportToExcel
        public void ConvertToExcel(string fileXmlPath,string fileXlName)
        {
            if (fileXlName != "" && fileXmlPath != "") // using Custome Xml File Name  
            {
                if (File.Exists(fileXmlPath))
                {
                    string CustXmlFilePath = Path.Combine(new FileInfo(fileXmlPath).DirectoryName, fileXlName); // Ceating Path for Xml Files  
                    System.Data.DataTable dt = CreateDataTableFromXml(fileXmlPath);
                    ExportDataTableToExcel(dt, CustXmlFilePath);
                }

            }
            else if ( fileXmlPath != "") // Using Default Xml File Name  
            {
                if (File.Exists(fileXmlPath))
                {
                    FileInfo fi = new FileInfo(fileXmlPath);
                    string XlFile = fi.DirectoryName + "\\" + fi.Name.Replace(fi.Extension, ".xlsx");
                    System.Data.DataTable dt = CreateDataTableFromXml(fileXmlPath);
                    ExportDataTableToExcel(dt, XlFile);
                }
            }
        }

        private System.Data.DataTable CreateDataTableFromXml(string XmlFile)
        {

            System.Data.DataTable Dt = new System.Data.DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(XmlFile);
                Dt.Load(ds.CreateDataReader());

            }
            catch (Exception ex)
            {

            }
            return Dt;
        }
        private void ExportDataTableToExcel(System.Data.DataTable table, string Xlfile)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook book = excel.Application.Workbooks.Add(Type.Missing);
            excel.Visible = false;
            excel.DisplayAlerts = false;
            Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)book.ActiveSheet;
            excelWorkSheet.Name = table.TableName;
        
            for (int i = 1; i < table.Columns.Count + 1; i++) // Creating Header Column In Excel  
            {
                excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                
            }
   
            for (int j = 0; j < table.Rows.Count; j++) // Exporting Rows in Excel  
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                }
     
            }

            book.SaveAs(Xlfile);
            book.Close(true);
            excel.Quit();

            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(excel);

        }

        #endregion

        #region History
       public IEnumerable<BO.LineHistory> GetLineHistory()
        {
            return from line in dl.GetLineHistory()
                   where line != null
                   select Utility.DOtoBOConvertor<BO.LineHistory, DO.LineHistory>(line);
        }

       public IEnumerable<BO.BusHistory> getBusHistory()
        {
            return from bus in dl.getBusHistory()
                   where bus != null
                   select Utility.DOtoBOConvertor<BO.BusHistory, DO.BusHistory>(bus);
        }

        public void addLineHistory(BO.LineHistory history)
        {
            dl.addLineHistory(Utility.BOtoDOConvertor<DO.LineHistory, BO.LineHistory>(history));
        }
        public void addBusHistory(BO.BusHistory history)
        {
            dl.addBusHistory(Utility.BOtoDOConvertor<DO.BusHistory, BO.BusHistory>(history));
        }
        #endregion

    }
}
