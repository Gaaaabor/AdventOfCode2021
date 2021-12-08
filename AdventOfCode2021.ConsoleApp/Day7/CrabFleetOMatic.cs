using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.ConsoleApp.Day7
{
    public static class CrabFleetOMatic
    {
        public static double GetOptimalFuelConsumption(int[] crabFleetPositions)
        {
            var crabSubmarines = new Dictionary<int, CrabSubmarine>();

            var sum = 0;
            foreach (var crabFleetPosition in crabFleetPositions)
            {
                if (!crabSubmarines.TryGetValue(crabFleetPosition, out var crabSubmarine))
                {
                    crabSubmarine = new CrabSubmarine(crabFleetPosition);
                    crabSubmarines.Add(crabFleetPosition, crabSubmarine);
                }

                crabSubmarine.AddCrab();
                sum += crabFleetPosition;
            }

            var optimal = double.MaxValue;

            foreach (var key1 in crabSubmarines.Keys)
            {
                foreach (var key2 in crabSubmarines.Keys)
                {
                    crabSubmarines[key2].CalculateFuelCost(key1);
                }

                var v = crabSubmarines.Values.Sum(x => x.FuelCost);
                if (v < optimal)
                {
                    optimal = v;
                }
            }

            return optimal;
        }

        public static double GetOptimalFuelConsumption2(int[] crabFleetPositions)
        {
            var crabSubmarines = new Dictionary<int, CrabSubmarine>();

            var sum = 0;
            foreach (var crabFleetPosition in crabFleetPositions)
            {
                if (!crabSubmarines.TryGetValue(crabFleetPosition, out var crabSubmarine))
                {
                    crabSubmarine = new CrabSubmarine(crabFleetPosition);
                    crabSubmarines.Add(crabFleetPosition, crabSubmarine);
                }

                crabSubmarine.AddCrab();
                sum += crabFleetPosition;
            }

            var optimal = double.MaxValue;

            foreach (var key1 in crabSubmarines.Keys)
            {
                foreach (var key2 in crabSubmarines.Keys)
                {
                    crabSubmarines[key2].CalculateFuelCostDynamic(key1);
                }

                var v = crabSubmarines.Values.Sum(x => x.FuelCost);
                if (v < optimal)
                {
                    optimal = v;
                }
            }

            return optimal;
        }

        private class CrabSubmarine
        {
            public int HorizontalPosition { get; private set; }
            public int NumberOfCrabs { get; private set; }
            public double FuelCost { get; private set; }

            public CrabSubmarine(int horizontalPosition)
            {
                HorizontalPosition = horizontalPosition;
            }

            public void AddCrab()
            {
                NumberOfCrabs++;
            }

            public void CalculateFuelCost(double distance)
            {
                FuelCost = Math.Abs(distance - HorizontalPosition) * NumberOfCrabs;
            }

            public void CalculateFuelCostDynamic(double distance)
            {
                var diff = Math.Abs(distance - HorizontalPosition);
                if (diff == 0)
                {
                    FuelCost = 0;
                    return;
                }

                var x = diff;
                for (var i = diff - 1; i > 0; i--)
                {
                    x += i;
                }

                FuelCost = x * NumberOfCrabs;
            }

            public override string ToString()
            {
                return $"H: {HorizontalPosition}, N: {NumberOfCrabs}, F: {FuelCost}";
            }
        }
    }
}
