using CommonModels;
using InterviewTask.Models;
using InterviewTask.Models.LoanModels;

namespace InterviewTask.Data
{
    public class MockDb : IDatabase
    {
        private LoanType[] LoanTypes { get; } =
        {
            new LoanType
            {
                LoanTypeId = 0,
                LoanText = "Coffee express"
            },

            new LoanType {
                LoanTypeId = 1,
                LoanText = "Laptop"
            },

            new LoanType {
                LoanTypeId = 2,
                LoanText = "MTB Bike"
            },

            new LoanType {
                LoanTypeId = 3,
                LoanText = "Home renovation"
            },

            new LoanType {
                LoanTypeId = 4,
                LoanText = "Holidays"
            },

            new LoanType {
                LoanTypeId = 5,
                LoanText = "Car"
            },

            new LoanType {
                LoanTypeId = 6,
                LoanText = "House"
            },

            new LoanType {
                LoanTypeId = 7,
                LoanText = "Plastic surgery"
            }
        };

        private Loan[] Loans { get; } =
        {
            new Loan(new MonthlyCapitalization(), 0.11M),
            new Loan(new MonthlyCapitalization(), 0.09M),
            new Loan(new MonthlyCapitalization(), 0.055M),
            new Loan(new MonthlyCapitalization(), 0.08M),
            new Loan(new MonthlyCapitalization(), 0.21M),
            new Loan(new MonthlyCapitalization(), 0.03M),
            new Loan(new MonthlyCapitalization(), 0.05M),
            new Loan(new MonthlyCapitalization(), 0.145M)
        };

        public LoanType[] GetLoanTypes()
        {
            return LoanTypes;
        }

        public Loan GetLoan(ushort LoanTypeId)
        {
            return Loans[LoanTypeId];
        }
    }
}