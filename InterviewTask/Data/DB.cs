
namespace InterviewTask.Data
{
    using InterviewTask.Models;

    public abstract class DB
    {
        public abstract LoanType[] Loans { get; }
    }
}