using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
    public static class Day11
    {
        public static void Process()
        {
            // Calculate part 1.
            var part1 = string.Empty;

            var sn = 5093;
            var area = new int[300, 300];
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    var rackId = (x + 1) + 10;
                    var powerLevel = rackId * (y + 1);
                    powerLevel += sn;
                    powerLevel *= rackId;

                    var digit = (int)(powerLevel / Math.Pow(10, 2)) % 10;
                    digit -= 5;

                    area[x, y] = digit;
                }
            }

            int maxSize = 0;
            int maxX = 0;
            int maxY = 0;
            var max = int.MinValue;
            for (int size = 1; size < 300; size++)
            {
                for (int y = 0; y < area.GetLength(1) - size - 1; y++)
                {
                    for (int x = 0; x < area.GetLength(0) - size - 1; x++)
                    {
                        var sum = 0;
                        for (int ySize = 0; ySize < size; ySize++)
                        {
                            for (int xSize = 0; xSize < size; xSize++)
                            {
                                sum += area[x + xSize, y + ySize];
                            }
                        }

                        if (sum > max)
                        {
                            maxSize = size;
                            maxX = x + 1;
                            maxY = y + 1;
                            max = sum;
                        }
                    }
                }
            }

            // Calculate part 2.
            var part2 = 0;

            Console.WriteLine("Day 11");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
