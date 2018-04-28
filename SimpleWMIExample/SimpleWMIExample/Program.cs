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
            List<dynamic> processor =  WMIQuery.GetAllObjects("Processor");
            Console.Read();
        }
    }
}
