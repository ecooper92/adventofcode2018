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
    public static class Day11
    {
        public static void Process()
        {
            // Calculate part 1.
            var part1 = string.Empty;

            var sn = 3628;
            var area = new int[300, 300, 301];
            var xSums = new int[300, 300, 300];
            var ySums = new int[300, 300, 300];
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

                    xSums[x, y, 1] = digit;
                    ySums[x, y, 1] = digit;
                    area[x, y, 1] = digit;
                }
            }

            int maxSize = 0;
            int maxX = 0;
            int maxY = 0;
            var max = int.MinValue;
            for (int size = 2; size <= 300; size++)
            {
                for (int y = 0; y < area.GetLength(1) - size - 1; y++)
                {
                    for (int x = 0; x < area.GetLength(0) - size - 1; x++)
                    {
                        xSums[x, y + size - 1, size] = xSums[x, y + size - 1, size - 1] + area[x + size - 1, y + size - 1, 1];
                        ySums[x + size - 1, y, size] = ySums[x + size - 1, y, size - 1] + area[x + size - 1, y + size - 1, 1];
                        area[x, y, size] = area[x, y, size - 1] + xSums[x, y + size - 1, size - 1] + ySums[x + size - 1, y, size];

                        if (area[x, y, size] > max)
                        {
                            maxSize = size;
                            maxX = x + 1;
                            maxY = y + 1;
                            max = area[x, y, size];
                        }
                    }
                }
            }

            Console.WriteLine("Day 11");
        }
    }
}
