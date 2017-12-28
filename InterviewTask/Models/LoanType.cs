
namespace InterviewTask.Models
{
    using InterviewTask.Models.LoanModels;
    using System;
    using System.Runtime.Serialization;

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