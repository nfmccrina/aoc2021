using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("6_2")]
    public class Problem6_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var generations = 256;
            var input = GetData();
            var fish = input.Single().Split(',').Select(number => int.Parse(number));

            Dictionary<int, long> fishCounts = new Dictionary<int, long>();

            foreach (var i in Enumerable.Range(0, 9))
            {
                fishCounts[i] = fish.Count(f => f == i);
            }

            foreach (var generation in Enumerable.Range(0, generations))
            {
                long dueFishCount = fishCounts[0];

                for (var i = 1; i <= 8; i++)
                {
                    fishCounts[i - 1] = fishCounts[i];
                }

                fishCounts[6] = fishCounts[6] + dueFishCount;
                fishCounts[8] = dueFishCount;
            }

            Console.WriteLine(fishCounts.Values.Sum());
        }
    }
}