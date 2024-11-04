namespace RapidPay.General.Models
{
    public class GetCreditCardResponse
    {
        public CreditCard CreditCard { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public GetCreditCardResponse(bool success, string message)
        {
            Success = success;
            Message = message;
            CreditCard = new CreditCard();
        }
    }
}
