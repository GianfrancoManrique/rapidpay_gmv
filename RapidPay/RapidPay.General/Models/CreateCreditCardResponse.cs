namespace RapidPay.General.Models
{
    public class CreateCreditCardResponse
    {
        public string? Number { get; set; }
        public decimal? Balance { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public CreateCreditCardResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
    }
}
