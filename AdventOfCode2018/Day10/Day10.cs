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
    public static class Day10
    {
        public static void Process()
        {
            // Read input lines.
            var lines = File.ReadAllLines(@"Day10\input.txt")
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

            // Calculate part 1.
            var part1 = 0;

            int xOffset = 350;
            int yOffset = 12;

            float factor = 50;
            float speed = 1f;
            int size = 6;


            for (int i = 0; i < 100000; i++)
            {
                var bitmap = new Bitmap((int)(maxX / factor) + 1, (int)(maxY / factor) + 1);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (lines[j].X >= 0 && lines[j].X < maxX && lines[j].Y >= 0 && lines[j].Y < maxY)
                        {
                            g.FillRectangle(Brushes.Red, lines[j].X / factor - (size / 2.0f), lines[j].Y / factor - (size / 2.0f), size, size);
                        }

                        lines[j].X += (int)(lines[j].VX * speed);
                        lines[j].Y += (int)(lines[j].VY * speed);
                    }
                }
                
                using (Graphics g = Graphics.FromHwnd(DrawUtilities.GetConsoleWindow()))
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    g.FillRectangle(Brushes.DarkBlue, xOffset, yOffset, bitmap.Width, bitmap.Height);
                    g.DrawImage(bitmap, xOffset, yOffset, bitmap.Width, bitmap.Height);
                }

                Thread.Sleep(1);

                bitmap.Dispose();
            }


            // Calculate part 2.
            var part2 = 0;

            Console.WriteLine("Day 9");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
