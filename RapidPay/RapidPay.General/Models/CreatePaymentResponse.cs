namespace RapidPay.General.Models
{
    public class CreatePaymentResponse
    {
        public string CreditCardNumber { get; set; }
        public decimal NewBalance { get; set; }
        public decimal PaymentFee { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public CreatePaymentResponse(string creditCardNumber, bool success, string? message)
        {
            CreditCardNumber = creditCardNumber;
            Success = success;
            Message = message;
        }
    }
}
