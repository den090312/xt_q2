using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST1
{
    class Program
    {
        static void Main(string[] args)
        {

            const string ShowCurrentYear = "-curyear";

            if (args.Contains(ShowCurrentYear))
            {
                Console.WriteLine(DateTime.Now.Year);
            }

            Console.ReadKey();
        }
    }
}
