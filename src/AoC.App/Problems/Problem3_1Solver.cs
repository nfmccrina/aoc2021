using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("3_1")]
    public class Problem3_1Solver : BaseSolver
    {
        private const int INPUT_SIZE = 12;
        public override void Solve()
        {
            var data = GetData();
            var solution = data.Aggregate<string, int[], int>(
                new int[INPUT_SIZE * 2],
                aggregateFunction,
                resultSelector);

            Console.WriteLine(solution);
        }

        private Func<int[], string, int[]> aggregateFunction = (state, currentValue) => {
            var updatedState = new int[INPUT_SIZE * 2];

            for (var i = 0; i < INPUT_SIZE; i++)
            {
                updatedState[i] = state[i] + (currentValue[i] == '1' ? 1 : -1);
                updatedState[i + INPUT_SIZE] = state[i + INPUT_SIZE] + (currentValue[i] == '1' ? -1 : 1);
            }

            return updatedState;
        };

        private Func<int[], int> resultSelector = (state) => {
            var gammaRate = 0;
            var epsilonRate = 0;

            for (var i = 0; i < INPUT_SIZE; i++)
            {
                gammaRate = gammaRate | (state[i] > 0 ? 1 << INPUT_SIZE - 1 - i : 0);
                epsilonRate = epsilonRate | (state[i] > 0 ? 0 : 1 << INPUT_SIZE - 1 - i);
            }

            return gammaRate * epsilonRate;
        };
    }
}