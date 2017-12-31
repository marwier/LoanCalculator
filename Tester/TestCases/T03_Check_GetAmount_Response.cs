using System.Net;
using Tester.Core;
using Tester.Tools.IISExpress;
using Tester.Tools.Logs;
using Tester.Tools.WebHelpers;

namespace Tester.TestCases
{
    public class T03_Check_GetAmount_Response : ITestCase
    {
        private const string ServerAddress = @"http://localhost:55735";

        public void Prepare()
        {
            if (!IisExpressActions.CheckIfIisExpressIsRunning())
                IisExpressActions.StartIisExpress();
        }

        public void Run()
        {
            TestLog.AddMessage("Checking response for existing value - first record");
            var urlToTest = $"{ServerAddress}/api/Loan/GetAmount?LoanTypeID=0";

            if (WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.OK))
            {
                using (var response = WebRequest.Create(urlToTest).GetResponse())
                {
                    var content = WebHelpers.GetJsonPageContent<decimal>(response);
                    var expectedValue = 4000M;

                    if (expectedValue == content)
                        TestLog.AddMessage($"Received value: {content} is equal to expected value: {expectedValue}",
                            TestLog.LogResult.Passed);
                    else
                        TestLog.AddMessage($"Received value is: {content}, but expected is: {expectedValue}",
                            TestLog.LogResult.Failed);
                }
            }

            TestLog.AddMessage($"Checking response for non existing value - index lower than {ushort.MaxValue}");
            urlToTest = $"{ServerAddress}/api/Loan/GetAmount?LoanTypeID=55";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.NotFound);


            TestLog.AddMessage($"Checking response for non existing value - index bigger than {ushort.MaxValue}");
            urlToTest = $"{ServerAddress}/api/Loan/GetAmount?LoanTypeID={ushort.MaxValue + 1}";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad id value - value lower than 0");
            urlToTest = $"{ServerAddress}/api/Loan/GetAmount?LoanTypeID={-1}";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad id value - char value");
            urlToTest = $"{ServerAddress}/api/Loan/GetAmount?LoanTypeID=test";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);
        }

        public void Finish()
        {
            IisExpressActions.CloseIisExpress();
        }
    }
}
