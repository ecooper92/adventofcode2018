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
            // Read input integers.
            var input = File.ReadAllLines("Day1\\input.txt").Select(d => int.Parse(d)).ToList();

            // Calculate part 1.
            var part1 = input.Aggregate(0, (sum, next) => sum + next);

            int part2 = 0;
            int index = 0;
            var set = new HashSet<int>();
            while (!set.Contains(part2))
            {
                set.Add(part2);
                part2 += input[index];
                index = (index + 1) % input.Count;
            }

            Console.WriteLine("Day 1");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
