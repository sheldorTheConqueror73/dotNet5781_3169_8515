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
        internal static T DOtoBOConvertor<T, S>(S line) where T : BO.BOobject, new() where S : DO.DOobject, new()
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

        internal static T BOtoDOConvertor<T, S>(S line) where T : DO.DOobject, new() where S : BO.BOobject, new()
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
        internal static void formatPlateNumber(this BO.Bus bus)
        {
            bus.plateNumber.removeChar('-');
            if (bus.plateNumber.Length == 7)
                bus.plateNumber = $"{bus.plateNumber[0]}{bus.plateNumber[1]}-{bus.plateNumber[2]}{bus.plateNumber[3]}{bus.plateNumber[4]}-{bus.plateNumber[5]}{bus.plateNumber[6]}";
            else
                bus.plateNumber = $"{bus.plateNumber[0]}{bus.plateNumber[1]}{bus.plateNumber[2]}-{bus.plateNumber[3]}{bus.plateNumber[4]}-{bus.plateNumber[5]}{bus.plateNumber[6]}{bus.plateNumber[7]}";

        }
        internal static void removeChar(this string str,char ch)
        {
            string temp = "";
            foreach (char element in str)
                if (element != ch)
                    temp += element;
            str = temp;
        }
        internal static string convertTime(this DO.busLine line)
        {
            return line.driveTime.ToString().Split(' ')[1];
        }
    }
}
