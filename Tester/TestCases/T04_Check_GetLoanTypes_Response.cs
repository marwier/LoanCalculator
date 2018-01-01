using System.Collections.Generic;
using System.Linq;
using System.Net;
using CommonModels;
using Tester.Core;
using Tester.Tools.IISExpress;
using Tester.Tools.Logs;
using Tester.Tools.WebHelpers;

namespace Tester.TestCases
{
    class T04_Check_GetLoanTypes_Response : ITestCase
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
            TestLog.AddMessage("Checking response correct endpoint");
            var urlToTest = $"{ServerAddress}/api/Loan/GetLoanTypes";

            if (WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.OK))
            {
                using (var response = WebRequest.Create(urlToTest).GetResponse())
                {
                    var entireContent = WebHelpers.GetJsonPageContent<List<LoanType>>(response);
                    var firstRecord = entireContent.First();

                    var expectedNumberOfElements = 8;
                    var expectedId = 0;
                    var expectedText = "Coffee express";

                    if (entireContent.Count == expectedNumberOfElements)
                        TestLog.AddMessage(
                            $"Received number of elements: {entireContent.Count} is equal to expected: {expectedNumberOfElements}",
                            TestLog.LogResult.Passed);
                    else
                        TestLog.AddMessage(
                            $"Received number of elements is: {entireContent.Count}, but expected is: {expectedNumberOfElements}",
                            TestLog.LogResult.Failed);

                    if (expectedText == firstRecord.LoanText && expectedId == firstRecord.LoanTypeId)
                        TestLog.AddMessage(
                            $"Received values: {firstRecord.LoanText} & {firstRecord.LoanTypeId} are equal to expected value: {expectedText} & {expectedId}",
                            TestLog.LogResult.Passed);
                    else
                        TestLog.AddMessage(
                            $"Received value are: {firstRecord.LoanText} & {firstRecord.LoanTypeId}, but expected are: : {expectedText} & {expectedId}",
                            TestLog.LogResult.Failed);
                }
            }
        }

        public void Finish()
        {
            IisExpressActions.CloseIisExpress();
        }
    }
}
