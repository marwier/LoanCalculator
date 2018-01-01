using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using Tester.Core;
using Tester.Tools.IISExpress;
using Tester.Tools.Logs;
using Tester.Tools.WebHelpers;

namespace Tester.TestCases
{
    class T05_Check_ReturnPayments_Monthly_Response : ITestCase
    {
        private const string ServerAddress = @"http://localhost:55735";

        public void Prepare()
        {
            if (!IisExpressActions.CheckIfIisExpressIsRunning())
                IisExpressActions.StartIisExpress();
        }

        public void Run()
        {
            TestLog.AddMessage("Checking response correct endpoint");
            var urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=0&NumberOfYears=1";

            if (WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.OK))
            {
                using (var response = WebRequest.Create(urlToTest).GetResponse())
                {
                    var entireContent = WebHelpers.GetJsonPageContent<List<Payment>>(response);
                    var expectedContent = GenerateListOfPaymentsBasedOnModel();

                    if ((expectedContent.Count == entireContent.Count) && !(expectedContent.Select(x => x.Total).Except(entireContent.Select((x => x.Total)))).Any())
                        TestLog.AddMessage($"Received values are identical!", TestLog.LogResult.Passed);
                    else
                    {
                        TestLog.AddMessage($"Received values are not identical. Number of differences: {expectedContent.Except(entireContent).Count()}", TestLog.LogResult.Failed);
                    }

                }
            }

            // Veryfing ID values
            TestLog.AddMessage($"Checking response for non existing ID value - index lower than {ushort.MaxValue}");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=55&NumberOfYears=1";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.NotFound);


            TestLog.AddMessage($"Checking response for non existing ID value - index bigger than {ushort.MaxValue}");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID={ushort.MaxValue + 1}&NumberOfYears=1";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad ID value - value lower than 0");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=-1&NumberOfYears=1";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad ID value - char value");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=test&NumberOfYears=1";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);

            // Veryfing years value
            TestLog.AddMessage($"Checking response for bad years value - index bigger than {ushort.MaxValue}");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=0&NumberOfYears={ushort.MaxValue + 1}";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad ID value - value lower than 0");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=0&NumberOfYears=-1";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);


            TestLog.AddMessage("Checking response for bad ID value - char value");
            urlToTest = $"{ServerAddress}/api/Loan/ReturnPayments?LoanTypeID=0&NumberOfYears=test";
            WebHelpers.VerifyEndpoints(urlToTest, HttpStatusCode.BadRequest);
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
            decimal totalAmount = 4000;

            var interestPerMonth = interest / numberOfMonths;
            var capitalizationPeriod = numberOfYears * numberOfMonths;
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
    }
}
