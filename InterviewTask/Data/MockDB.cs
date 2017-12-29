
namespace InterviewTask.Data
{
    using InterviewTask.Models;
    using InterviewTask.Models.LoanModels;

    public class MockDB
    {
        private LoanType[] _loans =
        {
            new LoanType
            {
                LoanTypeID = 0,
                LoanText = "Coffee express",
                Loan = new MonthlyLoan(0.11M, 4000)
            },

            new LoanType {
                LoanTypeID = 1,
                LoanText = "Laptop",
                Loan = new MonthlyLoan(0.09M, 8000)
            },

            new LoanType {
                LoanTypeID = 2,
                LoanText = "MTB Bike",
                Loan = new AnnuallyLoan(0.055M, 6000)
            },

            new LoanType {
                LoanTypeID = 3,
                LoanText = "Home renovation",
                Loan = new AnnuallyLoan(0.08M, 25000)
            },

            new LoanType {
                LoanTypeID = 4,
                LoanText = "Holidays",
                Loan = new MonthlyLoan(0.21M, 10000)
            },

            new LoanType {
                LoanTypeID = 5,
                LoanText = "Car",
                Loan = new MonthlyLoan(0.03M, 75000)
            },
        };

        public LoanType[] Loans
        {
            get => _loans;
        }
    }
}