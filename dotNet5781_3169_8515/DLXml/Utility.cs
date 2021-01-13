using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
namespace DL
{

    internal static class Utility
    {
        private static string dir = @"xml\";
        private static string busPath = @"Buses.xml";
        private static string linePath = @"Lines.xml";
        private static string stationPath = @"Stations.xml";
        private static string userPath = @"Users.xml";
        private static string lineInStationPath = @"LineInStations.xml";
        private static string followStationPath = @"FollowStations.xml";
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
        public static XElement ToXml<T>(this T data) where T : DOobject
        {
            XElement root = new XElement($"{data.GetType().ToString().Split('.')[1]}es");
            root.Add(from prop in data.GetType().GetProperties()
                     select new XElement(prop.Name, prop.GetValue(data, null)));
            return root;
        }

        public static T ToObject<T>(this XElement root) where T : DOobject, new()
        {
            T obj = new T();
            foreach (var element in root.Elements())
            {
                foreach (var prop in typeof(T).GetProperties())
                    if (prop.Name == element.Name)
                    {
                        prop.SetValue(element, element.Value);
                    }
            }
            return obj;
        }

        
        public static void save(XElement root, Type type)
        {
            string path = getPath(type);
            try 
            {
                XDocument document = new XDocument(root);
                document.Save(dir + path);
            }
            catch (Exception e) { throw new cannotFindXmlFileException(path, e, $"fail to save xml file: {path}"); }
        }
        public static void save(XDocument document, Type type)
        {
            string path = getPath(type);
            try 
            {
                document.Save(dir + path);
            }
            catch (Exception e) { throw new cannotFindXmlFileException(path, e, $"fail to save xml file: {path}"); }
        }
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
                        XElement rootElem = new XElement(type.Name);
                        save(rootElem,type);
                        return rootElem;
                    }
                }
                catch (Exception ex)
                {
                    throw new cannotFindXmlFileException(filePath, ex, $"fail to load xml file: {filePath}");

                }
            }
        }
    }
