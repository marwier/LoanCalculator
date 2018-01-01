
namespace CommonModels
{
    public class LoanType
    {
        public ushort LoanTypeId { get; set; }

        public string LoanText { get; set; }

        // override required for ComboBox in win forms
        public override string ToString()
        {
            return $"{LoanTypeId + 1}: {LoanText}";
        }
    }
}