
namespace InterviewTask.Models.LoanModels
{
    using System;
    using System.Collections.Generic;

    public abstract class Loan
    {
        public Decimal Interest { get; protected set; }

        public abstract List<Payment> ReturnPayments(Decimal TotalAmount, UInt16 NumberOfYears);

        protected void ValidateInputs(Decimal TotalAmount, UInt16 NumberOfYears)
        {
            if (this.Interest < 0)
                throw new ArgumentOutOfRangeException("Interest cannot be less than 0");

            if (TotalAmount <= 0)
                throw new ArgumentOutOfRangeException("Loan amount cannot be less or equal 0");

            if (NumberOfYears <= 0)
                throw new ArgumentOutOfRangeException("Years cannot be less or equal 0");
        }

        protected struct Compounds
        {
            public const int Monthly = 12;
            public const int Quarterly = 4;
            public const int Annually = 1;
        }
    }
}