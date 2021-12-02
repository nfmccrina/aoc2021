using System;
using System.IO;
using System.Linq;

namespace Problem1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var depths = File.ReadAllLines("data.txt").Select(depth => int.Parse(depth));
            var increaseCount = 0;
            int? previousDepth = null;

            foreach (var depth in depths)
            {
                if (previousDepth != null && depth > previousDepth)
                {
                    increaseCount++;
                }

                previousDepth = depth;
            }

            Console.WriteLine($"Depth increased {increaseCount} times.");
        }
    }
}
