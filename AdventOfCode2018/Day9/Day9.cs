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
            var test = "9 players; last marble is worth 32 points";

            var match = Regex.Match(test, "([0-9]+) players; last marble is worth ([0-9]+) points", RegexOptions.Compiled);
            var players = int.Parse(match.Groups[1].Value);
            var finalPoints = int.Parse(match.Groups[2].Value);

            // Calculate part 1.
            var part1 = 0;
            var tree = new LinkedList<int>();
            tree.AddFirst(0);
            var currentNode = tree.AddLast(1);
            var scores = new int[players];

            var counter = 2;
            var isLeft = true;
            while (true)
            {
                if (counter % 23 == 0)
                {
                    //scores[counter % players] += counter + 
                    isLeft = true;
                }
                else if (isLeft)
                {
                    currentNode = tree.AddBefore(currentNode, counter++);
                    isLeft = false;
                }
                else
                {
                    currentNode = tree.AddAfter(currentNode, counter++);
                    isLeft = true;
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
