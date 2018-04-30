using System;
using SimpleWMI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWMIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processes currently running with 'google' in the caption:");

            IEnumerable<dynamic> googleProcesses = WMIQuery.GetAllObjects(Win32.Process).Where(s => s.Caption.ToLower().Contains("google"));

            foreach (dynamic item in googleProcesses)
            {
                Console.WriteLine(item.Caption);
            }

            Console.WriteLine();

            Console.WriteLine("Names of detected GPUs in this computer:");
            foreach (dynamic item in WMIQuery.GetAllObjects(Win32.VideoController))
            {
                //Wrapped in a try-catch because trying to write null values to console causes errors.
                try
                {
                    Console.WriteLine(item.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Information about connected monitors:");

            foreach (dynamic item in WMIQuery.GetAllObjects(Win32.DesktopMonitor))
            {
                Console.WriteLine("Resolution: {0}x{1}", item.ScreenWidth, item.ScreenHeight);
                Console.WriteLine("Manufacturer: {0}", item.MonitorManufacturer);
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Device ID: {0}", item.DeviceID);
                Console.WriteLine();
            }

            Console.WriteLine("Information about connected printers:");

            foreach (dynamic item in WMIQuery.GetAllObjects(Win32.Printer))
            {
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Device ID: {0}", item.DeviceID);
                Console.WriteLine();
            }

            Console.WriteLine("Information about connected mice:");

            foreach (dynamic item in WMIQuery.GetAllObjects(Win32.PointingDevice))
            {
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Manufacturer: {0}", item.Manufacturer);
                Console.WriteLine("Device ID: {0}", item.DeviceID);
                Console.WriteLine();
            }

            Console.WriteLine("Information about connected keyboards:");

            foreach (dynamic item in WMIQuery.GetAllObjects(Win32.Keyboard))
            {
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Device ID: {0}", item.DeviceID);
                Console.WriteLine();
            }

            Console.Read();
        }
    }
}
