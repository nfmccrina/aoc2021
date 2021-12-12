using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day12
{
    [ProblemName("12_2")]
    public class Problem12_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            List<Cave> caves = new List<Cave>();

            foreach (var line in input)
            {
                var names = line.Split('-');

                if (!caves.Any(c => c.Name == names[0]))
                {
                    caves.Add(new Cave(names[0]));
                }

                if (!caves.Any(c => c.Name == names[1]))
                {
                    caves.Add(new Cave(names[1]));
                }

                caves.Where(c => c.Name == names[0]).Single().AddNeighbor(caves.Where(c => c.Name == names[1]).Single());
                caves.Where(c => c.Name == names[1]).Single().AddNeighbor(caves.Where(c => c.Name == names[0]).Single());
            }

            var pathCount = FindPaths(caves.Where(c => c.Name == "start").Single(), null, new List<string>(), false).Count();
            stopwatch.Stop();

            Console.WriteLine(pathCount);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private IEnumerable<string> FindPaths(Cave start, string path, List<string> visited, bool doubleVisitUsed)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = start.Name;
            }
            else
            {
                path += $"-{start.Name}";
            }

            if (start.Name == "end")
            {
                return new List<string>() { path };
            }

            visited.Add(start.Name);
            var possibleNeighbors = start.Neighbors
                .Where(cave => cave.Name != "start" && (!doubleVisitUsed || !cave.Name.Any(c => char.IsLower(c)) || !visited.Contains(cave.Name))).ToList();
            
            var paths = possibleNeighbors.SelectMany(cave => FindPaths(cave, path, visited.Select(c => c).ToList(), doubleVisitUsed ? true : (cave.Name.Any(c => char.IsLower(c) && visited.Select(c => c).Contains(cave.Name)))));
            return paths;
        }
    }
}