using CommonModels;

namespace InterviewTask.Models
{
    public interface IDatabase
    {
        LoanType[] GetLoanTypes();

        Loan GetLoan(ushort LoanTypeId);
    }
}
