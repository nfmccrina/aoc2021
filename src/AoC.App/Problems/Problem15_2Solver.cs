using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day15
{
    [ProblemName("15_2")]
    public class Problem15_2Solver : BaseSolver
    {
        private static int width = 0;
        private static int height = 0;
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            width = input.First().Count() * 5;
            height = input.Count() * 5;
            var risks = new int[width, height];
            var gScores = new int[width, height];
            var fScores = new int[width, height];
            var openSet = new PriorityQueue<(int, int), int>();

            int column = 0;
            int row = 0;
            foreach (var line in input)
            {
                foreach (var digit in line.Select(d => int.Parse(d.ToString())))
                {
                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            var currentLocation = (column + (i * (width / 5)), row + (j * (height / 5)));

                            var value = digit + i + j;
                            while (value > 9)
                            {
                                value -= 9;
                            }

                            risks[currentLocation.Item1, currentLocation.Item2] = value;
                        }
                    }
                    
                    column++;
                }
                column = 0;
                row++;
            }

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    gScores[i, j] = int.MaxValue;
                    fScores[i, j] = int.MaxValue;
                }
            }

            var leastRisk = int.MaxValue;
            var start = (0, 0);
            Func<(int, int), int> h = location => (1 * risks[location.Item1, location.Item1]) + ((width - location.Item1) + (height - location.Item2));
            gScores[start.Item1, start.Item2] = 0;
            fScores[start.Item1, start.Item2] = gScores[start.Item1, start.Item2] + h(start);
            openSet.Enqueue(start, fScores[start.Item1, start.Item2]);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                if (current.Item1 == (width - 1) && current.Item2 == (height - 1))
                {
                    leastRisk = gScores[current.Item1, current.Item2];
                    break;
                }

                var left = (current.Item1 - 1, current.Item2);
                var right = (current.Item1 + 1, current.Item2);
                var up = (current.Item1, current.Item2 - 1);
                var down = (current.Item1, current.Item2 + 1);

                if (left.Item1 >= 0)
                {
                    var tentativeGScore = gScores[current.Item1, current.Item2] + risks[left.Item1, left.Item2];

                    if (tentativeGScore < gScores[left.Item1, left.Item2])
                    {
                        gScores[left.Item1, left.Item2] = tentativeGScore;
                        fScores[left.Item1, left.Item2] = tentativeGScore + h(left);

                        if (!openSet.UnorderedItems.Any(item => item.Element == left))
                        {
                            openSet.Enqueue(left, fScores[left.Item1, left.Item2]);
                        }
                    }
                }

                if (right.Item1 < width)
                {
                    var tentativeGScore = gScores[current.Item1, current.Item2] + risks[right.Item1, right.Item2];

                    if (tentativeGScore < gScores[right.Item1, right.Item2])
                    {
                        gScores[right.Item1, right.Item2] = tentativeGScore;
                        fScores[right.Item1, right.Item2] = tentativeGScore + h(right);

                        if (!openSet.UnorderedItems.Any(item => item.Element == right))
                        {
                            openSet.Enqueue(right, fScores[right.Item1, right.Item2]);
                        }
                    }
                }

                if (up.Item2 >= 0)
                {
                    var tentativeGScore = gScores[current.Item1, current.Item2] + risks[up.Item1, up.Item2];

                    if (tentativeGScore < gScores[up.Item1, up.Item2])
                    {
                        gScores[up.Item1, up.Item2] = tentativeGScore;
                        fScores[up.Item1, up.Item2] = tentativeGScore + h(up);

                        if (!openSet.UnorderedItems.Any(item => item.Element == up))
                        {
                            openSet.Enqueue(up, fScores[up.Item1, up.Item2]);
                        }
                    }
                }

                if (down.Item2 < height)
                {
                    var tentativeGScore = gScores[current.Item1, current.Item2] + risks[down.Item1, down.Item2];

                    if (tentativeGScore < gScores[down.Item1, down.Item2])
                    {
                        gScores[down.Item1, down.Item2] = tentativeGScore;
                        fScores[down.Item1, down.Item2] = tentativeGScore + h(down);

                        if (!openSet.UnorderedItems.Any(item => item.Element == down))
                        {
                            openSet.Enqueue(down, fScores[down.Item1, down.Item2]);
                        }
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine(leastRisk);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}