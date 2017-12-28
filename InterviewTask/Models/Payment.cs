
namespace InterviewTask.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Payment
    {
        [DataMember]
        public UInt16 PaymentID { get; set; }

        [DataMember]
        public Decimal Capital { get; set; }

        [DataMember]
        public Decimal Interest { get; set; }

        [DataMember]
        public Decimal Total
        {
            get
            {
                return Capital + Interest;
            }

            private set
            {
                // set is required due to DataMember attribute, no actions performed
            }
        }
    }
}