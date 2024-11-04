using RapidPay.General.Models;

namespace RapidPay.General.MockData
{
    public static class MockCardManagmentData
    {
        public static List<CreditCard> CreditCards = new List<CreditCard>()
        {
            new CreditCard() { Number = "348899684619369", Balance = 100}
        };

        public static CreateCreditCardRequest creditCardRequest = new CreateCreditCardRequest()
        {
            Number = "348899684619369",
            Balance = 100
        };

        public static CreditCard fakeCreditCard = new CreditCard()
        {
            Number = "348899684619369",
            Balance = 100
        };

        public static CreatePaymentRequest paymentRequestOne = new CreatePaymentRequest()
        {
            Number = "348899684619369",
            Amount = 5
        };

        public static CreatePaymentRequest paymentRequestTwo = new CreatePaymentRequest()
        {
            Number = "348899684619369",
            Amount = 105
        };

        public static CreatePaymentRequest paymentRequestThree = new CreatePaymentRequest()
        {
            Number = "348899684619369",
            Amount = 15
        };

        public static CreatePaymentRequest invalidPaymentRequest = new CreatePaymentRequest()
        {
            Number = string.Empty,
            Amount = 10
        };
    }
}
