using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Day4
    {
        public static void Process()
        {
            // Read input lines.
            var sleepTimeByGuard = new int[3500];
            var sleepMinutesByGuard = new int[3500][];
            foreach (var entry in GetData())
            {
                sleepTimeByGuard[entry.Guard] += (int)entry.SleepingTime.TotalMinutes;
                for (int i = entry.SleepStart; i < entry.SleepStart + (int)entry.SleepingTime.TotalMinutes; i++)
                {
                    var minutes = sleepMinutesByGuard[entry.Guard] ?? (sleepMinutesByGuard[entry.Guard] = new int[60]);
                    minutes[i]++;
                }
            }

            // Calculate part 1.
            var sleepiestGuard = ((IList<int>)sleepTimeByGuard).IndexOf(sleepTimeByGuard.Max());
            var sleepiestMinute = ((IList<int>)sleepMinutesByGuard[sleepiestGuard]).IndexOf(sleepMinutesByGuard[sleepiestGuard].Max());

            // Calculate part 2.
            var longestTime = int.MinValue;
            var longestGuard = int.MinValue;
            var longestMinute = int.MinValue;
            for (var i = 0; i < sleepMinutesByGuard.Length; i++)
            {
                if (sleepMinutesByGuard[i] != null)
                {
                    for (int j = 0; j < 60; j++)
                    {
                        if (sleepMinutesByGuard[i][j] > longestTime)
                        {
                            longestTime = sleepMinutesByGuard[i][j];
                            longestGuard = i;
                            longestMinute = j;
                        }
                    }
                }
            }
            
            Console.WriteLine("Day 4");
            Console.WriteLine("     - Part 1: " + sleepiestGuard * sleepiestMinute);
            Console.WriteLine("     - Part 2: " + longestGuard * longestMinute);
        }

        private static IEnumerable<(int Guard, TimeSpan SleepingTime, int SleepStart)> GetData()
        {
            var data = File.ReadAllLines("Day4\\input.txt")
                .Select(l => (Date: DateTime.Parse(l.Substring(1, 16)), Action: l.Substring(19, l.Length - 19)))
                .OrderBy(d => d.Date);

            int currentGuard = -1;
            var sleepTime = DateTime.MinValue;
            foreach (var d in data)
            {
                if (d.Action == "falls asleep")
                {
                    sleepTime = d.Date;
                }
                else if (d.Action == "wakes up")
                {
                    yield return (currentGuard, d.Date - sleepTime, sleepTime.Minute);
                }
                else
                {
                    currentGuard = int.Parse(Regex.Match(d.Action, "#([0-9]+)", RegexOptions.Compiled).Groups[1].Value);
                }
            }
        }
    }
}
