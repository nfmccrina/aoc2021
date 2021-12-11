using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("10_1")]
    public class Problem10_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            Dictionary<char, int> points = new Dictionary<char, int>()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };

            Dictionary<char, char> matches = new Dictionary<char, char>()
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' },
                { '>', '<' }
            };

            IEnumerable<char> openingCharacters = new List<char>() { '(', '[', '{', '<' };

            var result = input.Aggregate(0, (score, currentLine) => {
                Stack<char> instructionStack = new Stack<char>();
                foreach (char c in currentLine)
                {
                    if (openingCharacters.Contains(c))
                    {
                        instructionStack.Push(c);
                    }
                    else
                    {
                        char match = instructionStack.Pop();
                        if (matches[c] != match)
                        {
                            return score + points[c];
                        }
                    }
                }

                return score;
            });

            stopwatch.Stop();

            Console.WriteLine(result);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}