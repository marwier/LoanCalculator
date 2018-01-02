
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using CommonModels;
using InterviewTask.Data;
using InterviewTask.Models;

namespace InterviewTask.Controllers
{
    public class LoanController : ApiController
    {
        private readonly IDatabase _database = new MockDb();

        [HttpGet]
        public decimal GetInterest(ushort loanTypeId)
        {
            return GetLoan(loanTypeId).Interest;
        }

        [HttpGet]
        public List<LoanType> GetLoanTypes()
        {
            return _database.GetLoanTypes().ToList();
        }

        [HttpGet]
        public List<Payment> ReturnPayments(ushort loanTypeId, decimal totalAmount, ushort numberOfYears)
        {
            var selectedLoan = GetLoan(loanTypeId);

            try
            {
                return selectedLoan.ReturnPayments(totalAmount, numberOfYears);
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
                return _database.GetLoan(loanTypeId);
            }
            catch (IndexOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
