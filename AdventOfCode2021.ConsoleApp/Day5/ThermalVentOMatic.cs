namespace AdventOfCode2021.ConsoleApp.Day5
{
    public static class ThermalVentOMatic
    {
        public static int GetVentIntersections(string[] source)
        {
            var map = CreateMap(source);
            var overlappings = GetIntersections(map);
            return overlappings;
        }

        private static int[,] CreateMap(string[] source)
        {
            var map = new int[999, 999];

            foreach (var item in source)
            {
                var rawPoints = item.Split("->", System.StringSplitOptions.RemoveEmptyEntries);

                var startPoint = Point.Parse(rawPoints[0]);
                var endPoint = Point.Parse(rawPoints[1]);

                Mark(startPoint, endPoint, ref map);
            }

            return map;
        }

        private static void Mark(Point startPoint, Point endPoint, ref int[,] map)
        {
            var dX = endPoint.X - startPoint.X;
            var dY = endPoint.Y - startPoint.Y;

            var deltaX = dX > 0 ? 1 : -1;
            var deltaY = dY > 0 ? 1 : -1;

            if (dX == 0)
            {
                var x = startPoint.X;

                for (var y = startPoint.Y; (y - deltaY) != endPoint.Y; y += deltaY)
                {
                    map[x, y]++;
                }

                return;
            }

            if (dY == 0)
            {
                var y = startPoint.Y;

                for (var x = startPoint.X; (x - deltaX) != endPoint.X; x += deltaX)
                {
                    map[x, y]++;
                }

                return;
            }

            //Diagonal            
            for (int x = startPoint.X, y = startPoint.Y; (x - deltaX) != endPoint.X || (y - deltaY) != endPoint.Y; x += deltaX, y += deltaY)
            {
                map[x, y]++;
            }
        }

        private static int GetIntersections(int[,] map)
        {
            var intersections = 0;
            for (int x = 0; x < 999; x++)
            {
                for (int y = 0; y < 999; y++)
                {
                    if (map[x, y] >= 2)
                    {
                        intersections++;
                    }
                }
            }

            return intersections;
        }

        private struct Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"X: {X}, Y: {Y}";
            }

            internal static Point Parse(string source)
            {
                var split = source.Split(",", System.StringSplitOptions.RemoveEmptyEntries);

                return new Point(int.Parse(split[0]), int.Parse(split[1]));
            }
        }
    }
}
