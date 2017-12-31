using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tester.TestCases;
using Tester.TestCases.Core;
using Tester.Tools.Logs;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTemplate.ExecuteTest(args[0]);
            //TestTemplate.ExecuteTest(new T01_Check_GetLoanTypes_Request());
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
