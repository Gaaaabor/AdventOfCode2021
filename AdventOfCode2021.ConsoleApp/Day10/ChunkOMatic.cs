using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.ConsoleApp.Day10
{
    public static class ChunkOMatic
    {
        private const string Openings = "([{<";
        private const string Closings = ")]}>";

        internal static int CalculateSyntaxErrorScore(string[] chunkSource)
        {
            var score = 0;
            var rawChunks = new Stack<char>();

            foreach (var rawChunk in chunkSource)
            {
                for (var i = 0; i < rawChunk.Length; i++)
                {
                    if (Openings.Contains(rawChunk[i]))
                    {
                        rawChunks.Push(rawChunk[i]);
                    }
                    else if (Closings.Contains(rawChunk[i]))
                    {
                        if (Openings.IndexOf(rawChunks.Pop()) != Closings.IndexOf(rawChunk[i]))
                        {
                            score += GetSyntaxErrorScore(rawChunk[i]);
                            break;
                        }
                    }
                }
            }

            return score;
        }

        internal static ulong CalculateCompletionMiddleScore(string[] chunkSource)
        {
            var scores = new List<ulong>();
            foreach (var rawChunk in chunkSource)
            {
                var rawChunks = new Stack<char>();
                var isCorrupt = false;

                var asdf = rawChunk.ToCharArray();
                for (var i = 0; i < asdf.Length; i++)
                {
                    if (Openings.Contains(asdf[i]))
                    {
                        rawChunks.Push(asdf[i]);
                    }
                    else
                    {
                        var p = rawChunks.Pop();
                        if (Openings.IndexOf(p) != Closings.IndexOf(asdf[i]))
                        {
                            isCorrupt = true;
                            break;
                        }
                    }
                }

                if (!isCorrupt)
                {
                    var missingClosings = GetMissingClosings(rawChunks);
                    var score = GetCompletionScore(missingClosings);
                    scores.Add(score);
                }
            }

            var finalScore = scores
                .OrderBy(x => x)
                .ElementAt(scores.Count / 2);

            return finalScore;
        }

        private static string GetMissingClosings(Stack<char> rawChunks)
        {
            var missingClosings = new StringBuilder();
            while (rawChunks.Count != 0)
            {
                var theKnownOpening = Openings.IndexOf(rawChunks.Pop());
                var theMissingClosing = Closings[theKnownOpening];
                missingClosings.Append(theMissingClosing);
            }

            return missingClosings.ToString();
        }

        private static int GetSyntaxErrorScore(char c)
        {
            switch (Closings.IndexOf(c))
            {
                case 0:
                    return 3;
                case 1:
                    return 57;
                case 2:
                    return 1197;
                case 3:
                    return 25137;
                default:
                    throw new ArgumentOutOfRangeException($"{c}");
            }
        }

        private static ulong GetCompletionScore(string completionString)
        {
            return completionString.Aggregate(0ul, (x, y) =>
            {
                x *= 5ul;
                x += (ulong)Closings.IndexOf(y) + 1ul;
                return x;
            });
        }
    }
}
