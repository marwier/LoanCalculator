
using System.Runtime.Serialization;

namespace CommonModels
{
    [DataContract]
    public class Payment
    {
        [DataMember]
        public ushort PaymentId { get; set; }

        [DataMember]
        public decimal Capital { get; set; }

        [DataMember]
        public decimal Interest { get; set; }

        [DataMember]
        public decimal Total
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