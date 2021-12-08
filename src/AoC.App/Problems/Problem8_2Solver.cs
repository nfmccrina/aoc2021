using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.App.Problems
{
    [ProblemName("8_2")]
    public class Problem8_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var displays = input.Select(line => {
               var parts = line.Split('|');

               return (parts[0].Trim().Split(' '), parts[1].Trim().Split(' ')); 
            });

            var count = displays.Aggregate(0, (state, display) => {
                var segmentMap = MapSegments(display.Item1);
                var displayValue = string.Join("", display.Item2.Select(digit => ReadDigit(segmentMap, digit)));
                return state + int.Parse(displayValue);
            });

            Console.WriteLine(count);
        }

        private char[] MapSegments(IEnumerable<string> signals)
        {
            char[] map = new char[7];

            var oneSignal = signals.Where(signal => signal.Length == 2).Single();
            var sixSignal = signals.Where(signal => signal.Length == 6 && signal.Intersect(oneSignal).Count() == 1).Single();

            map[5] = sixSignal.Intersect(oneSignal).Single();
            map[2] = oneSignal.Where(c => c != map[5]).Single();

            var sevenSignal = signals.Where(signal => signal.Length == 3).Single();
            map[0] = sevenSignal.Except(oneSignal).Single();

            var fourSignal = signals.Where(signal => signal.Length == 4).Single();
            var nineSignal = signals.Where(signal => signal.Length == 6 && signal.Intersect(fourSignal).Count() == 4 && signal.Intersect(oneSignal).Count() == 2).Single();
            var zeroSignal = signals.Where(signal => signal.Length == 6 && signal.Intersect(fourSignal).Count() == 3 && signal.Intersect(oneSignal).Count() == 2).Single();
            map[3] = fourSignal.Except(zeroSignal).Single();
            map[1] = fourSignal.Except(sevenSignal.Append(map[3])).Single();
            map[4] = sixSignal.Except(nineSignal).Single();
            map[6] = zeroSignal.Except(sevenSignal.Append(map[1]).Append(map[4])).Single();

            return map;
        }

        private string ReadDigit(char[] segmentMap, string digit)
        {
            if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[1]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[4]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 6
            )
            {
                return "0";
            }
            else if (
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Length == 2
            )
            {
                return "1";
            }
            else if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[4]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 5
            )
            {
                return "2";
            }
            else if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 5
            )
            {
                return "3";
            }
            if (
                digit.Contains(segmentMap[1]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Length == 4
            )
            {
                return "4";
            }
            if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[1]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 5
            )
            {
                return "5";
            }
            if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[1]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[4]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 6
            )
            {
                return "6";
            }
            if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Length == 3
            )
            {
                return "7";
            }
            if (
                digit.Contains(segmentMap[0]) &&
                digit.Contains(segmentMap[1]) &&
                digit.Contains(segmentMap[2]) &&
                digit.Contains(segmentMap[3]) &&
                digit.Contains(segmentMap[4]) &&
                digit.Contains(segmentMap[5]) &&
                digit.Contains(segmentMap[6]) &&
                digit.Length == 7
            )
            {
                return "8";
            }
            else
            {
                return "9";
            }
        }
    }
}