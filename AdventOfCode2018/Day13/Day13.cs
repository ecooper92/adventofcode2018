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
    public static class Day13
    {
        public static void Process()
        {
            var lines = File.ReadAllLines(@"Day13\input.txt");
            var track = new char[lines.Length][];
            var vehicles = new List<Vehicle>();

            for (int i = 0; i < lines.Length; i++)
            {
                track[i] = lines[i].ToCharArray();
            }

            var counter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < track[i].Length; j++)
                {
                    if (track[i][j] == '<'
                         || track[i][j] == '>'
                         || track[i][j] == '^'
                         || track[i][j] == 'v')
                    {
                        var hasNorth = i > 0 && track[i - 1][j] == '|';
                        var hasSouth = i < track.Length - 1 && track[i + 1][j] == '|';
                        var hasWest = j > 0 && track[i][j - 1] == '-';
                        var hasEast = j < track[i].Length - 1 && track[i][j + 1] == '-';

                        if (track[i][j] == '<')
                        {
                            vehicles.Add(new Vehicle(counter++, j, i, 180, 0));
                        }
                        else if (track[i][j] == '>')
                        {
                            vehicles.Add(new Vehicle(counter++, j, i, 0, 0));
                        }
                        else if (track[i][j] == '^')
                        {
                            vehicles.Add(new Vehicle(counter++, j, i, 90, 0));
                        }
                        else if (track[i][j] == 'v')
                        {
                            vehicles.Add(new Vehicle(counter++, j, i, 270, 0));
                        }

                        if (hasNorth && hasSouth && hasWest && hasEast)
                        {
                            track[i][j] = '+';
                        }
                        else if (hasNorth && hasSouth)
                        {
                            track[i][j] = '|';
                        }
                        else if (hasWest && hasEast)
                        {
                            track[i][j] = '-';
                        }
                        else if ((hasWest && hasNorth) || (hasEast && hasSouth))
                        {
                            track[i][j] = '/';
                        }
                        else if ((hasWest && hasSouth) || (hasEast && hasNorth))
                        {
                            track[i][j] = '\\';
                        }
                    }
                }
            }

            // Calculate part 1.
            var isDone = false;
            var iteration = 0;
            while (!isDone)
            {
                iteration++;
                foreach (var vehicle in vehicles.OrderBy(v => v.Y).ThenBy(v => v.X))
                {
                    vehicle.X +=  vehicle.Dir.Item1;
                    vehicle.Y += vehicle.Dir.Item2;

                    if (track[vehicle.Y][vehicle.X] == '/')
                    {
                        vehicle.Dir = Tuple.Create(vehicle.Dir.Item2 * -1, vehicle.Dir.Item1 * -1);
                    }
                    else if (track[vehicle.Y][vehicle.X] == '\\')
                    {
                        vehicle.Dir = Tuple.Create(vehicle.Dir.Item2, vehicle.Dir.Item1);
                    }
                    else if (track[vehicle.Y][vehicle.X] == '+')
                    {
                        if (vehicle.Sequence == 0)
                        {
                            vehicle.Angle = (vehicle.Angle + 90) % 360;
                        }
                        else if (vehicle.Sequence == 2)
                        {
                            vehicle.Angle = (vehicle.Angle - 90);
                            if (vehicle.Angle < 0)
                            {
                                vehicle.Angle = 360 + vehicle.Angle;
                            }
                        }

                        vehicle.Sequence = (vehicle.Sequence + 1) % 3;
                    }
                    else if (vehicles.Any(v => v.Id != vehicle.Id && v.X == vehicle.X && v.Y == vehicle.Y))
                    {
                        isDone = true;
                    }
                }
            }

            Console.WriteLine("Day 13");
        }

        private class Vehicle
        {
            public Vehicle(int id, int x, int y, int angle, int Seq)
            {
                Id = id;
                X = x;
                Y = y;
                Angle = angle;
                Sequence = Seq;
            }

            public int Id { get; }

            public int X { get; set; }

            public int Y { get; set; }

            public int Angle { get; set; }

            public Tuple<int, int> Dir
            {
                get => Tuple.Create(Angle == 0 ? 1 : (Angle == 180 ? -1 : 0), Angle == 270 ? 1 : (Angle == 90 ? -1 : 0));
                set
                {
                    if (value.Item1 == 1 && value.Item2 == 0)
                    {
                        Angle = 0;
                    }
                    else if (value.Item1 == 0 && value.Item2 == -1)
                    {
                        Angle = 90;
                    }
                    else if (value.Item1 == -1 && value.Item2 == 0)
                    {
                        Angle = 180;
                    }
                    else if (value.Item1 == 0 && value.Item2 == 1)
                    {
                        Angle = 270;
                    }
                }
            }

            public int Sequence { get; set; }
        }
    }
}
