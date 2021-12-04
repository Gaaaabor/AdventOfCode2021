using System;
using System.Collections.Generic;
using System.Linq;

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

        public static Consumption Calculate2(string[] source)
        {
            var binaryBalance = GetBinaryBalance(source);
            var o2GenRating = GetRating(source.ToArray(), ref binaryBalance, x => x.OneCount < x.ZeroCount ? '1' : '0');

            binaryBalance = GetBinaryBalance(source);
            var co2ScrubRating = GetRating(source.ToArray(), ref binaryBalance, x => x.OneCount < x.ZeroCount ? '0' : '1');

            return new Consumption
            {
                GammaRate = Convert.ToInt32(o2GenRating[0], 2),
                EpsionRate = Convert.ToInt32(co2ScrubRating[0], 2),
            };
        }

        private static string[] GetRating(string[] source, ref BinaryBalance[] binaryBalance, Func<BinaryBalance, char> rule)
        {
            for (var i = 0; i < binaryBalance.Length; i++)
            {
                var balance = binaryBalance[i];
                source = Search(source, i, rule(balance)).ToArray();

                if (source.Length == 1)
                {
                    break;
                }

                binaryBalance = GetBinaryBalance(source);
            }

            return source;
        }

        private static BinaryBalance[] GetBinaryBalance(string[] source)
        {
            var binaryBalance = new BinaryBalance[source[0].Length];

            foreach (var bits in source)
            {
                for (var i = 0; i < bits.Length; i++)
                {
                    if (bits[i] == '0')
                    {
                        binaryBalance[i].ZeroCount++;
                    }
                    else
                    {
                        binaryBalance[i].OneCount++;
                    }
                }
            }

            return binaryBalance;
        }

        private static IEnumerable<string> Search(string[] source, int position, char value)
        {
            for (var i = 0; i < source.Length; i++)
            {
                if (source[i][position] == value)
                {
                    yield return source[i];
                }
            }
        }

        private struct BinaryBalance
        {
            public int OneCount { get; set; }
            public int ZeroCount { get; set; }
        }
    }
}
