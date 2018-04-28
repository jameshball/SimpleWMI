using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWMI
{
    class WMIQuery
    {
        public static List<dynamic> GetAllObjects(string className)
        {
            ManagementObjectCollection col = new ManagementObjectSearcher("SELECT * FROM Win32_" + className).Get();
            List<dynamic> objects = new List<dynamic>();

            foreach (ManagementObject obj in col)
            {
                objects.Add(obj);
            }

            return objects;
        }
    }
}
