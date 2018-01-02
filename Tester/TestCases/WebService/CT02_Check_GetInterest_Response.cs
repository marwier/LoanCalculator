using System.Net;
using Tester.Core;
using Tester.Tools.IISExpress;
using Tester.Tools.Logs;
using Tester.Tools.WebHelpers;

namespace Tester.TestCases
{
    public class CT02_Check_GetInterest_Response : ITestCase
    {
        private const string ServerAddress = @"http://localhost:55735";
        private readonly string _urlToTest = $"{ServerAddress}/api/Loan/GetInterest?LoanTypeID=";

        public void Prepare()
        {
            if (!IisExpressActions.CheckIfIisExpressIsRunning())
                IisExpressActions.StartIisExpress();
            else
                IisExpressActions.GetRunningProcess();
        }

        public void Run()
        {
            var selectedID = 0;

            if (VerifyGetInterestResponse(selectedID, HttpStatusCode.OK,
                "Checking response for existing value - first record"))
            {
                using (var response = WebRequest.Create(_urlToTest + selectedID).GetResponse())
                {
                    var content = WebHelpers.GetJsonPageContent<decimal>(response);
                    var expectedValue = 0.11M;

                    if (expectedValue == content)
                        TestLog.AddMessage($"Received value: {content} is equal to expected value: {expectedValue}",
                            TestLog.LogResult.Passed);
                    else
                        TestLog.AddMessage($"Received value is: {content}, but expected is: {expectedValue}",
                            TestLog.LogResult.Failed);
                }
            }

            VerifyGetInterestResponse(55, HttpStatusCode.NotFound,
                $"Checking response for non existing value - index lower than {ushort.MaxValue}");

            VerifyGetInterestResponse(ushort.MaxValue + 1, HttpStatusCode.BadRequest,
                $"Checking response for bad id value - index bigger than {ushort.MaxValue}");

            VerifyGetInterestResponse(-1, HttpStatusCode.BadRequest,
                $"Checking response for bad id value - value lower than {0}");

            VerifyGetInterestResponse("test", HttpStatusCode.BadRequest,
                "Checking response for bad id value - string value");
        }

        public void Finish()
        {
            IisExpressActions.CloseIisExpress();
        }

        private bool VerifyGetInterestResponse(object value, HttpStatusCode statusCode, string message)
        {
            TestLog.AddMessage(message);
            return WebHelpers.VerifyEndpoints(_urlToTest + value.ToString(), statusCode);
        }
    }
}
