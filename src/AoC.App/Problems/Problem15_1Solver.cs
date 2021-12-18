using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day15
{
    class Node
    {
        public Node((int, int) location)
        {
            Location = location;
            TentativeDistance = int.MaxValue;
        }

        public (int, int) Location { get; }
        public int TentativeDistance { get; set; }
    }

    [ProblemName("15_1")]
    public class Problem15_1Solver : BaseSolver
    {
        private static int width = 0;
        private static int height = 0;
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            width = input.First().Count();
            height = input.Count();
            var risks = new int[width, height];
            var unvisited = new List<Node>();

            int column = 0;
            int row = 0;
            foreach (var line in input)
            {
                foreach (var digit in line.Select(d => int.Parse(d.ToString())))
                {
                    var currentLocation = (column, row);
                    var node = new Node(currentLocation);
                    unvisited.Add(new Node(currentLocation));

                    risks[column, row] = digit;
                    column++;
                }
                column = 0;
                row++;
            }

            Node currentNode = unvisited.Single(n => n.Location.Item1 == 0 && n.Location.Item2 == 0);
            currentNode.TentativeDistance = 0;
            var leastRisk = int.MaxValue;

            while (unvisited.Any(n => n.Location.Item1 == (width - 1) && n.Location.Item2 == (height - 1)))
            {
                Node left = null;
                Node right = null;
                Node up = null;
                Node down = null;

                if (currentNode.Location.Item1 > 0)
                {
                    left = unvisited.SingleOrDefault(n => n.Location.Item1 == (currentNode.Location.Item1 - 1) && n.Location.Item2 == currentNode.Location.Item2);
                    if (left != null)
                    {
                        left.TentativeDistance = (currentNode.TentativeDistance + risks[left.Location.Item1, left.Location.Item2]) < left.TentativeDistance ? currentNode.TentativeDistance + risks[left.Location.Item1, left.Location.Item2] : left.TentativeDistance;
                    }
                }

                if (currentNode.Location.Item1 < (width - 1))
                {
                    right = unvisited.SingleOrDefault(n => n.Location.Item1 == (currentNode.Location.Item1 + 1) && n.Location.Item2 == currentNode.Location.Item2);
                    if (right != null)
                    {
                        right.TentativeDistance = (currentNode.TentativeDistance + risks[right.Location.Item1, right.Location.Item2]) < right.TentativeDistance ? currentNode.TentativeDistance + risks[right.Location.Item1, right.Location.Item2] : right.TentativeDistance;
                    }
                }

                if (currentNode.Location.Item2 > 0)
                {
                    up = unvisited.SingleOrDefault(n => n.Location.Item1 == currentNode.Location.Item1 && n.Location.Item2 == (currentNode.Location.Item2 - 1));
                    if (up != null)
                    {
                        up.TentativeDistance = (currentNode.TentativeDistance + risks[up.Location.Item1, up.Location.Item2]) < up.TentativeDistance ? currentNode.TentativeDistance + risks[up.Location.Item1, up.Location.Item2] : up.TentativeDistance;
                    }
                }

                if (currentNode.Location.Item2 < (height - 1))
                {
                    down = unvisited.SingleOrDefault(n => n.Location.Item1 == currentNode.Location.Item1 && n.Location.Item2 == (currentNode.Location.Item2 + 1));
                    if (down != null)
                    {
                        down.TentativeDistance = (currentNode.TentativeDistance + risks[down.Location.Item1, down.Location.Item2]) < down.TentativeDistance ? currentNode.TentativeDistance + risks[down.Location.Item1, down.Location.Item2] : down.TentativeDistance;
                    }
                }

                unvisited.Remove(currentNode);
                Node smallest = unvisited.OrderBy(n => n.TentativeDistance).First();
                // int min = unvisited.Min(n => n.TentativeDistance);
                // Node smallest = unvisited.First(n => n.TentativeDistance == min);
                currentNode = smallest;

                if (currentNode.Location.Item1 == (width - 1) && currentNode.Location.Item2 == (height - 1))
                {
                    leastRisk = currentNode.TentativeDistance;
                    break;
                }
            }

            stopwatch.Stop();
            Console.WriteLine(leastRisk);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}