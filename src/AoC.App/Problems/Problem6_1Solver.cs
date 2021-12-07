using System;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("6_1")]
    public class Problem6_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var fish = input.Single().Split(',').Select(number => int.Parse(number));

            foreach (var i in Enumerable.Range(0, 80))
            {
                var zeroCount = fish.Count(f => f == 0);
                fish = fish.Select(f => f == 0 ? 6 : f - 1).Concat(Enumerable.Range(0, zeroCount).Select(i => 8));
            }

            Console.WriteLine(fish.Count());
        }
    }
}