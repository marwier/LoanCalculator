using System.Net;
using Tester.Core;
using Tester.Tools.IISExpress;
using Tester.Tools.Logs;
using Tester.Tools.WebHelpers;

namespace Tester.TestCases
{
    public class T01_Check_Endpoints : ITestCase
    {
        private const string ServerAddress = @"http://localhost:55735";

        public void Prepare()
        {
            if (!IisExpressActions.CheckIfIisExpressIsRunning())
                IisExpressActions.StartIisExpress();
            else
                IisExpressActions.GetRunningProcess();
        }

        public void Run()
        {
            var urlToTest = $"{ServerAddress}";
            TestLog.AddMessage("Checking response: server address and no endpoints");
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.Forbidden);

            urlToTest = $"{ServerAddress}/IncorrectEndpoint";
            TestLog.AddMessage("Checking response: server address and not existing endpoint");
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.NotFound);

            urlToTest = $"{ServerAddress}/Website/LoanCalculatorOnline.html";
            TestLog.AddMessage("Checking response: server address and existing endpoint");
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.OK);
        }

        public void Finish()
        {
            IisExpressActions.CloseIisExpress();
        }
    }
}
