using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day12
{
    class Cave
    {
        public Cave(string name)
        {
            Name = name;
            _neighbors = new List<Cave>();
        }

        public void AddNeighbor(Cave cave)
        {
            _neighbors.Add(cave);
        }

        public IEnumerable<Cave> Neighbors
        {
            get
            {
                return _neighbors;
            }
        }

        public string Name { get; }

        private List<Cave> _neighbors;
    }

    [ProblemName("12_1")]
    public class Problem12_1Solver : BaseSolver
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

            var pathCount = FindPaths(caves.Where(c => c.Name == "start").Single(), null, new List<string>()).Count();
            stopwatch.Stop();

            Console.WriteLine(pathCount);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private IEnumerable<string> FindPaths(Cave start, string path, List<string> visited)
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
                .Where(cave => !cave.Name.Any(c => char.IsLower(c)) || !visited.Contains(cave.Name)).ToList();
            
            var paths = possibleNeighbors.SelectMany(cave => FindPaths(cave, path, visited.Select(c => c).ToList()));
            return paths;
        }
    }
}