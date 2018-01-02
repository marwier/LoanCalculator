using System.Collections.Generic;
using CommonModels;

namespace LoanCalculatorDesktop
{
    // required to separate presenter and view layer
    interface IViewLinker
    {
        List<LoanType> LoanTypes { get; set; }
        List<Payment> Payments { get; set; }
    }
}
