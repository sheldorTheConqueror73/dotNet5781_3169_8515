using System;
using System.Reflection;
using System.Threading;
//using DO;

namespace DL
{
    static class Utilty
    {
        private static Mutex BusMutex = null;
        private static Mutex LineMutex = null;
        private static Mutex StationMutex = null;
        private static Mutex UserMutex = null;
        private static Mutex LISMutex = null;
        private static Mutex FollowMutex = null;
        private static Mutex LineHistoryMutex = null;
        private static Mutex BusHistoryMutex = null;
        public static Mutex getMutex(Type type)
        {
            if (type == typeof(DO.Bus))
            {
                if (BusMutex == null)
                    BusMutex = new Mutex(false, "BusesListMutex");
                return BusMutex;
            }
            if (type == typeof(DO.BusLine))
            {
                if (LineMutex == null)
                    LineMutex = new Mutex(false, "LinesListMutex");
                return LineMutex;
            }

            if (type == typeof(DO.BusLineStation))
            {
                if (StationMutex == null)
                    StationMutex = new Mutex(false, "StationssListMutex");
                return StationMutex;
            }
            if (type == typeof(DO.User))
            {
                if (UserMutex == null)
                    UserMutex = new Mutex(false, "UsersListMutex");
                return UserMutex;
            }
            if (type == typeof(DO.LineInStation))
            {
                if (LISMutex == null)
                    LISMutex = new Mutex(false, "LISListMutex");
                return LISMutex;
            }
            if (type == typeof(DO.FollowStations))
            {
                if (FollowMutex == null)
                    FollowMutex = new Mutex(false, "FollowsListMutex");
                return FollowMutex;
            }
            if (type == typeof(DO.BusHistory))
            {
                if (BusHistoryMutex == null)
                    BusHistoryMutex = new Mutex(false, "BusHListMutex");
                return BusHistoryMutex;
            }
            if (type == typeof(DO.LineHistory))
            {
                if (LineHistoryMutex == null)
                    LineHistoryMutex = new Mutex(false, "LineHListMutex");
                return LineHistoryMutex;
            }
            throw new DO.InvalidArgumentException("Invalid Argument");
        }

        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);

            return copyToObject;
        }

        


    }
}
