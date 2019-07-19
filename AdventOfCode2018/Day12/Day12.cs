using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day12
    {
        public static void Process()
        {
            var lines = File.ReadAllLines(@"Day12\input.txt");
            var initialState = lines[0].Substring(15);

            var rules = new string[lines.Length - 2];
            Array.Copy(lines, 2, rules, 0, lines.Length - 2);

            // Calculate part 1.
            var index = new Dictionary<Tuple<bool, bool, bool, bool, bool>, bool>();
            foreach (var rule in rules)
            {
                var key = Tuple.Create(rule[0] == '#', rule[1] == '#', rule[2] == '#', rule[3] == '#', rule[4] == '#');
                var value = rule[9] == '#';
                index[key] = value;
            }

            var bufferLeft = 8;
            var bufferRight = 128;
            var area = new bool[initialState.Length + bufferLeft + bufferRight];
            var nextArea = new bool[initialState.Length + bufferLeft + bufferRight];
            for (int i = 0; i < initialState.Length; i++)
            {
                area[i + bufferLeft] = initialState[i] == '#';
            }

            var rowCount = 0;
            PrintArea(area);
            for (long counter = 0; counter < 104; counter++)
            {
                rowCount = 0;
                for (int i = 2; i < area.Length - 4; i++)
                {
                    var key = Tuple.Create(area[i], area[i + 1], area[i + 2], area[i + 3], area[i + 4]);
                    if (index.ContainsKey(key))
                    {
                        var value = index[key];
                        nextArea[i + 2] = value;
                        if (value)
                        {
                            rowCount += i - bufferLeft + 2;
                        }
                    }
                    else
                    {

                    }
                }

                PrintArea(nextArea);
                var temp = area;
                area = nextArea;
                nextArea = temp;
                Array.Clear(nextArea, 0, nextArea.Length);
            }

            Console.WriteLine("Day 12");
            Console.WriteLine(rowCount);
        }

        private static void PrintArea(bool[] area)
        {
            foreach (var entry in area)
            {
                if (entry)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }

            Console.WriteLine();
        }
    }
}
