using System;

namespace Maps.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            CSharp7_TestOutVariables(out int x, out int y);

            Console.WriteLine($"CSharp7_TestOutVariables {x} {y}");
        }

        private static void CSharp7_TestOutVariables(out int x, out int y)
        {
            x = 1;
            y = 2;
        }
    }
}
