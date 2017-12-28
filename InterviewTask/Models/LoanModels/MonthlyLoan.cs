using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class MonthlyLoan : Loan
    {
        public MonthlyLoan(Decimal interest)
        {
            Interest = interest;
        }

        public override List<Payment> ReturnPayments(Decimal TotalAmount, UInt16 NumberOfYears)
        {
            if (NumberOfYears * 12 > UInt16.MaxValue)
                throw new ArgumentOutOfRangeException("CapitalizationPeriods");

            var monthlyInterest = this.Interest / 12;
            var monthlyCapitalizationPeriods = (ushort)(NumberOfYears * 12);
            var monthlyCapital = TotalAmount / monthlyCapitalizationPeriods;
            var paymentList = new List<Payment>();

            for (ushort i = 0; i < monthlyCapitalizationPeriods; i++)
            {
                var currentPayment = new Payment
                {
                    PaymentNo = i,
                    Capital = monthlyCapital,
                    Interest = TotalAmount * monthlyInterest
                };
                paymentList.Add(currentPayment);

                TotalAmount -= monthlyCapital;
            }

            return paymentList;
        }
    }
}