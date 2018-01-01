using System;
using System.Linq;
using Tester.Core;

namespace Tester
{
    internal static class Tester
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                var groupName = args.SingleOrDefault(x => x.StartsWith("g="))?.Substring(2);
                var testCasesNames = args.Where(x => x.StartsWith("n="));

                foreach (var testCase in testCasesNames)
                {
                    TestTemplate.ExecuteTest(testCase.Substring(2), groupName);
                }
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
