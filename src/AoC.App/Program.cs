using System;
using System.Linq;
using AoC.App.Problems;

namespace AoC.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No problem specified.");
                return;
            }

            Type solverType = typeof(Program).Assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(ISolver)))
                .Where(type => type.CustomAttributes.Any(t => t.AttributeType == typeof(ProblemNameAttribute)))
                .Where(type => (string)type.CustomAttributes.Single(attribute => attribute.AttributeType == typeof(ProblemNameAttribute)).ConstructorArguments.Single().Value == args[0])
                .Single();

            ISolver solver = (ISolver)Activator.CreateInstance(solverType);

            solver.Solve();
        }
    }
}
