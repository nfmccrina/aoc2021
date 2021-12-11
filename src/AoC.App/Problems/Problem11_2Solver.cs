using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day11
{
    [ProblemName("11_2")]
    public class Problem11_2Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();

            List<Octopus> octopuses = new List<Octopus>();
            var publisher = new Publisher();
            var row = 0;
            foreach (var line in input)
            {
                var column = 0;
                foreach (var energyLevel in line.Select(c => int.Parse(c.ToString())))
                {
                    var octopusObject = new Octopus((row, column), energyLevel, publisher);
                    octopuses.Add(octopusObject);
                    publisher.AddSubscriber(MessageType.INCREASE_ENERGY, ref octopusObject);
                    publisher.AddSubscriber(MessageType.OCTOPUS_FLASHED, ref octopusObject);
                    publisher.AddSubscriber(MessageType.RESET_ENERGY, ref octopusObject);
                    column++;
                }
                row++;
            }

            var result = 1;
            while (true)
            {
                publisher.SendMessage(new IncreaseEnergyMessage());
                publisher.SendMessage(new ResetEnergyMessage());
                if (octopuses.Select(o => int.Parse(o.ToString())).Sum() == 0)
                {
                    break;
                }
                else
                {
                    result++;
                }
            }

            stopwatch.Stop();

            Console.WriteLine(result);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private string PrintOctopusEnergy(IEnumerable<Octopus> octopuses)
        {
            var position = 0;
            string result = "";
            foreach (var octopus in octopuses)
            {
                result += octopus.ToString();
                if ((position % 10) == 9)
                {
                    result += '\n';
                }

                position++;
            }

            return result;
        }
    }
}