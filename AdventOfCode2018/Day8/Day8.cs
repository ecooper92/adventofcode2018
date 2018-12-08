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
    public static class Day8
    {
        public static void Process()
        {
            // Read input lines.
            //var line = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2".Split(' ').Select(l => int.Parse(l.ToString())).ToArray();
            var line = File.ReadAllLines("Day8\\input.txt")[0].Split(' ').Select(l => int.Parse(l.ToString())).ToArray();

            // Calculate part 1.
            var part1 = SumMetadata(line, 0).sum;

            // Calculate part 2.
            var node = BuildGraph(line, 0).node;
            var part2 = node.Value;

            Console.WriteLine("Day 8");
            Console.WriteLine("     - Part 1: " + part1);
            Console.WriteLine("     - Part 2: " + part2);
        }

        private static (int sum, int end) SumMetadata(int[] input, int start)
        {
            var sum = 0;

            var children = input[start];
            var metaEntries = input[start + 1];

            start += 2;
            for (int i = 0; i < children; i++)
            {
                var result = SumMetadata(input, start);
                sum += result.sum;
                start = result.end;
            }

            for (int i = 0; i < metaEntries; i++)
            {
                sum += input[start + i];
            }

            return (sum, metaEntries + start);
        }

        private static (Node node, int end) BuildGraph(int[] input, int start)
        {
            var node = new Node();

            var children = input[start];
            var metaEntries = input[start + 1];

            start += 2;
            for (int i = 0; i < children; i++)
            {
                var result = BuildGraph(input, start);
                node.Children.Add(result.node);

                start = result.end;
            }

            for (int i = 0; i < metaEntries; i++)
            {
                node.Entries.Add(input[start + i]);
            }

            return (node, metaEntries + start);
        }

        private class Node
        {
            public List<int> Entries { get; } = new List<int>();

            public List<Node> Children { get; } = new List<Node>();

            public int Value
            {
                get
                {
                    var sum = 0;

                    if (Children.Count == 0)
                    {
                        sum += Entries.Sum();
                    }
                    else
                    {
                        foreach (var entry in Entries)
                        {
                            if (entry > 0 && entry <= Children.Count)
                            {
                                sum += Children[entry - 1].Value;
                            }
                        }
                    }

                    return sum;
                }
            }
        }
    }
}
