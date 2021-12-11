using System;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("9_1")]
    public class Problem9_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            var gridWidth = input.First().Count();
            var gridHeight = input.Count();
            int[,] grid = new int[gridHeight, gridWidth];
            
            for (var row = 0; row < gridHeight; row++)
            {
                for (var column = 0; column < gridWidth; column++)
                {
                    grid[row,column] = int.Parse(input.ElementAt(row).ElementAt(column).ToString());
                }
            }

            var result = 0;

            for (var row = 0; row < gridHeight; row++)
            {
                for (var column = 0; column < gridWidth; column++)
                {
                    var currentHeight = grid[row, column];
                    if (
                        ((row == 0) ? true : currentHeight < grid[row - 1, column]) &&
                        ((column == 0) ? true : currentHeight < grid[row, column - 1]) &&
                        ((row == (gridHeight - 1)) ? true : currentHeight < grid[row + 1, column]) &&
                        ((column == (gridWidth - 1)) ? true : currentHeight < grid[row, column + 1])
                    )
                    {
                        result += (1 + grid[row, column]);
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine(result);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}