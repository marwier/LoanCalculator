
namespace InterviewTask.Models.LoanModels
{
    using System;
    using System.Collections.Generic;

    public class AnnuallyLoan : Loan
    {
        public AnnuallyLoan(Decimal interest, Decimal totalAmount)
        {
            this.Interest = interest;
            this.TotalAmount = totalAmount;
        }

        public override List<Payment> ReturnPayments(ushort NumberOfYears)
        {
            this.ValidateInputs(NumberOfYears);

            var interestPerMonth = this.Interest / Compounds.Annually;
            var capitalizationPeriod = NumberOfYears * Compounds.Annually;
            var capitalPerMonth = this.TotalAmount / capitalizationPeriod;

            var paymentList = new List<Payment>();

            for (ushort i = 0; i < capitalizationPeriod; i++)
            {
                paymentList.Add(new Payment
                {
                    PaymentID = i,
                    Capital = capitalPerMonth,
                    Interest = this.TotalAmount * interestPerMonth
                });

                this.TotalAmount -= capitalPerMonth;
            }

            return paymentList;
        }
    }
}