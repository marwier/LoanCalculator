
using System;
using System.Collections.Generic;
using CommonModels;
using InterviewTask.Models.LoanModels;

namespace InterviewTask.Models
{
    public class Loan
    {
        public readonly decimal Interest;
        private readonly ILoanModel _loanModel;

        public Loan(ILoanModel loanModel, decimal interest)
        {
            _loanModel = loanModel ?? throw new ArgumentNullException(nameof(loanModel));

            Interest = interest;
        }

        public List<Payment> ReturnPayments(decimal totalAmount, ushort numberOfYears)
        {
            return _loanModel.ReturnPayments(Interest, totalAmount, numberOfYears);
        }
    }
}

