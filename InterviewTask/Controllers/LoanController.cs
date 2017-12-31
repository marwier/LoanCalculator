
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using InterviewTask.Data;
using InterviewTask.Models;

namespace InterviewTask.Controllers
{
    public class LoanController : ApiController
    {
        private readonly MockDb _database = new MockDb();

        [HttpGet]
        public decimal GetInterest(ushort loanTypeId)
        {
            return GetLoan(loanTypeId).Interest;
        }

        [HttpGet]
        public decimal GetAmount(ushort loanTypeId)
        {
            return GetLoan(loanTypeId).TotalAmount;
        }

        [HttpGet]
        public List<LoanType> GetLoanTypes()
        {
            return _database.Loans.ToList();
        }

        [HttpGet]
        public List<Payment> ReturnPayments(ushort loanTypeId, ushort numberOfYears)
        {
            var selectedLoan = GetLoan(loanTypeId);

            try
            {
                return selectedLoan.ReturnPayments(numberOfYears);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        private Loan GetLoan(ushort loanTypeId)
        {
            try
            {
                return _database.Loans.Single(x => loanTypeId.Equals(x.LoanTypeId)).Loan;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
