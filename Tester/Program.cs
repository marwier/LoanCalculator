using Tester.TestCases.Core;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTemplate.ExecuteTest(args[0]);
            //TestTemplate.ExecuteTest(new T01_Check_Endpoints());
        }


        //private static ITestCase BeginTests(string name)
        //{
        //    var assembly = Assembly.GetExecutingAssembly();

        //    try
        //    {
        //        LogMessage($"Trying to load {name} testcase");

        //        var type = assembly.GetTypes().First(t => t.Name == name);

        //        var test = (ITestCase)Activator.CreateInstance(type);
        //        LogMessage($"Test case loaded");
        //        return test;
        //    }
        //    catch (Exception e)
        //    {
        //        LogMessage($"Failed to load test {name}! {e.ToString()}", LogLevel.EXCEPTION);
        //        return null;
        //    }
        //}
    }
}
