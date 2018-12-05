using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day5
    {
        public static void Process()
        {
            var part1 = 0;
            var part2 = 0;

            // Read input lines.
            var line = File.ReadAllLines("Day5\\input.txt")[0];

            // Calculate part 1.
            part1 = Reduce1(line);

            // Calculate part 2.
            var cs = new List<int>();
            part2 = line.ToUpper()
                .Distinct()
                .Select(l => Reduce(line.Replace(l.ToString(), "").Replace(l.ToString().ToLower(), "")))
                .Min();

            Console.WriteLine("Day 5");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }

        private static int Reduce(string line)
        {
            int lastSize = int.MaxValue;
            int currentSize = line.Length;
            var types = new LinkedList<char>(line);
            while (currentSize != lastSize)
            {
                lastSize = currentSize;

                var first = types?.First;
                var second = first?.Next;
                while (second != null)
                {
                    if ((first.Value + 32 == second.Value || first.Value == second.Value + 32))
                    {
                        types.Remove(first);
                        first = second?.Next;
                        types.Remove(second);
                        second = first?.Next;
                        currentSize -= 2;
                    }
                    else
                    {
                        first = second;
                        second = second.Next;
                    }
                }
            }

            return currentSize;
        }

        /// <summary>
        /// Not used, just playing with indexing for speed.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static int Reduce1(string line)
        {
            int lastSize = int.MaxValue;
            int currentSize = line.Length;
            var indices = Enumerable.Range(0, line.Length).Select(i => i + 1).ToArray();
            indices[indices.Length - 1] = -1;
            var types = line.ToArray();
            while (currentSize != lastSize)
            {
                lastSize = currentSize;

                var lastFirst = -1;
                var lastSecond = -1;
                var first = 0;
                var second = indices[0];
                while (first != -1 && second != -1)
                {
                    if (types[first] + 32 == types[second] || types[first] == types[second] + 32)
                    {
                        types[first] = (char)0;
                        types[second] = (char)0;

                        indices[lastFirst] = indices[second];

                        lastFirst = first;
                        lastSecond = second;

                        first = indices[lastSecond];
                        second = indices[first];
                        currentSize -= 2;
                    }
                    else
                    {
                        lastFirst = first;
                        lastSecond = second;

                        first = second;
                        second = indices[second];
                    }
                }
            }

            return currentSize;
        }
    }
}
