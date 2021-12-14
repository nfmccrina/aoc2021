using System;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day13
{
    [ProblemName("13_1")]
    public class Problem13_1Solver : BaseSolver
    {
        private static int PAPER_WIDTH = 0;
        private static int PAPER_HEIGHT = 0;
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            var coordinates = input
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => (int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1])));

            var folds = input
                .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
                .Skip(1)
                .Select(line => line.Replace("fold along ", ""))
                .Select(line => (line.Split('=')[0], int.Parse(line.Split('=')[1])));

            PAPER_WIDTH = coordinates.Select(coordinate => coordinate.Item1).Max() + 1;
            PAPER_HEIGHT = coordinates.Select(coordinate => coordinate.Item2).Max() + 1;

            var paper = new bool[PAPER_WIDTH, PAPER_HEIGHT];

            foreach (var coordinate in coordinates)
            {
                paper[coordinate.Item1, coordinate.Item2] = true;
            }
            // for (var row = 0; row < PAPER_HEIGHT; row++)
            // {
            //     for (var column = 0; column < PAPER_WIDTH; column++)
            //     {
            //         paper[column, row] = coordinates.Contains((column, row));
            //     }
            // }

            foreach (var fold in folds.Take(1))
            {
                Fold(paper, fold);
            }

            var dotCount = 0;
            for (var row = 0; row < PAPER_HEIGHT; row++)
            {
                for (var column = 0; column < PAPER_WIDTH; column++)
                {
                    if (paper[column, row])
                    {
                        dotCount++;
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine(dotCount);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        public void Fold(bool[,] paper, (string, int) foldInfo)
        {
            if (foldInfo.Item1 == "x")
            {
                FoldLeft(paper, foldInfo.Item2);
            }

            if (foldInfo.Item1 == "y")
            {
                FoldUp(paper, foldInfo.Item2);
            }
        }

        public void FoldLeft(bool[,] paper, int index)
        {
            for (var row = 0; row < PAPER_HEIGHT; row++)
            {
                for (var column = 0; column < PAPER_WIDTH; column++)
                {
                    if (column > index)
                    {
                        if (paper[column, row])
                        {
                            paper[index - (column - index), row] = true;
                            paper[column, row] = false;
                        }
                    }
                }
            }
        }

        public void FoldUp(bool[,] paper, int index)
        {
            for (var row = 0; row < PAPER_HEIGHT; row++)
            {
                for (var column = 0; column < PAPER_WIDTH; column++)
                {
                    if (row > index)
                    {
                        if (paper[column, row])
                        {
                            paper[column, index - (row - index)] = true;
                            paper[column, row] = false;
                        }
                    }
                }
            }
        }
    }
}