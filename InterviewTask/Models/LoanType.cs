using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace InterviewTask.Models
{
    [DataContract]
    public class LoanType
    {
        [DataMember]
        public UInt16 LoanTypeID { get; set; }

        [DataMember]
        public String LoanText { get; set; }

        [IgnoreDataMember]
        public Loan Loan { get; set; }

        // override required for ComboBox in win forms
        public override string ToString()
        {
            return $"{LoanTypeID + 1}: {LoanText}";
        }
    }
}