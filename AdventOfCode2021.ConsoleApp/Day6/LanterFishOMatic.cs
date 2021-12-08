using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day6
{
    public static class LanterFishOMatic
    {
        public static long MeasurePopulation(string[] fishReport, int days)
        {
            var initialPopulation = ParseInitialPopulation(fishReport);
            var fishDict = CreateFishDict(initialPopulation);

            //The sliding dictionary method :D
            while (days > 0)
            {
                var lastValue = fishDict[0];
                for (var i = fishDict.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        fishDict[6] += fishDict[i];
                    }

                    var oldValue = fishDict[i];
                    fishDict[i] = lastValue;
                    lastValue = oldValue;
                }

                days--;
            }

            var sum = fishDict.Values.Sum();
            return sum;
        }

        private static IDictionary<int, long> CreateFishDict(List<int> initialPopulation)
        {
            var fishDict = new Dictionary<int, long>();
            for (var i = 0; i < 9; i++)
            {
                fishDict.Add(i, 0);
            }

            for (var i = 0; i < initialPopulation.Count; i++)
            {
                fishDict[initialPopulation[i]]++;
            }

            return fishDict;
        }

        private static List<int> ParseInitialPopulation(string[] fishReport)
        {
            var result = new List<int>();
            foreach (var report in fishReport)
            {
                var numbers = report.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                result.AddRange(numbers);
            }

            return result;
        }
    }
}
