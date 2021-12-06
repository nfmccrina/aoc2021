using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("5_1")]
    public class Problem5_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var coordinates = input
                .Select(line => ParseCoordinates(line))
                .SelectMany(line => BuildLine(line.Item1, line.Item2));
            var answer = coordinates.GroupBy(coordinate => coordinate).Count(group => group.Count() > 1);

            Console.WriteLine(answer);
        }

        private ((int, int), (int, int)) ParseCoordinates(string line)
        {
            var parts = line.Split(" -> ");
            return (ParseCoordinate(parts[0]), ParseCoordinate(parts[1]));
        }

        private (int, int) ParseCoordinate(string coordinateString)
        {
            var parts = coordinateString.Split(',');
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        private IEnumerable<(int, int)> BuildLine((int, int) start, (int, int) end)
        {
            var coordinates = new List<(int, int)>();
            if (start.Item1 != end.Item1 && start.Item2 != end.Item2)
            {
                return coordinates;
            }

            var xIncrement = end.Item1 - start.Item1 == 0 ? 0 : (end.Item1 - start.Item1) / Math.Abs(end.Item1 - start.Item1);
            var yIncrement = end.Item2 - start.Item2 == 0 ? 0 : (end.Item2 - start.Item2) / Math.Abs(end.Item2 - start.Item2);
            var x = start.Item1;
            var y = start.Item2;
            
            while (x != end.Item1 || y != end.Item2)
            {
                coordinates.Add((x, y));

                x += xIncrement;
                y += yIncrement;
            }

            coordinates.Add(end);

            return coordinates;
        }
    }
}