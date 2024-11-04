using RapidPay.General.Models;

namespace RapidPay.General.MockData
{
    public static class MockCardManagmentData
    {
        public static List<CreditCard> CreditCards = new List<CreditCard>()
        {
            new CreditCard() { Number = "348899684619369", Balance = 100}
        };
    }
}
