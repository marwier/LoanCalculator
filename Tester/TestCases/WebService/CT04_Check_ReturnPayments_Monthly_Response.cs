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
    class CT04_Check_ReturnPayments_Monthly_Response : ITestCase
    {
        private string _urlToTest;
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
            _urlToTest = PrepareUrl(0, 10000, 1);
            
            if (VerifyReturnPaymentsResponse(_urlToTest, HttpStatusCode.OK,
                "Checking response correct endpoint"))
            {
                using (var response = WebRequest.Create(_urlToTest).GetResponse())
                {
                    var entireContent = WebHelpers.GetJsonPageContent<List<Payment>>(response);
                    var expectedContent = GenerateListOfPaymentsBasedOnModel();

                    if (expectedContent.Count == entireContent.Count && !expectedContent.Select(x => x.Total)
                            .Except(entireContent.Select(x => x.Total)).Any())
                        TestLog.AddMessage("Received values are identical!", TestLog.LogResult.Passed);
                    else
                    {
                        TestLog.AddMessage(
                            $"Received values are not identical. Number of differences: {expectedContent.Except(entireContent).Count()}",
                            TestLog.LogResult.Failed);
                    }

                }
            }

            // Veryfing ID values
            {
                VerifyReturnPaymentsResponse(PrepareUrl(55, 10000, 1), HttpStatusCode.NotFound,
                    $"Checking response for non existing ID value - index lower than {ushort.MaxValue}");

                VerifyReturnPaymentsResponse(PrepareUrl(ushort.MaxValue + 1, 10000, 1), HttpStatusCode.BadRequest,
                    $"Checking response for bad ID value - index bigger than {ushort.MaxValue}");

                VerifyReturnPaymentsResponse(PrepareUrl(-1, 10000, 1), HttpStatusCode.BadRequest,
                    $"Checking response for bad ID value - value lower than 0");

                VerifyReturnPaymentsResponse(PrepareUrl("test", 10000, 1), HttpStatusCode.BadRequest,
                    $"Checking response for bad ID value - string value");
            }

            // Veryfing years value
            {
                VerifyReturnPaymentsResponse(PrepareUrl(0, 10000, 0), HttpStatusCode.BadRequest,
                    "Checking response for bad years value - value equals 0");

                VerifyReturnPaymentsResponse(PrepareUrl(0, 10000, ushort.MaxValue + 1), HttpStatusCode.BadRequest,
                    $"Checking response for bad years value - index bigger than {ushort.MaxValue}");

                VerifyReturnPaymentsResponse(PrepareUrl(0, 10000, -1), HttpStatusCode.BadRequest,
                    "Checking response for bad years value - value lower than 0");

                VerifyReturnPaymentsResponse(PrepareUrl(0, 10000, "test"), HttpStatusCode.BadRequest,
                    "Checking response for bad years value - string value");
            }

            // Veryfing amount value
            {
                VerifyReturnPaymentsResponse(PrepareUrl(0, 0, 1), HttpStatusCode.BadRequest,
                    "Checking response for bad loan amount value - value equals 0");

                VerifyReturnPaymentsResponse(PrepareUrl(0, -1, 1), HttpStatusCode.BadRequest,
                    "Checking response for bad loan amount value - value lower than 0");

                VerifyReturnPaymentsResponse(PrepareUrl(0, "test", 1), HttpStatusCode.BadRequest,
                    "Checking response for bad loan amount value - value equals 0");
            }
        }

        public void Finish()
        {
            IisExpressActions.CloseIisExpress();
        }

        private List<Payment> GenerateListOfPaymentsBasedOnModel()
        {
            decimal interest = 0.11M;
            int numberOfMonths = 12;
            ushort numberOfYears = 1;
            decimal totalAmount = 10000;

            var capitalizationPeriod = numberOfYears * numberOfMonths;
            var interestPerMonth = interest / capitalizationPeriod;
            var capitalPerMonth = totalAmount / capitalizationPeriod;

            var paymentList = new List<Payment>();

            for (ushort i = 0; i < capitalizationPeriod; i++)
            {
                paymentList.Add(new Payment
                {
                    PaymentId = i,
                    Capital = capitalPerMonth,
                    Interest = totalAmount * interestPerMonth
                });

                totalAmount -= capitalPerMonth;
            }

            return paymentList;
        }

        private bool VerifyReturnPaymentsResponse(string url, HttpStatusCode statusCode, string message)
        {
           
            TestLog.AddMessage(message);
            return WebHelpers.VerifyEndpoints(url, statusCode);
        }

        private string PrepareUrl(object id, object amount, object years)
        {
            return $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID={id}&TotalAmount={amount}&NumberOfYears={years}";
        }
    }
}
