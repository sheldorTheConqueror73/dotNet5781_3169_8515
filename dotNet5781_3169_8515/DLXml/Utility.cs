using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
namespace DLXml
{

        class Utility
        {
            string dir = @"xml\";
            string busPath = @"Buses.xml";
            string linePath = @"Lines.xml";
            string stationPath = @"Stations.xml";
            string userPath = @"Users.xml";
            string lineInStationPath = @"lineInStations.xml";
            string followStationPath = @"FollowStations.xml";
            private string getPath(Type type)
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
            public XElement ToXml<T>(T data) where T : DOobject
            {

                XElement root = new XElement($"{data.GetType().ToString().Split('.')[1]}");
                root.Add(from prop in data.GetType().GetProperties()
                         select new XElement(prop.Name, prop.GetValue(data, null)));
                return root;
            }
            public void save(XElement root, Type type)
            {
                string path = this.getPath(type);
                try { root.Save(dir + path); }
                catch (Exception e) { throw new cannotFindXmlFileException(path, e, $"fail to save xml file: {path}"); }
            }
            public XElement load(string filePath)
            {
                try
                {
                    if (File.Exists(dir + filePath))
                    {
                        return XElement.Load(dir + filePath);
                    }
                    else
                    {
                        XElement rootElem = new XElement(dir + filePath);
                        rootElem.Save(dir + filePath);
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
