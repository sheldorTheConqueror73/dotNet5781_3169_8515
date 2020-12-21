using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal getDal()
        {

            string dlType = DLConfig.DLName;
            DLConfig.DLPackage dlPackage;
            try 
            {
                dlPackage = DLConfig.DLPackages[dlType];
            }
            catch (KeyNotFoundException ex)
            {
                // if package name is not found in the list - there is a problem in config.xml
                throw new DLConfigException($"Wrong DL type: {dlType}", ex);
            }
            string dlPackageName = dlPackage.PkgName;
            string dlNameSpace = dlPackage.NameSpace;
            string dlClass = dlPackage.ClassName;
            try
            {
                Assembly.Load(dlPackageName);
            }
            catch (Exception ex)
            {
                throw new DLConfigException($"Failed loading {dlPackageName}.dll", ex);
            }
            Type type;
            try
            {
                type = Type.GetType($"{dlNameSpace}.{dlClass}, {dlPackageName}", true);
            }
            catch (Exception ex)
            {
                throw new DLConfigException($"Class not found due to a wrong namespace or/and class name: {dlPackageName}:{dlNameSpace}.{dlClass}", ex);
            }
            try
            {
                IDal dal = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDal;      
                if (dal == null)
                    throw new DLConfigException($"Class {dlNameSpace}.{dlClass} instance is not initialized");
                return dal;
            }
            catch (NullReferenceException ex)
            {
                throw new DLConfigException($"Class {dlNameSpace}.{dlClass} is not a singleton", ex);
            }
        }

    }
}
