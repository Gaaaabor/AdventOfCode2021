using System;
using System.Collections.Generic;
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
            var entryScore = 0;

            foreach (var segments in segmentSource)
            {
                var segmentParts = segments.Split("|", StringSplitOptions.RemoveEmptyEntries);
                entryScore += AssumeNumbers(segmentParts[0], segmentParts[1]);
            }

            return entryScore;
        }

        private static int AssumeNumbers(string firstPart, string secondPart)
        {
            var sevenSegment = new SevenSegment();
            var segments = firstPart.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var segment in segments)
            {
                sevenSegment.Store(segment);
            }

            sevenSegment.AssumeNumbers();

            var numbers = sevenSegment.Decode(secondPart);

            return numbers;
        }

        internal class SevenSegment
        {
            private IDictionary<int, string> _segments = new Dictionary<int, string>();

            internal void Add(string segment, int number)
            {
                if (_segments.ContainsKey(number))
                {
                    _segments[number] += $"|{segment}";
                }
                else
                {
                    _segments.Add(number, segment);
                }
            }

            internal void AssumeNumbers()
            {
                var result960 = Assume(
                    _segments[960].Split("|", StringSplitOptions.RemoveEmptyEntries),
                    new string[]
                    {
                        _segments[1],
                        _segments[4],
                        _segments[7]
                    },
                    (score, item) =>
                    {
                        switch (score)
                        {
                            case 9:
                                _segments[9] = item;
                                break;

                            case 10:
                                _segments[0] = item;
                                break;

                            case 12:
                                _segments[6] = item;
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(score));
                        }
                    });

                if (result960)
                {
                    _segments.Remove(960);
                }

                var result532 = Assume(
                    _segments[532].Split("|"),
                    new string[]
                    {
                        _segments[1],
                        _segments[4],
                        _segments[7]
                    },
                    (score, item) =>
                    {
                        switch (score)
                        {
                            case 7:
                                _segments[3] = item;
                                break;

                            case 9:
                                _segments[5] = item;
                                break;

                            case 10:
                                _segments[2] = item;
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(score));
                        }
                    });

                if (result532)
                {
                    _segments.Remove(532);
                }
            }

            internal void Store(string segment)
            {
                switch (segment.Length)
                {
                    case 2:
                        //Its a ONE
                        Add(segment, 1);
                        break;
                    case 3:
                        //Its a SEVEN
                        Add(segment, 7);
                        break;
                    case 4:
                        //Its a FOUR
                        Add(segment, 4);
                        break;
                    case 7:
                        //Its an EIGHT
                        Add(segment, 8);
                        break;

                    case 5:
                        Add(segment, 532);
                        break;

                    case 6:
                        Add(segment, 960);
                        break;

                    default:
                        break;
                }
            }

            internal int Decode(string secondPart)
            {
                var score = string.Empty;

                var secondPartSegments = secondPart.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var secondPartSegment in secondPartSegments)
                {
                    foreach (var segment in _segments) 
                    {
                        if (secondPartSegment.Length != segment.Value.Length)
                        {
                            continue;
                        }

                        var match = secondPartSegment.Except(segment.Value).Count() == 0;
                        if (match)
                        {
                            score += segment.Key;                            
                        }
                    }
                }

                if (score.Length != 4)
                {
                    throw new Exception("Decode failed!");
                }

                return int.Parse(score);
            }

            private bool Assume(string[] source, string[] keys, Action<int, string> scoringAction)
            {
                foreach (var item in source)
                {
                    var score = 0;
                    foreach (var key in keys)
                    {
                        score += item.Except(key).Count();
                    }

                    scoringAction(score, item);
                }

                return true;
            }
        }
    }
}
