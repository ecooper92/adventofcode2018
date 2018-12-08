using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day6
    {
        public static void Process()
        {
            // Read input lines.
            var idCounter = 1;
            var input = File.ReadAllLines("Day6\\input.txt")
                .Select(s => s.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(s => (id: idCounter++, x: int.Parse(s[0]), y: int.Parse(s[1])))
                .ToArray();

            // Calculate part 1.
            var part1 = 0;
            var area = new (int distance, IList<int> ids)[400, 400];
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    area[x, y] = (int.MaxValue, new List<int>());
                }
            }

            var sizeById = new Dictionary<int, int>();
            foreach (var data in input)
            {
                sizeById[data.id] = 0;
                FillInMap(data.x, data.y, data.id, area);
            }

            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    if (area[x, y].ids.Count == 1)
                    {
                        sizeById[area[x, y].ids[0]]++;
                    }
                }
            }

            // Remove all edges
            for (int i = 0; i < area.GetLength(0); i++)
            {
                if (area[i, 0].ids.Count == 1)
                {
                    sizeById.Remove(area[i, 0].ids[0]);
                }

                if (area[i, area.GetLength(1) - 1].ids.Count == 1)
                {
                    sizeById.Remove(area[i, area.GetLength(1) - 1].ids[0]);
                }

                if (area[0, i].ids.Count == 1)
                {
                    sizeById.Remove(area[0, i].ids[0]);
                }

                if (area[area.GetLength(0) - 1, i].ids.Count == 1)
                {
                    sizeById.Remove(area[area.GetLength(0) - 1, i].ids[0]);
                }
            }
            
            foreach (var kvp in sizeById)
            {
                if (kvp.Value > part1)
                {
                    part1 = kvp.Value;
                }
            }

            // Calculate part 2.
            var part2 = 0;
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    var totalDistance = 0;
                    foreach (var data in input)
                    {
                        totalDistance += Math.Abs(data.x - x) + Math.Abs(data.y - y);
                    }

                    if (totalDistance < 10000)
                    {
                        part2++;
                    }
                }
            }

            Console.WriteLine("Day 6");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);

            DrawNeighborhood(area);
        }

        private static void FillInMap(int xStart, int yStart, int id, (int distance, IList<int> ids)[,] area)
        {
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                {
                    var dist = Math.Abs(x - xStart) + Math.Abs(y - yStart);
                    if (dist < area[x, y].distance)
                    {
                        area[x, y].ids.Clear();
                        area[x, y].ids.Add(id);
                        area[x, y].distance = dist;
                    }
                    else if (dist == area[x, y].distance)
                    {
                        area[x, y].ids.Add(id);
                    }
                }
            }
        }

        private static void DrawNeighborhood((int distance, IList<int> ids)[,] area)
        {
            int size = 1;
            var bitmap = new Bitmap(area.GetLength(0)* size, area.GetLength(1)* size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawLine(Pens.Green, 0, 0, 0, bitmap.Height);
                g.DrawLine(Pens.Green, 0, 0, bitmap.Width, 0);
                g.DrawLine(Pens.Green, 0, bitmap.Height - 1, bitmap.Width, bitmap.Height - 1);
                g.DrawLine(Pens.Green, bitmap.Width - 1, 0, bitmap.Width - 1, bitmap.Height);
                
                var b = new SolidBrush(Color.FromArgb(75, 0, 0, 255));

                for (int y = 0; y < area.GetLength(1); y++)
                {
                    for (int x = 0; x < area.GetLength(0); x++)
                    {
                        if (area[x, y].ids.Count > 3)
                        {
                            g.FillRectangle(Brushes.Yellow, x, y, 1, 1);
                        }
                        else if (area[x, y].ids.Count == 2)
                        {
                            g.FillRectangle(Brushes.Red, x, y, 1, 1);
                        }
                        else if (area[x, y].ids[0] != area[Math.Max(x - 1, 0), y].ids[0]
                            || area[x, y].ids[0] != area[x, Math.Max(y - 1, 0)].ids[0]
                            || area[x, y].ids[0] != area[Math.Min(x + 1, area.GetLength(0) - 1), y].ids[0]
                            || area[x, y].ids[0] != area[x, Math.Min(y + 1, area.GetLength(1) - 1)].ids[0])
                        {
                            g.FillRectangle(Brushes.Purple, x, y, 1, 1);
                        }
                        else if (area[x, y].distance == 0)
                        {
                            g.FillRectangle(Brushes.Blue, x - 1, y - 1, 3, 3);
                        }
                        else if (area[x, y].ids.Count == 1)
                        {
                            g.FillRectangle(b, x, y, 1, 1);
                        }

                        //g.DrawString(area[x, y].id.ToString(), new Font("Segoe UI", 0.5f), Brushes.Red, x * size, y * size, );
                    }
                }
            }

            using (Graphics g = Graphics.FromHwnd(DrawUtilities.GetConsoleWindow()))
            {
                g.DrawImage(bitmap, new Rectangle(350, 12, 975, 975));
            }
        }
    }
}
