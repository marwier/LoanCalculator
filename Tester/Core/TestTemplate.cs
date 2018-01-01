using System;
using System.Linq;
using System.Reflection;
using Tester.Tools.Logs;

namespace Tester.Core
{
    public static class TestTemplate
    {
        private const string TestLogHistoryPath = @"C:\TestingHistory";

        public static void ExecuteTest(string testCaseName, string scenarioName = "")
        {
            if (!string.IsNullOrEmpty(scenarioName))
                scenarioName = "\\" + scenarioName;

            var testCase = LoadTestCase(testCaseName);

            if (testCase == null)
            {
                TestLog.AddMessage("Saving error log...", TestLog.LogResult.System);
                TestLog.SaveLog(
                    $@"{TestLogHistoryPath}{scenarioName}\_Errors\{DateTime.Now:hhmmsstt_ddMMyyyy}_TestLog.xml");
                Environment.Exit(1);
            }

            try
            {
                TestLog.AddMessage("Preparing test case components...", TestLog.LogResult.System);
                testCase.Prepare();

                TestLog.AddMessage("Finished preparation. Running test case...", TestLog.LogResult.System);
                testCase.Run();
                TestLog.AddMessage("Finished test case run. Cleaning execution", TestLog.LogResult.System);

                testCase.Finish();
                TestLog.AddMessage("Finished cleaning.", TestLog.LogResult.System);
            }
            catch (Exception ex)
            {
                TestLog.AddMessage($"Unhandled exception occured.\n{ex}",
                    TestLog.LogResult.Exception);
            }
            finally
            {
                TestLog.AddMessage("Finishing execution and saving logs...", TestLog.LogResult.System);
                TestLog.SaveLog(
                    $@"{TestLogHistoryPath}{scenarioName}\{testCase.GetType().Name}\{DateTime.Now:hhmmss_ddMMyyyy}_TestLog.xml");
            }
        }

        private static ITestCase LoadTestCase(string testName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                TestLog.AddMessage("Provided empty test case name. Please verify arguments.",
                    TestLog.LogResult.Blocked);
                return null;
            }

            var possibleTestCases = Assembly.GetExecutingAssembly().GetTypes().
                Where(x => x.GetInterfaces().Any(y => y == typeof(ITestCase)));

            try
            {
                TestLog.AddMessage($"Starting test case: \"{testName}\"", TestLog.LogResult.System);
                return (ITestCase)Activator.CreateInstance(possibleTestCases.Single(x => x.Name == testName));
            }
            catch (InvalidOperationException)
            {
                TestLog.AddMessage("Could not find selected test case. Please verify arguments.",
                    TestLog.LogResult.Blocked);
                return null;
            }
        }
    }
}
