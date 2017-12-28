using InterviewTask.Data;
using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InterviewTask.Controllers
{
    public class LoanController : ApiController
    {
        private MockDB _db = new MockDB();

        [HttpGet]
        public List<Payment> ReturnPayments(UInt16 LoanTypeID, Decimal TotalAmount, UInt16 NumberOfYears)
        {
            Loan l;
            try
            {
                l = GetLoan(LoanTypeID);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                return l.ReturnPayments(TotalAmount, NumberOfYears);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        private Loan GetLoan(UInt16 LoanTypeID)
        {
            return _db.Loans[LoanTypeID].Loan;
        }


        [HttpGet]
        public Decimal GetInterest(UInt16 LoanTypeID)
        {
            try
            {
                return GetLoan(LoanTypeID).Interest;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        public List<LoanType> GetLoanTypes()
        {
            return _db.Loans.ToList();
        }
    }
}
