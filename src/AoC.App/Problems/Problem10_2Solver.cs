using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("10_2")]
    public class Problem10_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();
            Dictionary<char, int> points = new Dictionary<char, int>()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };

            Dictionary<char, char> matches = new Dictionary<char, char>()
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' },
                { '>', '<' }
            };

            Dictionary<char, char> reverseMatches = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            IEnumerable<char> openingCharacters = new List<char>() { '(', '[', '{', '<' };

            var lineScores = input.Select(currentLine => {
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
                            return -1;
                        }
                    }
                }

                long score = 0;

                while (instructionStack.Any())
                {
                    char openingCharacter = instructionStack.Pop();

                    score *= 5;
                    score += points[reverseMatches[openingCharacter]];
                }

                return score;
            });

            lineScores = lineScores.Where(score => score != -1);

            var result = lineScores.OrderByDescending(score => score).ElementAt(lineScores.Count() / 2);

            stopwatch.Stop();

            Console.WriteLine(result);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}