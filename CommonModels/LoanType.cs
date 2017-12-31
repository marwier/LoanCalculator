
using System.Runtime.Serialization;

namespace CommonModels
{
    [DataContract]
    public class LoanType
    {
        [DataMember]
        public ushort LoanTypeId { get; set; }

        [DataMember]
        public string LoanText { get; set; }

        // override required for ComboBox in win forms
        public override string ToString()
        {
            return $"{LoanTypeId + 1}: {LoanText}";
        }
    }
}