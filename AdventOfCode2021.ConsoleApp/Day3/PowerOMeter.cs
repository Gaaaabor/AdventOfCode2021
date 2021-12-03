using System;

namespace AdventOfCode2021.ConsoleApp.Day3
{
    public static class PowerOMeter
    {
        public static Consumption Calculate(string[] report)
        {
            var ones = new int[report[0].Length];
            var zeros = new int[ones.Length];

            foreach (var item in report)
            {
                for (var i = 0; i < item.Length; i++)
                {
                    var d = item[i].ToString();
                    var a = int.Parse(d);
                    if (a == 0)
                    {
                        zeros[i]++;
                    }
                    else
                    {
                        ones[i]++;
                    }
                }
            }

            var gamma = string.Empty;
            var epsilon = string.Empty;
            for (var i = 0; i < ones.Length; i++)
            {
                var moreZero = ones[i] < zeros[i];
                gamma += moreZero ? "0" : "1";
                epsilon += moreZero ? "1" : "0";
            }

            return new Consumption
            {
                GammaRate = Convert.ToInt32(gamma, 2),
                EpsionRate = Convert.ToInt32(epsilon, 2),
            };
        }
    }
}
