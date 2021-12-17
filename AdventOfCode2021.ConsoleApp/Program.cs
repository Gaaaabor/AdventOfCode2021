using AdventOfCode2021.ConsoleApp.Day1;
using AdventOfCode2021.ConsoleApp.Day10;
using AdventOfCode2021.ConsoleApp.Day11;
using AdventOfCode2021.ConsoleApp.Day2;
using AdventOfCode2021.ConsoleApp.Day3;
using AdventOfCode2021.ConsoleApp.Day4;
using AdventOfCode2021.ConsoleApp.Day5;
using AdventOfCode2021.ConsoleApp.Day6;
using AdventOfCode2021.ConsoleApp.Day7;
using AdventOfCode2021.ConsoleApp.Day8;
using AdventOfCode2021.ConsoleApp.Day9;
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

            //var report = File
            //    .ReadAllLines("Day3\\Report.txt")
            //    .ToArray();

            //var consumption = PowerOMeter.Calculate2(report);

            //var result = consumption.GammaRate * consumption.EpsionRate;
            //Console.WriteLine($"Consumption: {result}");
            //Console.ReadKey();

            //var bingoSource = File
            //    .ReadAllLines("Day4\\Bingo.txt")
            //    .ToArray();

            //var score = BingOMatic.GetMyWinningScoreLast(bingoSource);
            //Console.WriteLine($"My bingo score: {score}");
            //Console.ReadKey();

            //var ventSource = File
            //    .ReadAllLines("Day5\\Vents.txt")
            //    .ToArray();

            //var intersections = ThermalVentOMatic.GetVentIntersections(ventSource);
            //Console.WriteLine($"Intersections: {intersections}");
            //Console.ReadKey();

            //var fishReport = File
            //    .ReadAllLines("Day6\\FishReport.txt")
            //    .ToArray();

            //var days = 256;
            //var population = LanterFishOMatic.MeasurePopulation(fishReport, days);
            //Console.WriteLine($"Population after {days}: {population}");
            //Console.ReadKey();

            //var crabFleetPositions = File
            //    .ReadAllText("Day7\\CrabFleetPositions.txt")
            //    .Split(",", StringSplitOptions.RemoveEmptyEntries)
            //    .Select(int.Parse)
            //    .ToArray();

            //var optimalFuelConsumption = CrabFleetOMatic.GetOptimalFuelConsumption2(crabFleetPositions);
            //Console.WriteLine($"Optimal fuel consumption: {optimalFuelConsumption}");
            //Console.ReadKey();

            //var segmentSource = File
            //    .ReadAllLines("Day8\\SegmentSource.txt")
            //    .ToArray();

            //var digitCount = SegmentOMatic.CountDigitsFull(segmentSource);
            //Console.WriteLine($"Count of 1,4,7 and 8 digits: {digitCount}");
            //Console.ReadKey();

            //var lavaTubeSource = File
            //    .ReadAllLines("Day9\\LavaTubeSource.txt")
            //    .ToArray();

            //var largestBasin = SmokeBasinOMatic.LargestBasinSize(lavaTubeSource);
            //Console.WriteLine($"Largest basin: {largestBasin}");
            //Console.ReadKey();

            //var chunkSource = File
            //    .ReadAllLines("Day10\\ChunkSource.txt")
            //    .ToArray();

            //var completionMiddleScore = ChunkOMatic.CalculateCompletionMiddleScore(chunkSource);
            //Console.WriteLine($"Completion middle score: {completionMiddleScore}");
            //Console.ReadKey();

            var octopusSource = File
                .ReadAllLines("Day11\\OctopusSource.txt")
                .ToArray();

            var flashCount = FlashOMatic.GetNumberOfFlashesAfter100Steps(octopusSource);
            Console.WriteLine($"Flash count: {flashCount}");
            Console.ReadKey();
        }
    }
}
