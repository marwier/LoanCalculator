using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace InterviewTask.Models
{
    [DataContract]
    public class Payment
    {
        [DataMember]
        public UInt16 PaymentNo { get; set; }

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