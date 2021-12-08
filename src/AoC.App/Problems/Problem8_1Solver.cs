using System;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("8_1")]
    public class Problem8_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var count = input.Aggregate(0, (state, line) => state + line
                .Split('|')[1]
                .Trim()
                .Split(' ')
                .Where(digit =>
                    digit.Count() == 2 ||
                    digit.Count() == 4 ||
                    digit.Count() == 3 ||
                    digit.Count() == 7
                )
                .Count()
            );

            Console.WriteLine(count);
        }
    }
}