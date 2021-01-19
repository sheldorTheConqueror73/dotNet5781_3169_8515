using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DO;
namespace DL
{

    internal static class Utility
    {
        private static string dir = @"..\xml\";
        private static string busPath = @"Buses.xml";
        private static string linePath = @"Lines.xml";
        private static string stationPath = @"Stations.xml";
        private static string userPath = @"Users.xml";
        private static string lineInStationPath = @"LineInStations.xml";
        private static string followStationPath = @"FollowStations.xml";
        private static string LineHistoryPath = @"LineHistory.xml";
        private static string BusHistoryPath = @"BusHistory.xml";
        private static Mutex BusMutex = null;
        private static Mutex LineMutex = null;
        private static Mutex StationMutex = null;
        private static Mutex UserMutex = null;
        private static Mutex LISMutex = null;
        private static Mutex FollowMutex = null;
        private static Mutex LineHistoryMutex = null;
        private static Mutex BusHistoryMutex = null;
        private static int counter = 0;

        /// <summary>
        /// returns the path for the file where Type is stored
        /// </summary>
        /// <param name="type">type of class to choose</param>
        /// <returns>string containing path to file </returns>
        private static string getPath(Type type)
        {
            if (type == typeof(Bus))
                return busPath;
            if (type == typeof(BusLine))
                return linePath;
            if (type == typeof(BusLineStation))
                return stationPath;
            if (type == typeof(User))
                return userPath;
            if (type == typeof(LineInStation))
                return lineInStationPath;
            if (type == typeof(FollowStations))
                return followStationPath;
            if (type == typeof(BusHistory))
                return BusHistoryPath;
            if (type == typeof(LineHistory))
                return LineHistoryPath;
            throw new InvalidArgumentException("Invalid Argument");
        }
        private static Mutex getMutex(Type type)
        {
            counter++;
            if (type == typeof(Bus))
            {
                if (BusMutex == null)
                    BusMutex = new Mutex(false, "BusesMutex");
                return BusMutex;
            }
            if (type == typeof(BusLine))
            {
                if (LineMutex == null)
                    LineMutex = new Mutex(false, "LinesMutex");
                return LineMutex;
            }

            if (type == typeof(BusLineStation))
            {
                if (StationMutex == null)
                    StationMutex = new Mutex(false, "StationssMutex");
                return StationMutex;
            }
            if (type == typeof(User))
            {
                if (UserMutex == null)
                    UserMutex = new Mutex(false, "UsersMutex");
                return UserMutex;
            }
            if (type == typeof(LineInStation))
            {
                if (LISMutex == null)
                    LISMutex = new Mutex(false, "UsersMutex");
                return LISMutex;
            }
            if (type == typeof(FollowStations))
            {
                if (FollowMutex == null)
                    FollowMutex = new Mutex(false, "FollowsMutex");
                return FollowMutex;
            }
            if (type == typeof(BusHistory))
            {
                if (BusHistoryMutex == null)
                    BusHistoryMutex = new Mutex(false, "BusHMutex");
                return BusHistoryMutex;
            }
            if (type == typeof(LineHistory))
            {
                if (LineHistoryMutex == null)
                    LineHistoryMutex = new Mutex(false, "LineHMutex");
                return LineHistoryMutex;
            }
            throw new InvalidArgumentException("Invalid Argument");
        }


        private static string getName(Type type)
        {
            if (type == typeof(Bus))
                return "Buses";
            if (type == typeof(BusLine))
                return "Lines";
            if (type == typeof(BusLineStation))
                return "Station";
            if (type == typeof(User))
                return "Users";
            if (type == typeof(LineInStation))
                return "LinesInStations";
            if (type == typeof(FollowStations))
                return "FollowStations";
            if (type == typeof(LineHistory))
                return "LineHistory";
            if (type == typeof(BusHistory))
                return "BusHistory";
            throw new InvalidArgumentException("Invalid Argument");
        }



        /// <summary>
        /// casts an object to xml
        /// </summary>
        /// <typeparam name="T">class of object to cast</typeparam>
        /// <param name="data">object to cast</param>
        /// <returns>xml version of object</returns>
        public static XElement ToXml<T>(this T data) where T : DOobject
        {
            XElement root = new XElement(data.GetType().Name);
            root.Add(from prop in data.GetType().GetProperties()
                     select new XElement(prop.Name, prop.GetValue(data, null)));
            return root;
        }



        /// <summary>
        /// casts xml to currect class
        /// </summary>
        /// <typeparam name="T">type of class to cast to</typeparam>
        /// <param name="root">root element of XDocument</param>
        /// <returns>class version of xml element</returns>
        public static T ToObject<T>(this XElement root) where T : DOobject, new()
        {
            T obj = new T();
            foreach (var element in root.Elements())
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.Name == element.Name)
                    {
                        if (prop.PropertyType == typeof(bool))
                            prop.SetValue(obj, bool.Parse(element.Value));
                        else if (prop.PropertyType == typeof(int))
                            prop.SetValue(obj, int.Parse(element.Value));
                        else if (prop.PropertyType == typeof(double))
                            prop.SetValue(obj, double.Parse(element.Value));
                        else if (prop.PropertyType == typeof(TimeSpan))
                            prop.SetValue(obj, XmlConvert.ToTimeSpan(element.Value));
                        else if (prop.PropertyType == typeof(DateTime))
                            prop.SetValue(obj, DateTime.Parse(element.Value));
                        else if (prop.PropertyType == typeof(DO.Area))
                        {
                            DO.Area a; ;
                            Enum.TryParse<DO.Area>(element.Value, out a);
                            prop.SetValue(obj, a);
                        }
                        else
                            prop.SetValue(obj, element.Value);

                    }
                }

            }
            return obj;
        }

        /// <summary>
        /// saves data in xml file
        /// </summary>
        /// <param name="root">XElement to save</param>
        /// <param name="type">Type of calss file</param>
        public static void save(XElement root, Type type)
        {
            Mutex mutex = getMutex(type);
            string path = getPath(type);
            XDocument document = new XDocument(root);
            if (mutex.WaitOne())
            {
                try
                {
                    document.Save(dir + path);
                }

                catch (Exception e) { Console.WriteLine(e.Message); }
                mutex.ReleaseMutex();
                return;
            }
            throw new unexpectedException("saving mutex has failed");
        }
        /// <summary>
        /// loads data from file
        /// </summary>
        /// <param name="type">type of class file</param>
        /// <returns>root XElement of file</returns>
        public static XElement load(Type type)
        {
            Mutex mutex = getMutex(type);
            string filePath = getPath(type);
            if(mutex==null)
                Console.WriteLine(counter);
            if (mutex.WaitOne())
            {
                try { 
                if (File.Exists(dir + filePath))
                {


                    var root = XElement.Load(dir + filePath);
                    mutex.ReleaseMutex();
                        return root;
                }
                else
                {
                    XElement rootElem = new XElement(getName(type));
                    save(rootElem, type);
                       
                        mutex.ReleaseMutex();
                        return rootElem;
                }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            throw new unexpectedException("loading mutex has failed");
        }
    }
}



