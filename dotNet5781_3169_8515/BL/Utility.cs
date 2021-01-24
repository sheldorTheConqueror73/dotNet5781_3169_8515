using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Utility
    {
        /// <summary>
        /// convert a DO object to BO object
        /// </summary>
        /// <typeparam name="T">BO object</typeparam>
        /// <typeparam name="S">DO object</typeparam>
        /// <param name="line">the converting object</param>
        /// <returns> return the object as BO object</returns>
        internal static T DOtoBOConvertor<T, S>(S line) where T : BO.BOobject, new() where S : DO.DOobject, new()
        {
            if (line == null)
                return null;
            T output = new T();
            output.id = line.id;
            foreach (PropertyInfo propTo in output.GetType().GetProperties())
            {
                PropertyInfo propFrom = line.GetType().GetProperty(propTo.Name);
                if (propFrom == null||propTo.PropertyType != propFrom.PropertyType)
                    continue;
                propTo.SetValue(output, propFrom.GetValue(line, null));
            }
            return output;

        }
        /// <summary>
        /// convert a BO object to DO object
        /// </summary>
        /// <typeparam name="T">DO object</typeparam>
        /// <typeparam name="S">BO object</typeparam>
        /// <param name="line">the converting object</param>
        /// <returns> return the object as DO object</returns>
        internal static T BOtoDOConvertor<T, S>(S line) where T : DO.DOobject, new() where S : BO.BOobject, new()
        {
            if (line == null)
                return null;
            T output = new T();
            output.id = line.id;
            foreach (PropertyInfo propTo in output.GetType().GetProperties())
            {
                PropertyInfo propFrom = line.GetType().GetProperty(propTo.Name);
                if (propFrom == null||propTo.PropertyType!=propFrom.PropertyType)
                    continue;
                propTo.SetValue(output, propFrom.GetValue(line, null));
            }
            return output;

        }
        /// <summary>
        /// get bus and change its number to format of $$-$$$-$$ or  $$$-$$-$$$
        /// </summary>
        /// <param name="bus">bus</param>
        internal static void formatPlateNumber(this BO.Bus bus)
        {
            bus.plateNumber=removeChar(bus.plateNumber,'-');
            if (bus.plateNumber.Length == 7)
                bus.plateNumber = $"{bus.plateNumber[0]}{bus.plateNumber[1]}-{bus.plateNumber[2]}{bus.plateNumber[3]}{bus.plateNumber[4]}-{bus.plateNumber[5]}{bus.plateNumber[6]}";
            else
                bus.plateNumber = $"{bus.plateNumber[0]}{bus.plateNumber[1]}{bus.plateNumber[2]}-{bus.plateNumber[3]}{bus.plateNumber[4]}-{bus.plateNumber[5]}{bus.plateNumber[6]}{bus.plateNumber[7]}";

        }
        internal static string removeChar( string str,char ch)
        {
            string temp = "";
            foreach (char element in str)
                if (element != ch)
                    temp += element;
            return temp;
        }
        public static bool passedYearNowAndThen(this BO.Bus bus)//return true if a year has passed since the last maintenance.
        {
            DateTime currentDate = DateTime.Now;
            if ((currentDate.Year - bus.lastMaintenance.Year) < 1)
                return false;
            if ((currentDate.Month - bus.lastMaintenance.Month) < 0)
                return false;
            if ((currentDate.Day - bus.lastMaintenance.Day) < 0)
                return false;
            return true;
        }
        public static void UpdateDangerous(this BO.Bus bus)//updates dangerous status of selected bus
        {
            if ((bus.distance >= 20000) || (bus.passedYearNowAndThen() == true))
            {
                bus.dangerous = true;
                bus.status = "dangerous";
                bus.iconPath = "Resources/warningIcon.png";
                return;
            }
            bus.dangerous = false;
        }
    }
}
