using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day3
    {
        public static void Process()
        {
            // Read input lines.
            var rectangles = GetRectangles();

            // Initialize surface area.
            var allIds = new HashSet<int>();
            var area = new List<int>[1000, 1000];

            // Build data structures.
            foreach (var rectangle in rectangles)
            {
                allIds.Add(rectangle.Id);
                for (int y = rectangle.YOffset; y < rectangle.YOffset + rectangle.Height; y++)
                {
                    for (int x = rectangle.XOffset; x < rectangle.XOffset + rectangle.Width; x++)
                    {
                        var list = area[x, y] ?? (area[x, y] = new List<int>());
                        list.Add(rectangle.Id);
                    }
                }
            }

            // Calculate part 1 and 2.
            int part1 = 0;
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    if (area[x, y] != null && area[x, y].Count > 1)
                    {
                        for (int z = 0; z < area[x, y].Count; z++)
                        {
                            allIds.Remove(area[x, y][z]);
                        }

                        part1++;
                    }
                }
            }
            
            Console.WriteLine("Day 3");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + allIds.First());
        }

        private static (int Id, int XOffset, int YOffset, int Width, int Height)[] GetRectangles()
        {
            return File.ReadAllLines("Day3\\input.txt")
                .Select(l =>
                {
                    var m = Regex.Match(l, "#([0-9]+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)");
                    return (Id: int.Parse(m.Groups[1].Value),
                        XOffset: int.Parse(m.Groups[2].Value),
                        YOffset: int.Parse(m.Groups[3].Value),
                        Width: int.Parse(m.Groups[4].Value),
                        Height: int.Parse(m.Groups[5].Value));
                })
                .ToArray();
        }

        private static void DrawHeatMap(List<int>[,] area)
        {
            var bitmap = new Bitmap(area.GetLength(0), area.GetLength(1));
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawLine(Pens.Red, 0, 0, 0, area.GetLength(1));
                g.DrawLine(Pens.Red, 0, 0, area.GetLength(0), 0);
                g.DrawLine(Pens.Red, 0, area.GetLength(1) - 1, area.GetLength(0), area.GetLength(1) - 1);
                g.DrawLine(Pens.Red, area.GetLength(0) - 1, 0, area.GetLength(0) - 1, area.GetLength(1));
                var b1 = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
                var b2 = new SolidBrush(Color.FromArgb(200, 0, 255, 0));
                var b3 = new SolidBrush(Color.FromArgb(255, 0, 255, 0));

                for (int y = 0; y < area.GetLength(1); y++)
                {
                    for (int x = 0; x < area.GetLength(0); x++)
                    {
                        if (area[x, y] != null)
                        {
                            if (area[x, y].Count == 1)
                            {
                                g.FillRectangle(b1, x, y, 1, 1);
                            }
                            else if (area[x, y].Count == 2)
                            {
                                g.FillRectangle(b2, x, y, 1, 1);
                            }
                            else if (area[x, y].Count == 3)
                            {
                                g.FillRectangle(b3, x, y, 1, 1);
                            }
                            else
                            {
                                g.DrawRectangle(Pens.Yellow, x, y, 1, 1);
                            }
                        }
                    }
                }
            }

            using (Graphics g = Graphics.FromHwnd(DrawUtilities.GetConsoleWindow()))
            {
                g.DrawImage(bitmap, new Rectangle(350, 12, 450, 450));
            }
        }
    }
}
