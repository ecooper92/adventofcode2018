using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day1
    {
        public static void Process()
        {
            var part2 = int.MinValue;
            var set = new List<int> { 0 };
            var part1 = File.ReadAllLines("Day1\\input.txt")
                .Select(d => int.Parse(d))
                .Aggregate(0, (sum, next) =>
                {
                    var result = sum + next;

                    if (!set.Contains(result))
                    {
                        set.Add(result);
                    }
                    else if (part2 == int.MinValue)
                    {
                        part2 = result;
                    }

                    return result;
                });

            Console.WriteLine("Day 1");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
