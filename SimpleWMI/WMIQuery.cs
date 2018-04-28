using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace SimpleWMI
{
    public class WMIQuery
    {
        public static List<dynamic> GetAllObjects(string className)
        {
            ManagementObjectCollection col = new ManagementObjectSearcher("SELECT * FROM Win32_" + className).Get();
            List<dynamic> objects = new List<dynamic>();

            foreach (ManagementObject obj in col)
            {
                var currentObject = new ExpandoObject() as IDictionary<string, Object>;

                foreach (PropertyData prop in obj.Properties)
                {
                    currentObject.Add(prop.Name, prop.Value);
                }

                objects.Add(currentObject);
            }

            return objects;
        }

        public static dynamic GetObject(string className)
        {
            ManagementObjectCollection col = new ManagementObjectSearcher("SELECT * FROM Win32_" + className).Get();

            var currentObject = new ExpandoObject() as IDictionary<string, Object>;

            //There will only be one value returned (not a list), but ManagementObjectCollection does not allow indexing.
            foreach (ManagementObject obj in col)
            {
                foreach (PropertyData prop in obj.Properties)
                {
                    currentObject.Add(prop.Name, prop.Value);
                }

                break;
            }

            return currentObject;
        }
    }
}
