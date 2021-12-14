using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day9
{
    public static class SmokeBasinOMatic
    {
        internal static int RiskOfLowPoints(string[] lavaTubeSource)
        {
            var risk = CalculateRiskOfLowPoints(lavaTubeSource);
            return risk;
        }

        private static int CalculateRiskOfLowPoints(string[] lavaTubeSource, List<Point> lowPoints = null)
        {
            var risk = 0;

            for (var y = 0; y < lavaTubeSource.Length; y++)
            {
                for (var x = 0; x < lavaTubeSource[y].Length; x++)
                {
                    var top = y - 1 < 0 ? int.MaxValue : int.Parse(lavaTubeSource[y - 1][x].ToString());
                    var left = x - 1 < 0 ? int.MaxValue : int.Parse(lavaTubeSource[y][x - 1].ToString());
                    var center = int.Parse(lavaTubeSource[y][x].ToString());
                    var right = x + 1 >= lavaTubeSource[y].Length ? int.MaxValue : int.Parse(lavaTubeSource[y][x + 1].ToString());
                    var bottom = y + 1 >= lavaTubeSource.Length ? int.MaxValue : int.Parse(lavaTubeSource[y + 1][x].ToString());

                    if (new[] { top, left, right, bottom }.Min() > center)
                    {
                        risk += center + 1;
                        lowPoints?.Add(new Point(y, x));
                    }
                }
            }

            return risk;
        }

        internal static int LargestBasinSize(string[] lavaTubeSource)
        {
            var lowPoints = new List<Point>();
            CalculateRiskOfLowPoints(lavaTubeSource, lowPoints);

            var largestBasin = MeasureBasins(lavaTubeSource, lowPoints);
            return largestBasin;
        }

        private static int MeasureBasins(string[] lavaTubeSource, List<Point> lowPoints)
        {
            var basinSize = new List<int>();
            foreach (var lowPoint in lowPoints)
            {
                var points = MeasureBasin(lavaTubeSource, new List<Point> { lowPoint });
                basinSize.Add(points.Count);
            }

            var m1 = basinSize.Max();
            basinSize.Remove(m1);

            var m2 = basinSize.Max();
            basinSize.Remove(m2);

            var m3 = basinSize.Max();

            return m1 * m2 * m3;
        }

        private static List<Point> MeasureBasin(string[] lavaTubeSource, List<Point> points)
        {
            var q = new Queue<Point>(points);
            while (q.Count > 0)
            {
                var actualPoint = q.Dequeue();
                var point = Move(actualPoint.Y + 1, actualPoint.X, lavaTubeSource);
                if (point.HasValue && !points.Contains(point.Value))
                {
                    q.Enqueue(point.Value);
                    points.Add(point.Value);
                }

                point = Move(actualPoint.Y - 1, actualPoint.X, lavaTubeSource);
                if (point.HasValue && !points.Contains(point.Value))
                {
                    q.Enqueue(point.Value);
                    points.Add(point.Value);
                }

                point = Move(actualPoint.Y, actualPoint.X + 1, lavaTubeSource);
                if (point.HasValue && !points.Contains(point.Value))
                {
                    q.Enqueue(point.Value);
                    points.Add(point.Value);
                }

                point = Move(actualPoint.Y, actualPoint.X - 1, lavaTubeSource);
                if (point.HasValue && !points.Contains(point.Value))
                {
                    q.Enqueue(point.Value);
                    points.Add(point.Value);
                }
            }

            return points;
        }

        private static Point? Move(int y, int x, string[] lavaTubeSource)
        {
            var c = lavaTubeSource.ElementAtOrDefault(y)?.ElementAtOrDefault(x);
            if (c.HasValue && char.IsDigit(c.Value) && int.Parse(c.ToString()) < 9)
            {
                return new Point(y, x);
            }

            return null;
        }
    }

    public struct Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int y, int x)
        {
            Y = y;
            X = x;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
