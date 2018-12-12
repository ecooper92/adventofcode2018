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
    public static class Day9
    {
        public static void Process()
        {
            // Read input lines.
            var line = "404 players; last marble is worth 71852 points";
            var test = "10 players; last marble is worth 1618 points";

            var match = Regex.Match(line, "([0-9]+) players; last marble is worth ([0-9]+) points", RegexOptions.Compiled);
            var players = int.Parse(match.Groups[1].Value);
            var finalPoints = int.Parse(match.Groups[2].Value) * 100;

            // Calculate part 1.
            var part1 = 0;

            var counter = 0;
            var scores = new long[players];
            var circle = new LinkedList<int>();
            var current = circle.AddFirst(0);
            while ((counter + 1) <= finalPoints)
            {
                if ((counter + 1) % 23 == 0)
                {
                    var previous = current.Previous ?? circle.Last;
                    previous = previous.Previous ?? circle.Last;
                    previous = previous.Previous ?? circle.Last;
                    previous = previous.Previous ?? circle.Last;
                    previous = previous.Previous ?? circle.Last;

                    var nextprevious = previous.Previous ?? circle.Last;
                    previous = nextprevious.Previous ?? circle.Last;

                    scores[counter % players] += previous.Value + counter + 1;
                    circle.Remove(previous);
                    current = nextprevious;
                }
                else
                {
                    current = circle.AddAfter(current.Next ?? circle.First, counter + 1);
                }

                counter++;
            }

            var maxScore = long.MinValue;
            var maxPlayer = int.MinValue;
            for (int i = 0; i < players; i++)
            {
                if (scores[i] > maxScore)
                {
                    maxScore = scores[i];
                    maxPlayer = i + 1;
                }
            }

            // Calculate part 2.
            var part2 = 0;

            Console.WriteLine("Day 9");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
