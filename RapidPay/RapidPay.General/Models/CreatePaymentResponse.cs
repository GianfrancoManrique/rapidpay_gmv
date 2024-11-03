namespace RapidPay.General.Models
{
    public class CreatePaymentResponse
    {
        public string? Number { get; set; }
        public decimal Fee { get; set; }
        public decimal CurrentBalance { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
