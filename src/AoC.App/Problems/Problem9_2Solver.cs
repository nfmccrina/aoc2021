using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("9_2")]
    public class Problem9_2Solver : BaseSolver
    {
        private static int MAP_WIDTH = 100;
        private static int MAP_HEIGHT = 100;
        public override void Solve()
        {
            var input = GetData();
            List<List<(int, int)>> basins = new List<List<(int, int)>>();

            var stopwatch = Stopwatch.StartNew();
            int[,] grid = new int[MAP_HEIGHT, MAP_WIDTH];
            
            for (var row = 0; row < MAP_HEIGHT; row++)
            {
                for (var column = 0; column < MAP_WIDTH; column++)
                {
                    grid[row,column] = int.Parse(input.ElementAt(row).ElementAt(column).ToString());
                }
            }

            for (var row = 0; row < MAP_HEIGHT; row++)
            {
                for (var column = 0; column < MAP_WIDTH; column++)
                {
                    if (!basins.Any(basin => basin.Any(point => point == (row, column))))
                    {
                        var newBasin = TraverseBasin((row, column), grid, new List<(int, int)>()).ToList();

                        if (newBasin.Any())
                        {
                            basins.Add(newBasin);
                        }
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine(basins.Select(basin => basin.Count()).OrderByDescending(count => count).Take(3).Aggregate(1, (state, currentBasinSize) => state * currentBasinSize));
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private IEnumerable<(int, int)> TraverseBasin((int, int) start, int[,] grid, List<(int, int)> visitedPoints)
        {
            var result = new List<(int, int)>();
            var row = start.Item1;
            var column = start.Item2;
            if (
                row < 0 ||
                row >= MAP_HEIGHT ||
                column < 0 ||
                column >= MAP_WIDTH ||
                grid[row, column] == 9
            )
            {
                return result;
            }

            visitedPoints.Add(start);
            var nextPoints = new List<(int, int)>() {
                (row - 1, column),
                (row + 1, column),
                (row, column - 1),
                (row, column + 1)
            };

            return nextPoints
                .Where(point => !visitedPoints.Contains(point))
                .SelectMany(point => TraverseBasin(point, grid, visitedPoints))
                .Append(start);
        }
    }
}