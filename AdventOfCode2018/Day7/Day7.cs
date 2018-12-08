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
    public static class Day7
    {
        public static void Process()
        {
            // Read input lines.
            var input = File.ReadAllLines("Day7\\input.txt")
                .Select(l =>
                {
                    var match = Regex.Match(l, "Step ([A-Z]) must.+step ([A-Z]) can", RegexOptions.Compiled);
                    return (Source: match.Groups[1].Value, Destination: match.Groups[2].Value);
                })
                .ToArray();

            // Calculate part 1.
            var part1 = string.Empty;
            var forwardMap = new Dictionary<string, ISet<string>>();
            foreach (var data in input)
            {
                if (!forwardMap.ContainsKey(data.Source))
                {
                    forwardMap[data.Source] = new HashSet<string>();
                }

                forwardMap[data.Source].Add(data.Destination);
            }

            var start = new KeyValuePair<string, ISet<string>>();
            var allValues = forwardMap.SelectMany(kvp => kvp.Value).ToHashSet();
            foreach (var kvp in forwardMap)
            {
                if (!allValues.Contains(kvp.Key))
                {
                    start = kvp;
                    break;
                }
            }

            var completedNodes = new HashSet<string>();
            part1 += start.Key;
            completedNodes.Add(start.Key);

            var nextNodes = new SortedSet<string>();
            foreach (var item in start.Value)
            {
                if (!completedNodes.Contains(item))
                {
                    nextNodes.Add(item);
                    completedNodes.Add(item);
                }
            }
                       
            while (nextNodes.Count > 0)
            {
                var item = nextNodes.First();
                nextNodes.Remove(item);
                part1 += item;

                if (forwardMap.ContainsKey(item))
                {
                    foreach (var child in forwardMap[item])
                    {
                        if (!completedNodes.Contains(child))
                        {
                            nextNodes.Add(child);
                            completedNodes.Add(item);
                        }
                    }
                }
            }

            // Calculate part 2.
            var part2 = 0;

            Console.WriteLine("Day 6");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }
    }
}
