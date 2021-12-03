using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day1
{
    public class DepthOMeter
    {
        public static int HowDepthIncreases(int[] measurements)
        {
            var changes = 0;

            for (var i = 1; i < measurements.Length - 1; i++)
            {
                if (measurements[i] < measurements[(i + 1)])
                {
                    changes++;
                }
            }

            return changes;
        }

        public static int HowDepthIncreases2(int[] measurements)
        {
            var changes = 0;
            var sum = 0;

            for (var i = 1; i < measurements.Length - 2; i++)
            {
                var first = measurements[i];
                var second = measurements[i + 1];
                var third = measurements[i + 2];

                var temp = first + second + third;
                if (sum != 0 && sum < temp)
                {
                    changes++;
                }

                sum = temp;
            }

            return changes;
        }
    }
}
