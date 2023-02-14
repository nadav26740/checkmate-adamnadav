using System;

namespace Tests
{
    class Program
    {
        struct tests
        {
            string test;
        }

        static void Main(string[] args)
        {
            tests test;
            test.test = "Testing";
            Console.WriteLine(test.test);
        }
    }
}
