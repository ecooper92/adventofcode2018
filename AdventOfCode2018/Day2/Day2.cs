using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day2
    {
        public static void Process()
        {
            // Read input lines.
            var input = File.ReadAllLines("Day2\\input.txt");

            // Calculate part 1.
            var part1Sum1 = 0;
            var part1Sum2 = 0;
            foreach (var line in input)
            {
                var counters = new int[26];
                foreach (var c in line)
                {
                    counters[c - 97]++;
                }

                part1Sum1 = part1Sum1 + (counters.Any(c => c == 2) ? 1 : 0);
                part1Sum2 = part1Sum2 + (counters.Any(c => c == 3) ? 1 : 0);
            }

            var part1 = part1Sum1 * part1Sum2;

            // Calculate part 2.
            var part2 = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    var diffs = 0;
                    var mismatch = -1;
                    for (int k = 0; k < input[i].Length; k++)
                    {
                        if (input[i][k] != input[j][k])
                        {
                            diffs++;
                            mismatch = k;
                            if (diffs > 1)
                            {
                                break;
                            }
                        }
                    }

                    if (diffs == 1)
                    {
                        part2 = input[j].Remove(mismatch, 1);
                        i = int.MaxValue - 1;
                        break;
                    }
                }
            }

            Console.WriteLine("Day 1");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
