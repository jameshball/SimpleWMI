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

            //IEnumerable<object> results = processes.Where(s => s.Caption.ToLower().Contains("google"));

            //foreach (dynamic item in results)
            //{
            //    Console.WriteLine(item.Caption);
            //}

            Console.Read();
        }
    }
}
