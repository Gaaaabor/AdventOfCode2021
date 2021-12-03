using System;
using System.Collections.Generic;

namespace AdventOfCode2021.ConsoleApp.Day2
{
    public static class NavigationOMatic
    {

        public static Coordinate CalculatePosition(string[] navigation)
        {
            var navDict = new Dictionary<Direction, int>();
            foreach (var nav in navigation)
            {
                var s = nav.Split(" ");
                if (!Enum.TryParse<Direction>(s[0], true, out var direction))
                {
                    continue;
                }

                if (!int.TryParse(s[1], out var amount))
                {
                    continue;
                }

                if (!navDict.TryGetValue(direction, out _))
                {
                    navDict.Add(direction, 0);
                }

                navDict[direction] += amount;
            }

            return new Coordinate
            {
                X = navDict[Direction.Forward],
                Y = navDict[Direction.Down] - navDict[Direction.Up]
            };
        }

        public static Coordinate CalculatePosition2(string[] navigation)
        {

            int x = 0;
            int aim = 0;
            int depth = 0;

            foreach (var nav in navigation)
            {
                var s = nav.Split(" ");
                if (!Enum.TryParse<Direction>(s[0], true, out var direction))
                {
                    continue;
                }

                if (!int.TryParse(s[1], out var amount))
                {
                    continue;
                }

                switch (direction)
                {
                    case Direction.Forward:
                        x += amount;
                        depth += aim * amount;
                        break;
                    case Direction.Up:
                        aim -= amount;
                        break;
                    case Direction.Down:
                        aim += amount;
                        break;
                    default:
                        break;
                }
            }

            return new Coordinate
            {
                X = x,
                Y = depth
            };
        }

        private enum Direction
        {
            Forward,
            Up,
            Down
        }
    }
}
