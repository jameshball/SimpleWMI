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
            IEnumerable<dynamic> googleProcesses = WMIQuery.GetAllObjects(Win32.Process).Where(s => s.Caption.ToLower().Contains("google"));

            foreach (dynamic item in googleProcesses)
            {
                Console.WriteLine(item.Caption);
            }

            dynamic cpu = WMIQuery.GetObject("Win32_Processor");
            Console.WriteLine(cpu.Name);

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

            Console.Read();
        }
    }
}
