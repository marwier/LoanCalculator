﻿
using System.Collections.Generic;
using CommonModels;

namespace InterviewTask.Models.LoanModels
{
    public interface ILoanModel
    {
        List<Payment> ReturnPayments(decimal interest, decimal totalAmount, ushort numberOfYears);
    }
}
