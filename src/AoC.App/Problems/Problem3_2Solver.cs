using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    public enum RatingType
    {
        O2Generator,
        CO2Scrubber
    }

    [ProblemName("3_2")]
    public class Problem3_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var data = GetData();
            var o2GeneratorRating = GetRating(data, RatingType.O2Generator, 0);
            var co2ScrubberRating = GetRating(data, RatingType.CO2Scrubber, 0);

            Console.WriteLine(o2GeneratorRating * co2ScrubberRating);
        }

        private int GetRating(IEnumerable<string> data, RatingType ratingType, int index)
        {
            var onesCount = data.Aggregate(0, (count, currentValue) => count + (currentValue[index] == '1' ? 1 : 0));
            var zeroesCount = data.Aggregate(0, (count, currentValue) => count + (currentValue[index] == '1' ? 0 : 1));

            var criteria = '0';

            if (ratingType == RatingType.O2Generator)
            {
                criteria = onesCount >= zeroesCount ? '1' : '0';
            }
            else
            {
                criteria = onesCount >= zeroesCount ? '0' : '1';
            }
            var filteredInputs = data.Where(item => item[index] == criteria);

            if (filteredInputs.Count() == 1)
            {
                return Convert.ToInt32(filteredInputs.Single(), 2);
            }
            else
            {
                return GetRating(filteredInputs, ratingType, index + 1);
            }
        }
    }
}