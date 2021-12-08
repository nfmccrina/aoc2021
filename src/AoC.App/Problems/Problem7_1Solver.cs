using System;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("7_1")]
    public class Problem7_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var crabSubs = input.Single().Split(',').Select(number => int.Parse(number));
            var lowestPosition = crabSubs.Min();
            var highestPosition = crabSubs.Max();

            var leastFuel = int.MaxValue;
            for (var i = lowestPosition; i < highestPosition; i++)
            {
                var fuelCost = crabSubs.Aggregate(0, (fuel, sub) => fuel + Math.Abs(i - sub));

                if (fuelCost < leastFuel)
                {
                    leastFuel = fuelCost;
                }
            }

            Console.WriteLine(leastFuel);
        }
    }
}