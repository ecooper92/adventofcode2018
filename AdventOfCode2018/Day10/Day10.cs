using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day10
    {
        public static void Process()
        {
            // Read input lines.
            var lines = File.ReadAllLines(@"Day10\test.txt")
                .Select(l => Regex.Match(l, "position=<([ |-]*[0-9]+), ([ |-]*[0-9]+)> velocity=<([ |-]*[0-9]+), ([ |-]*[0-9]+)>", RegexOptions.Compiled))
                .Select(m => (X: int.Parse(m.Groups[1].Value), Y: int.Parse(m.Groups[2].Value), VX: int.Parse(m.Groups[3].Value), VY: int.Parse(m.Groups[4].Value)))
                .ToArray();

            var minX = lines.Min(l => l.X);
            var minY = lines.Min(l => l.Y);
            var maxX = lines.Max(l => l.X);
            var maxY = lines.Max(l => l.Y);
            if (minX < 0)
            {
                maxX -= minX;
            }
            if (minY < 0)
            {
                maxY -= minY;
            }

            var area = new (int X, int Y, int VX, int VY)[maxX + 1, maxY + 1];
            foreach (var line in lines)
            {
                area[line.X - minX, line.Y - minY] = line;
            }

            // Calculate part 1.
            var part1 = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int y = 0; y < area.GetLength(1); y++)
                {
                    for (int x = 0; x < area.GetLength(0); x++)
                    {
                        if (area[x, y].X == 0
                            && area[x, y].Y == 0
                            && area[x, y].VX == 0
                            && area[x, y].VX == 0)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("X");
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                Thread.Sleep(3000);
            }


            // Calculate part 2.
            var part2 = 0;

            Console.WriteLine("Day 9");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
