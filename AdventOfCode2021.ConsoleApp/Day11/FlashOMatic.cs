using System;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day11
{
    internal static class FlashOMatic
    {
        internal static int GetNumberOfFlashesAfter100Steps(string[] octopusSource)
        {
            var octopusMatrix = MapOctopuses(octopusSource);

            var flashes = 0;
            for (var step = 0; step < 100; step++)
            {
                flashes += CountFlashes(octopusMatrix);
            }

            return flashes;
        }

        internal static int GetCycleCountOfMegaFlash(string[] octopusSource)
        {
            var octopusMatrix = MapOctopuses(octopusSource);

            var steps = 0;
            while (true)
            {
                steps++;
                var flashes = CountFlashes(octopusMatrix);
                if (flashes == 100)
                {
                    return steps;
                }
            }
        }

        private static FlashyOctopus[,] MapOctopuses(string[] octopusSource)
        {
            var matrix = new FlashyOctopus[octopusSource.Length, octopusSource[0].Length];

            for (var y = 0; y < octopusSource.Length; y++)
            {
                for (var x = 0; x < octopusSource[y].Length; x++)
                {
                    var mappedOctopus = matrix[y, x];
                    if (mappedOctopus == null)
                    {
                        mappedOctopus = FlashyOctopus.Create(octopusSource[y][x]);
                        matrix[y, x] = mappedOctopus;
                    }

                    var ty = y - 1;
                    var lx = x - 1;
                    var rx = x + 1;
                    var by = y + 1;

                    if (ty >= 0)
                    {
                        var toptopus = matrix[ty, x];
                        if (toptopus == null)
                        {
                            toptopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(ty).ElementAtOrDefault(x));
                            matrix[ty, x] = toptopus;
                        }

                        mappedOctopus.SetTop(toptopus);
                    }

                    if (ty >= 0 && lx >= 0)
                    {
                        var topLeftopus = matrix[ty, lx];
                        if (topLeftopus == null)
                        {
                            topLeftopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(ty).ElementAtOrDefault(lx));
                            matrix[ty, lx] = topLeftopus;
                        }

                        mappedOctopus.SetTopLeft(topLeftopus);
                    }

                    if (ty >= 0 && rx < octopusSource[y].Length)
                    {
                        var topRightopus = matrix[ty, rx];
                        if (topRightopus == null)
                        {
                            topRightopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(ty).ElementAtOrDefault(rx));
                            matrix[ty, rx] = topRightopus;
                        }

                        mappedOctopus.SetTopRight(topRightopus);
                    }

                    if (lx >= 0)
                    {
                        var leftopus = matrix[y, lx];
                        if (leftopus == null)
                        {
                            leftopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(y).ElementAtOrDefault(lx));
                            matrix[y, lx] = leftopus;
                        }

                        mappedOctopus.SetLeft(leftopus);
                    }

                    if (rx < octopusSource[y].Length)
                    {
                        var rightopus = matrix[y, rx];
                        if (rightopus == null)
                        {
                            rightopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(y).ElementAtOrDefault(rx));
                            matrix[y, rx] = rightopus;
                        }

                        mappedOctopus.SetRight(rightopus);
                    }

                    if (by < octopusSource.Length && lx >= 0)
                    {
                        var bottomLeftopus = matrix[by, lx];
                        if (bottomLeftopus == null)
                        {
                            bottomLeftopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(by).ElementAtOrDefault(lx));
                            matrix[by, lx] = bottomLeftopus;
                        }

                        mappedOctopus.SetBottomLeft(bottomLeftopus);
                    }

                    if (by < octopusSource.Length && rx < octopusSource[y].Length)
                    {
                        var bottomRightopus = matrix[by, rx];
                        if (bottomRightopus == null)
                        {
                            bottomRightopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(by).ElementAtOrDefault(rx));
                            matrix[by, rx] = bottomRightopus;
                        }

                        mappedOctopus.SetBottomRight(bottomRightopus);
                    }

                    if (by < octopusSource.Length)
                    {
                        var bottomopus = matrix[by, x];
                        if (bottomopus == null)
                        {
                            bottomopus = FlashyOctopus.Create(octopusSource.ElementAtOrDefault(by).ElementAtOrDefault(x));
                            matrix[by, x] = bottomopus;
                        }

                        mappedOctopus.SetBottom(bottomopus);
                    }
                }
            }

            return matrix;
        }

        private static int CountFlashes(FlashyOctopus[,] octopusSource)
        {
            var flashes = 0;
            foreach (var item in octopusSource)
            {
                flashes += item.Up();
            }

            foreach (var item in octopusSource)
            {
                item.Reset();
            }

            return flashes;
        }

        internal class FlashyOctopus
        {
            private bool _onCooldown;
            private Guid _id;

            public int FlashCount { get; private set; }
            public int Charge { get; private set; }
            public FlashyOctopus Top { get; private set; }
            public FlashyOctopus TopLeft { get; private set; }
            public FlashyOctopus TopRight { get; private set; }
            public FlashyOctopus Left { get; private set; }
            public FlashyOctopus Right { get; private set; }
            public FlashyOctopus BottomLeft { get; private set; }
            public FlashyOctopus BottomRight { get; private set; }
            public FlashyOctopus Bottom { get; private set; }

            public FlashyOctopus(int charge)
            {
                _id = Guid.NewGuid();
                Charge = charge;
            }

            internal static FlashyOctopus Create(char rawCharge)
            {
                return new FlashyOctopus(int.Parse(rawCharge.ToString()));
            }

            internal void Reset()
            {
                _onCooldown = false;
            }

            internal int Up()
            {
                var flashes = 0;
                if (_onCooldown)
                {
                    return flashes;
                }

                Charge++;
                if (Charge > 9)
                {
                    _onCooldown = true;
                    flashes += Flash();
                }

                return flashes;
            }

            internal void SetTop(FlashyOctopus flashyOctopus)
            {
                if (Top != null || flashyOctopus == null)
                {
                    return;
                }

                Top = flashyOctopus;
                flashyOctopus.SetBottom(this);
            }

            internal void SetTopLeft(FlashyOctopus flashyOctopus)
            {
                if (TopLeft != null || flashyOctopus == null)
                {
                    return;
                }

                TopLeft = flashyOctopus;
                flashyOctopus.SetBottomRight(this);
            }

            internal void SetTopRight(FlashyOctopus flashyOctopus)
            {
                if (TopRight != null || flashyOctopus == null)
                {
                    return;
                }

                TopRight = flashyOctopus;
                flashyOctopus.SetBottomLeft(this);
            }

            internal void SetLeft(FlashyOctopus flashyOctopus)
            {
                if (Left != null || flashyOctopus == null)
                {
                    return;
                }

                Left = flashyOctopus;
                flashyOctopus.SetRight(this);
            }

            internal void SetRight(FlashyOctopus flashyOctopus)
            {
                if (Right != null || flashyOctopus == null)
                {
                    return;
                }

                Right = flashyOctopus;
                flashyOctopus.SetLeft(this);
            }

            internal void SetBottomLeft(FlashyOctopus flashyOctopus)
            {
                if (BottomLeft != null || flashyOctopus == null)
                {
                    return;
                }

                BottomLeft = flashyOctopus;
                flashyOctopus.SetTopRight(this);
            }

            internal void SetBottomRight(FlashyOctopus flashyOctopus)
            {
                if (BottomRight != null || flashyOctopus == null)
                {
                    return;
                }

                BottomRight = flashyOctopus;
                flashyOctopus.SetTopLeft(this);
            }

            internal void SetBottom(FlashyOctopus flashyOctopus)
            {
                if (Bottom != null || flashyOctopus == null)
                {
                    return;
                }

                Bottom = flashyOctopus;
                flashyOctopus.SetTop(this);
            }

            private int Flash()
            {
                var flashes = 1;
                Charge = 0;

                flashes += new[] { Top, TopLeft, TopRight, Left, Right, BottomLeft, BottomRight, Bottom }
                    .Where(x => x != null)
                    .Select(x => x.Up())
                    .Sum();

                return flashes;
            }

            public override string ToString()
            {
                return $"{Charge} - {_id:N}";
            }
        }
    }
}
