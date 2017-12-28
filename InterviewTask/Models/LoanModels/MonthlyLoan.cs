
namespace InterviewTask.Models.LoanModels
{
    using System;
    using System.Collections.Generic;

    public class MonthlyLoan : Loan
    {
        public MonthlyLoan(Decimal interest)
        {
            this.Interest = interest;
        }

        public override List<Payment> ReturnPayments(Decimal TotalAmount, UInt16 NumberOfYears)
        {
            this.ValidateInputs(TotalAmount, NumberOfYears);

            var interestPerMonth = this.Interest / Compounds.Monthly;
            var capitalizationPeriod = NumberOfYears * Compounds.Monthly;
            var capitalPerMonth = TotalAmount / capitalizationPeriod;

            var paymentList = new List<Payment>();

            for (ushort i = 0; i < capitalizationPeriod; i++)
            {
                paymentList.Add(new Payment
                {
                    PaymentID = i,
                    Capital = capitalPerMonth,
                    Interest = TotalAmount * interestPerMonth
                });

                TotalAmount -= capitalPerMonth;
            }

            return paymentList;
        }
    }
}