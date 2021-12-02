using System;
using System.IO;
using System.Linq;

namespace Problem1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var depths = File.ReadAllLines("data.txt").Select(depth => int.Parse(depth));
            var depthGroups = depths.Skip(2).Zip(depths.Skip(1).Zip(depths, (second, first) => (Item1: first, Item2: second)), (third, firstAndSecond) => (firstAndSecond.Item1, firstAndSecond.Item2, Item3: third));
            var depthGroupSums = depthGroups.Select(group => group.Item1 + group.Item2 + group.Item3);
            var depthIncreases = depthGroupSums.Aggregate<int, (int, int?), int>((0, null), (accumulator, value) => (accumulator.Item2 != null && value > accumulator.Item2 ? accumulator.Item1 + 1 : accumulator.Item1, value), (accumulator)  => accumulator.Item1);
            Console.WriteLine(depthIncreases);
        }
    }
}
