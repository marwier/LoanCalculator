
using System;
using System.Collections.Generic;
using CommonModels;
using InterviewTask.Models.LoanModels;

namespace InterviewTask.Models
{
    public class Loan
    {
        public readonly decimal Interest;
        public readonly decimal TotalAmount;
        private readonly ILoanModel _loanModel;

        public Loan(ILoanModel loanModel, decimal interest, decimal totalAmount)
        {
            _loanModel = loanModel ?? throw new ArgumentNullException(nameof(loanModel));

            Interest = interest;
            TotalAmount = totalAmount;
        }

        public List<Payment> ReturnPayments(ushort numberOfYears)
        {
            return _loanModel.ReturnPayments(Interest, TotalAmount, numberOfYears);
        }
    }
}

