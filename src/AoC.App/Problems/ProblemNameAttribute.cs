namespace AoC.App.Problems
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ProblemNameAttribute : System.Attribute
    {
        readonly string problemName;
        
        // This is a positional argument
        public ProblemNameAttribute(string problemName)
        {
            this.problemName = problemName;
        }
        
        public string ProblemName
        {
            get { return problemName; }
        }
    }
}