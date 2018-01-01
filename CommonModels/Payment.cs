
namespace CommonModels
{
    public class Payment
    {
        public ushort PaymentId { get; set; }

        public decimal Capital { get; set; }

        public decimal Interest { get; set; }

        public decimal Total => Capital + Interest;
    }
}