using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day14
{
    [ProblemName("14_1")]
    public class Problem14_1Solver : BaseSolver
    {
        private static Dictionary<(char, char), char> Rules;
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            Rules = new Dictionary<(char, char), char>();

            var template = input.Take(1).Single().Aggregate<char, (IEnumerable<(char, char)>, char?)>((new List<(char, char)>(), null), (state, character) => {
                if (state.Item2.HasValue)
                {
                    return (state.Item1.Append((state.Item2.Value, character)), character);
                }
                else
                {
                    return (state.Item1, character);
                }
            }).Item1;

            foreach (var line in input.Skip(2))
            {
                var parts = line.Split(" -> ");
                var chars = (parts[0].Take(1).Single(), parts[0].Skip(1).Single());

                Rules[chars] = parts[1].Single();
            }

            IEnumerable<(char, char)> result = template;

            foreach (var i in Enumerable.Range(0, 10))
            {
                result = InsertPairs(result);
            }

            var finalString = result.Any() ? result.First().Item1.ToString() + string.Join("", result.Select(p => p.Item2.ToString())) : "";
            var charCounts = finalString.Aggregate(new Dictionary<char, long>(), (state, character) => {
                if (state.ContainsKey(character))
                {
                    state[character] = state[character] + 1;
                }
                else
                {
                    state[character] = 1;
                }

                return state;
            });

            char highestCount = charCounts.Keys.First();
            char leastCount = charCounts.Keys.First();

            foreach (var key in charCounts.Keys)
            {
                if (charCounts[key] > charCounts[highestCount])
                {
                    highestCount = key;
                }

                if (charCounts[key] < charCounts[leastCount])
                {
                    leastCount = key;
                }
            }

            var difference = charCounts[highestCount] - charCounts[leastCount];

            stopwatch.Stop();
            Console.WriteLine(difference);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private IEnumerable<(char, char)> InsertPairs(IEnumerable<(char, char)> pairs)
        {
            Func<(char, char), IEnumerable<(char, char)>> insertionFunc = pair => {
                 char insertedChar = Rules[pair];

                 return new List<(char, char)>() { (pair.Item1, insertedChar), (insertedChar, pair.Item2) };
            };
            return pairs.SelectMany(insertionFunc);
        }
    }
}