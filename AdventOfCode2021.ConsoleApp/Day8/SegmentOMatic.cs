using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day8
{
    public static class SegmentOMatic
    {
        public static int CountDigitsLastFour(string[] segmentSource)
        {
            var digitCount = segmentSource
                .Select(x => x
                    .Split("|")
                    .Last()
                    .Split(" ")
                    .Count(x => (x.Length >= 2 && x.Length <= 4) || x.Length == 7)
                    )
                .Sum();

            return digitCount;
        }
        public static int CountDigitsFull(string[] segmentSource)
        {
            //TODO
            var digitCount = 0;

            return digitCount;
        }
    }
}
