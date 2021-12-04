using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{

    [ProblemName("4_2")]
    public class Problem4_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var data = GetData();
            var stopwatch = Stopwatch.StartNew();
            var randomNumbers = data.First().Split(',').Select(number => int.Parse(number));
            data = data.Skip(2);

            IEnumerable<IEnumerable<int>> boardData = new List<IEnumerable<int>>();
            IEnumerable<int> currentBoardData = new List<int>();
            foreach (var line in data)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    currentBoardData = currentBoardData
                        .Concat(
                                line
                                .Trim()
                                .Split(' ')
                                .Where(item => !string.IsNullOrWhiteSpace(item))
                                .Select(item => item.Trim()).Select(number => int.Parse(number))
                            );
                }
                else
                {
                    boardData = boardData.Append(currentBoardData);
                    currentBoardData = new List<int>();
                }
            }
            if (currentBoardData.Any())
            {
                boardData = boardData.Append(currentBoardData);
                currentBoardData = new List<int>();
            }

            IEnumerable<IEnumerable<int>> lastCompletedBoards = new List<IEnumerable<int>>();
            int lastNumber = 0;
            foreach (var number in randomNumbers)
            {
                boardData = boardData.Select(b => b.Contains(number) ? b.Select(item => item == number ? -1 : item) : b);
                var partitionedData = boardData.GroupBy(b => IsComplete(b));

                if (partitionedData.FirstOrDefault(p => p.Key)?.Any() ?? false)
                {
                    lastNumber = number;
                    lastCompletedBoards = partitionedData.Single(p => p.Key);
                }
                
                boardData = partitionedData.FirstOrDefault(p => !p.Key);
                if (boardData == null)
                {
                    boardData = new List<IEnumerable<int>>();
                }
            }

            stopwatch.Stop();

            Console.WriteLine(lastCompletedBoards.Single().Sum(i => i < 0 ? 0 : i) * lastNumber);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private bool IsComplete(IEnumerable<int> board)
        {
            for (var i = 0; i < 5; i++)
            {
                if (board.Where((number, index) => (index % 5) == i).Sum() == -5)
                {
                    return true;
                }

                if (board.Where((number, index) => (index / 5) == i).Sum() == -5)
                {
                    return true;
                }
            }

            return false;
        }
    }
}