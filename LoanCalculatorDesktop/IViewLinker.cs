using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
