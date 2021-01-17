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
                        if(prop.PropertyType==typeof(bool))
                            prop.SetValue(obj,bool.Parse(element.Value));
                        else if(prop.PropertyType == typeof(int))
                            prop.SetValue(obj,int.Parse(element.Value));
                        else if (prop.PropertyType == typeof(double))
                            prop.SetValue(obj, double.Parse(element.Value));
                        else if (prop.PropertyType == typeof(TimeSpan))
                            prop.SetValue(obj, XmlConvert.ToTimeSpan(element.Value));
                        else if (prop.PropertyType == typeof(DateTime))
                            prop.SetValue(obj, DateTime.Parse(element.Value));
                        else if (prop.PropertyType == typeof(DO.Area))
                        {
                            DO.Area a; ;
                            Enum.TryParse<DO.Area>(element.Value,out a);
                            prop.SetValue(obj,a);
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
            string path = getPath(type);
            try 
            {
                XDocument document = new XDocument(root);
                document.Save(dir + path);
            }
            catch (Exception e) 
            {
                Thread.Sleep(3);
                save(root, type);
            }
           
        }
        /// <summary>
        /// loads data from file
        /// </summary>
        /// <param name="type">type of class file</param>
        /// <returns>root XElement of file</returns>
        public static XElement load(Type type)
        { 
            string filePath = getPath(type);
                try
                {
                    if (File.Exists(dir + filePath))
                    {
                        return XElement.Load(dir + filePath);
                    }
                    else
                    {
                        XElement rootElem = new XElement(getName(type));
                        save(rootElem,type);
                        return rootElem;
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(3);
                    return load(type);

                }
            }
        }
    }
