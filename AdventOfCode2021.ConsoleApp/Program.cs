using AdventOfCode2021.ConsoleApp.Day1;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var measurements = File
                .ReadAllLines("Day1\\Measurements.txt")
                .Select(x => int.Parse(x))
                .ToArray();

            var i = DepthOMeter.HowDepthIncreases2(measurements);
            Console.WriteLine($"Total: {i}");
        }
    }
}
