
using CommonModels;
using InterviewTask.Models;
using InterviewTask.Models.LoanModels;

namespace InterviewTask.Data
{
    public class MockDb
    {
        public LoanType[] LoanTypes { get; } =
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

        public Loan[] Loans { get; } =
        {
            new Loan(new MonthlyCapitalization(), 0.11M, 4000),
            new Loan(new MonthlyCapitalization(), 0.09M, 8000),
            new Loan(new MonthlyCapitalization(), 0.055M, 6000),
            new Loan(new MonthlyCapitalization(), 0.08M, 25000),
            new Loan(new MonthlyCapitalization(), 0.21M, 10000),
            new Loan(new MonthlyCapitalization(), 0.03M, 75000),
            new Loan(new MonthlyCapitalization(), 0.05M, 225000),
            new Loan(new MonthlyCapitalization(), 0.145M, 15000)
        };
    }
}