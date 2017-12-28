using InterviewTask.Models;
using InterviewTask.Models.LoanModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Data
{
    public class MockDB
    {
        private LoanType[] _loans =
        {
            new LoanType
            {
                LoanTypeID = 0,
                LoanText = "Coffee express",
                Loan = new MonthlyLoan(0.11M)
            },

            new LoanType {
                LoanTypeID = 1,
                LoanText = "Laptop",
                Loan = new MonthlyLoan(0.09M)
            },

            new LoanType {
                LoanTypeID = 2,
                LoanText = "MTB Bike",
                Loan = new AnnuallyLoan(0.055M)
            },

            new LoanType {
                LoanTypeID = 3,
                LoanText = "Home renovation",
                Loan = new AnnuallyLoan(0.08M)
            },

            new LoanType {
                LoanTypeID = 4,
                LoanText = "Holidays",
                Loan = new MonthlyLoan(0.21M)
            },

            new LoanType {
                LoanTypeID = 5,
                LoanText = "Car",
                Loan = new MonthlyLoan(0.03M)
            },
        };

        public LoanType[] Loans
        {
            get { return _loans; }
        }

        //public Loan[] LoansTable =
        //{
        //    new Loan(0.0429M),
        //    new Loan(0.1M)
        //};
    }
}