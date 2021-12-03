﻿using AdventOfCode2021.ConsoleApp.Day1;
using AdventOfCode2021.ConsoleApp.Day2;
using AdventOfCode2021.ConsoleApp.Day3;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var measurements = File
            //    .ReadAllLines("Day1\\Measurements.txt")
            //    .Select(x => int.Parse(x))
            //    .ToArray();

            //var i = DepthOMeter.HowDepthIncreases2(measurements);
            //Console.WriteLine($"Total: {i}");

            //var movements = File
            //    .ReadAllLines("Day2\\Navigation.txt")
            //    .ToArray();

            //var coordinate = NavigationOMatic.CalculatePosition2(movements);

            //var result = coordinate.X * coordinate.Y;
            //Console.WriteLine($"Coordinate: {result}");
            //Console.ReadKey();

            var report = File
                .ReadAllLines("Day3\\Report.txt")
                .ToArray();

            var consumption = PowerOMeter.Calculate(report);

            var result = consumption.GammaRate * consumption.EpsionRate;
            Console.WriteLine($"Consumption: {result}");
            Console.ReadKey();
        }
    }
}
