using System.Collections.Generic;
using System.IO;

namespace AoC.App.Problems
{
    public abstract class BaseSolver : ISolver
    {
        public virtual IEnumerable<string> GetData()
        {
            return File.ReadAllLines("data.txt");
        }

        public abstract void Solve();
    }
}