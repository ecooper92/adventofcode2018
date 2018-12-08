using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(240, 63);

            Day1.Process();
            Day2.Process();
            Day3.Process();
            Day4.Process();
            Day5.Process();
            Day6.Process();
            Day7.Process();
            Day8.Process();

            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }
}
