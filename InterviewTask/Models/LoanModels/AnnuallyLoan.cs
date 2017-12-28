using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models.LoanModels
{
    public class AnnuallyLoan : Loan
    {
        public AnnuallyLoan(decimal interest)
        {
            this.Interest = interest;
        }

        public override List<Payment> ReturnPayments(decimal TotalAmount, ushort NumberOfYears)
        {
            if (NumberOfYears * 12 > UInt16.MaxValue)
                throw new ArgumentOutOfRangeException("CapitalizationPeriods");

            var annuallyInterest = this.Interest / NumberOfYears;
            var annuallyCapital = TotalAmount / NumberOfYears;

            var paymentList = new List<Payment>();

            for (ushort i = 0; i < NumberOfYears; i++)
            {
                var currentPayment = new Payment
                {
                    PaymentNo = i,
                    Capital = annuallyCapital,
                    Interest = TotalAmount * annuallyInterest
                };
                paymentList.Add(currentPayment);

                TotalAmount -= annuallyCapital;
            }

            return paymentList;
        }
    }
}