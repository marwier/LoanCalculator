
namespace InterviewTask.Models.LoanModels
{
    using System.Collections.Generic;

    public class AnnuallyLoan : Loan
    {
        public AnnuallyLoan(decimal interest)
        {
            this.Interest = interest;
        }

        public override List<Payment> ReturnPayments(decimal TotalAmount, ushort NumberOfYears)
        {
            this.ValidateInputs(TotalAmount, NumberOfYears);

            var interestPerMonth = this.Interest / Compounds.Annually;
            var capitalizationPeriod = NumberOfYears * Compounds.Annually;
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