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
                    return (Source: match.Groups[1].Value.ToCharArray()[0], Destination: match.Groups[2].Value.ToCharArray()[0]);
                })
                .ToArray();

            // Calculate part 1.
            var part1 = string.Empty;
            var results = BuildGraph(input);
            while (!results.All.All(n => n.IsVisited))
            {
                var next = results.All.FirstOrDefault(n => !n.IsVisited && n.BackwardNodes.All(bn => bn.IsVisited));
                next.IsVisited = true;
                part1 += next.Name;
            }

            // Calculate part 2.
            var part2 = 0;
            int availableWorkers = 5;
            while (!results.All.All(n => n.TimeRemaining == 0))
            {
                var assignableNodes = results.All.Where(n => !n.IsAssigned && n.TimeRemaining > 0 && n.BackwardNodes.All(bn => bn.TimeRemaining == 0));
                foreach (var assignableNode in assignableNodes)
                {
                    if (availableWorkers > 0)
                    {
                        assignableNode.IsAssigned = true;
                        availableWorkers--;
                    }
                }

                var activeNodes = results.All.Where(n => n.IsAssigned && n.TimeRemaining > 0);
                foreach (var activeNode in activeNodes)
                {
                    activeNode.TimeRemaining--;
                    if (activeNode.TimeRemaining == 0)
                    {
                        availableWorkers++;
                    }
                }

                part2++;
            }

            Console.WriteLine("Day 7");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }

        private static (Node Root, IEnumerable<Node> All) BuildGraph((char Source, char Destination)[] input)
        {
            var nodesByName = new Dictionary<char, Node>();
            foreach (var entry in input)
            {
                if (!nodesByName.ContainsKey(entry.Source))
                {
                    nodesByName[entry.Source] = new Node(entry.Source);
                }

                if (!nodesByName.ContainsKey(entry.Destination))
                {
                    nodesByName[entry.Destination] = new Node(entry.Destination);
                }

                nodesByName[entry.Source].ForwardNodes.Add(nodesByName[entry.Destination]);
                nodesByName[entry.Destination].BackwardNodes.Add(nodesByName[entry.Source]);
            }

            return (nodesByName.First(kvp => kvp.Value.BackwardNodes.Count == 0).Value, nodesByName.Select(n => n.Value).OrderBy(n => n.Name).ToArray());
        }

        private class Node
        {
            public Node(char name)
            {
                Name = name.ToString();
                TimeRemaining = 60 + name - 64;
            }

            public string Name { get; } = string.Empty;

            public List<Node> BackwardNodes { get; } = new List<Node>();

            public List<Node> ForwardNodes { get; } = new List<Node>();

            public bool IsVisited { get; set; } = false;

            public bool IsAssigned { get; set; } = false;

            public int TimeRemaining { get; set; }
        }
    }
}
