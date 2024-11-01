namespace RapidPay.General.Models
{
    public class CreateCreditCardRequest
    {
        public string? Number { get; set; }
        public decimal Balance { get; set; }
    }
}
