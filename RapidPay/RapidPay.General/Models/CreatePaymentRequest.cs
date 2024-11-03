namespace RapidPay.General.Models
{
    public class CreatePaymentRequest
    {
        public string? Number { get; set; }
        public decimal Amount { get; set; }
    }
}
