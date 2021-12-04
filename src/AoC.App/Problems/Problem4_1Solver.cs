using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    class BingoBoard
    {
        public BingoBoard(IEnumerable<int> squares)
        {
            board = new bool[5, 5] {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
            };

            coordinateMap = new Dictionary<int, (int, int)>();

            for (var i = 0; i < 25; i++)
            {
                coordinateMap[squares.ElementAt(i)] = (i / 5, i % 5);
            }

            values = squares;
            lastNumberApplied = 0;
        }

        private BingoBoard(IEnumerable<int> unmarkedValues, bool[,] board, Dictionary<int, (int, int)> coordinateMap, int lastNumberApplied)
        {
            this.board = board;
            this.values = unmarkedValues;
            this.coordinateMap = coordinateMap;
            this.lastNumberApplied = lastNumberApplied;
        }

        public BingoBoard ApplyNumber(int number)
        {
            if (coordinateMap.ContainsKey(number))
            {
                var coordinate = coordinateMap[number];
                board[coordinate.Item1, coordinate.Item2] = true;
                values = values.Where(i => i != number);
            }

            return new BingoBoard(values, board, coordinateMap, number);
        }

        public int GetScore()
        {
            return values.Sum() * lastNumberApplied;
        }

        public bool IsComplete()
        {
            return (board[0, 0] && board[0, 1] && board[0, 2] && board[0, 3] && board[0, 4]) ||
                (board[1, 0] && board[1, 1] && board[1, 2] && board[1, 3] && board[1, 4]) ||
                (board[2, 0] && board[2, 1] && board[2, 2] && board[2, 3] && board[2, 4]) ||
                (board[3, 0] && board[3, 1] && board[3, 2] && board[3, 3] && board[3, 4]) ||
                (board[4, 0] && board[4, 1] && board[4, 2] && board[4, 3] && board[4, 4]) ||
                (board[0, 0] && board[1, 0] && board[2, 0] && board[3, 0] && board[4, 0]) ||
                (board[0, 1] && board[1, 1] && board[2, 1] && board[3, 1] && board[4, 1]) ||
                (board[0, 2] && board[1, 2] && board[2, 2] && board[3, 2] && board[4, 2]) ||
                (board[0, 3] && board[1, 3] && board[2, 3] && board[3, 3] && board[4, 3]) ||
                (board[0, 4] && board[1, 4] && board[2, 4] && board[3, 4] && board[4, 4]);
        }

        private int lastNumberApplied;
        private IEnumerable<int> values;
        private bool[,] board;
        private Dictionary<int, (int, int)> coordinateMap;
    }

    [ProblemName("4_1")]
    public class Problem4_1Solver : BaseSolver
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

            var boards = boardData.Select(item => new BingoBoard(item));
            var score = randomNumbers.Aggregate(boards, (state, number) => {
                if (state.Any(b => b.IsComplete()))
                {
                    return state;
                }

                return (state.Select(b => b.ApplyNumber(number)));
            }, state => state.Single(b => b.IsComplete()).GetScore());
            stopwatch.Stop();

            Console.WriteLine(score);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}