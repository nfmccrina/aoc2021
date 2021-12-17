using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day14
{

    [ProblemName("14_2")]
    public class Problem14_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            Dictionary<(char, char), long> pairCounts = new Dictionary<(char, char), long>();
            Dictionary<(char, char), char> rules = new Dictionary<(char, char), char>();
            Dictionary<char, long> finalCounts = new Dictionary<char, long>();
            char firstCharacter = input.First().First();

            foreach (var pair in input.Take(1).Single().Aggregate<char, (IEnumerable<(char, char)>, char?)>((new List<(char, char)>(), null), (state, character) => {
                if (state.Item2.HasValue)
                {
                    return (state.Item1.Append((state.Item2.Value, character)), character);
                }
                else
                {
                    return (state.Item1, character);
                }
            }).Item1)
            {
                pairCounts[pair] = pairCounts.ContainsKey(pair) ? pairCounts[pair] + 1 : 1;
            }

            foreach (var line in input.Skip(2))
            {
                var parts = line.Split(" -> ");
                var chars = (parts[0].Take(1).Single(), parts[0].Skip(1).Single());

                rules[chars] = parts[1].Single();
            }

            for (var step = 0; step < 40; step++)
            {
                var tmpPairCounts = new Dictionary<(char, char), long>();
                foreach (var key in pairCounts.Keys)
                {
                    var count = pairCounts[key];
                    var insertedChar = rules[key];
                    tmpPairCounts[(key.Item1, insertedChar)] = tmpPairCounts.ContainsKey((key.Item1, insertedChar)) ? tmpPairCounts[(key.Item1, insertedChar)] + pairCounts[key] : pairCounts[key];
                    tmpPairCounts[(insertedChar, key.Item2)] = tmpPairCounts.ContainsKey((insertedChar, key.Item2)) ? tmpPairCounts[(insertedChar, key.Item2)] + pairCounts[key] : pairCounts[key];
                }

                pairCounts = tmpPairCounts;
            }

            foreach (var pair in pairCounts)
            {
                finalCounts[pair.Key.Item2] = finalCounts.ContainsKey(pair.Key.Item2) ? finalCounts[pair.Key.Item2] + pair.Value : pair.Value;
            }

            finalCounts[firstCharacter] = finalCounts.ContainsKey(firstCharacter) ? finalCounts[firstCharacter] + 1 : 1;
            long highestCount = 0;
            long lowestCount = long.MaxValue;

            foreach (var count in finalCounts.Values)
            {
                if (count > highestCount)
                {
                    highestCount = count;
                }

                if (count < lowestCount)
                {
                    lowestCount = count;
                }
            }

            stopwatch.Stop();
            Console.WriteLine(highestCount - lowestCount);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}