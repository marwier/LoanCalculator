
namespace InterviewTask.Controllers
{
    using InterviewTask.Data;
    using InterviewTask.Models;
    using InterviewTask.Models.LoanModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    public class LoanController : ApiController
    {
        private DB _database = new MockDB();

        [HttpGet]
        public Decimal GetInterest(UInt16 LoanTypeID)
        {
            return getLoan(LoanTypeID).Interest;
        }

        [HttpGet]
        public List<LoanType> GetLoanTypes()
        {
            return _database.Loans.ToList();
        }

        [HttpGet]
        public List<Payment> ReturnPayments(UInt16 LoanTypeID, Decimal TotalAmount, UInt16 NumberOfYears)
        {
            try
            {
                return getLoan(LoanTypeID).ReturnPayments(TotalAmount, NumberOfYears);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        private Loan getLoan(UInt16 LoanTypeID)
        {
            try
            {
                return _database.Loans[LoanTypeID].Loan;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
