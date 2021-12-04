using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day4
{
    public static class BingOMatic
    {
        public static int GetMyWinningScoreFirst(string[] source)
        {
            var drawnNumbers = ParseNumbers(source[0]);
            var bingoMatrices = ParseMatrices(source[2..]).ToList();
            foreach (var number in drawnNumbers)
            {
                foreach (var bingoMatrix in bingoMatrices)
                {
                    bingoMatrix.HasNumber(number);
                    if (bingoMatrix.IsBingo())
                    {
                        return bingoMatrix.CalculateScore(number);
                    }
                }
            }

            return 0;
        }

        public static int GetMyWinningScoreLast(string[] source)
        {
            var drawnNumbers = ParseNumbers(source[0]);
            var bingoMatrices = ParseMatrices(source[2..]).ToList();

            var winningOnes = new Stack<BingoMatrix>();

            foreach (var number in drawnNumbers)
            {
                foreach (var bingoMatrix in bingoMatrices)
                {
                    if (winningOnes.Contains(bingoMatrix))
                    {
                        continue;
                    }

                    bingoMatrix.HasNumber(number);
                    if (bingoMatrix.IsBingo())
                    {
                        bingoMatrix.CalculateScore(number);
                        winningOnes.Push(bingoMatrix);
                    }
                }
            }

            var lastWinningBingo = winningOnes.Pop();
            return lastWinningBingo.Score;
        }

        private static IEnumerable<BingoMatrix> ParseMatrices(string[] source)
        {
            var bingoMatrix = new BingoMatrix();
            foreach (var line in source)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return bingoMatrix;
                    bingoMatrix = new BingoMatrix();
                    continue;
                }

                var numbers = ParseNumbers(line, " ");
                bingoMatrix.AddRow(numbers);
            }
        }

        private static int[] ParseNumbers(string source, string separator = ",")
        {
            return source
                .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private class BingoMatrix
        {
            private int _actualRow = 0;

            public MarkableNumber[,] Bingo { get; } = new MarkableNumber[5, 5];

            public int Score { get; private set; }

            public void AddRow(int[] numbers)
            {
                for (var i = 0; i < numbers.Length; i++)
                {
                    Bingo[_actualRow, i] = new MarkableNumber
                    {
                        Value = numbers[i]
                    };
                }

                _actualRow++;
            }

            public bool HasNumber(int number)
            {
                for (var x = 0; x < 5; x++)
                {
                    for (var y = 0; y < 5; y++)
                    {
                        if (Bingo[x, y].Value == number)
                        {
                            Bingo[x, y].IsMarked = true;
                            return true;
                        }
                    }
                }

                return false;
            }

            public bool IsBingo()
            {
                if (HasSeries((x, y) => Bingo[x, y].IsMarked) || HasSeries((x, y) => Bingo[y, x].IsMarked))
                {
                    return true;
                }

                return false;
            }

            public int CalculateScore(int winnerNumber)
            {
                Score = 0;
                for (var x = 0; x < 5; x++)
                {
                    for (var y = 0; y < 5; y++)
                    {
                        if (!Bingo[x, y].IsMarked)
                        {
                            Score += Bingo[x, y].Value;
                        }
                    }
                }

                Score *= winnerNumber;

                return Score;
            }

            private bool HasSeries(Func<int, int, bool> isMarkedCheck)
            {
                for (var x = 0; x < 5; x++)
                {
                    var won = true;
                    for (var y = 0; y < 5; y++)
                    {
                        if (!isMarkedCheck(x, y))
                        {
                            won = false;
                            break;
                        }
                    }

                    if (won)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private struct MarkableNumber
        {
            public int Value { get; set; }
            public bool IsMarked { get; set; }

            public override string ToString()
            {
                return $"{Value}:{IsMarked}";
            }
        }
    }
}
