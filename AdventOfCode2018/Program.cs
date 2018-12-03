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
            Day1.Process();
            Day2.Process();
            Day3.Process();

            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }
}
