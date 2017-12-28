using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public abstract class Loan
    {
        public Decimal Interest { get; protected set; }

        public abstract List<Payment> ReturnPayments(Decimal TotalAmount, UInt16 NumberOfYears);
    }
}