
using InterviewTask.Models;
using InterviewTask.Models.LoanModels;

namespace InterviewTask.Data
{
    public class MockDb
    {
        public LoanType[] Loans { get; } =
        {
            new LoanType
            {
                LoanTypeId = 0,
                LoanText = "Coffee express",
                Loan = new Loan(new MonthlyCapitalization(), 0.11M, 4000)
            },

            new LoanType {
                LoanTypeId = 1,
                LoanText = "Laptop",
                Loan = new Loan(new MonthlyCapitalization(), 0.09M, 8000)
            },

            new LoanType {
                LoanTypeId = 2,
                LoanText = "MTB Bike",
                Loan = new Loan(new MonthlyCapitalization(), 0.055M, 6000)
            },

            new LoanType {
                LoanTypeId = 3,
                LoanText = "Home renovation",
                Loan = new Loan(new MonthlyCapitalization(), 0.08M, 25000)
            },

            new LoanType {
                LoanTypeId = 4,
                LoanText = "Holidays",
                Loan = new Loan(new MonthlyCapitalization(), 0.21M, 10000)
            },

            new LoanType {
                LoanTypeId = 5,
                LoanText = "Car",
                Loan = new Loan(new MonthlyCapitalization(), 0.03M, 75000)
            },

            new LoanType {
                LoanTypeId = 6,
                LoanText = "House",
                Loan = new Loan(new MonthlyCapitalization(), 0.05M, 225000)
            },

            new LoanType {
                LoanTypeId = 7,
                LoanText = "Plastic surgery",
                Loan = new Loan(new MonthlyCapitalization(), 0.145M, 15000)
            }
        };
    }
}