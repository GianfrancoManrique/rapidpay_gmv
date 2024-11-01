using RapidPay.General.Models;
using System.Security.Cryptography.Xml;

namespace RapidPay.General.MockData
{
    public static class MockCardManagmentData
    {
        public static List<CreditCard> CreditCards = new List<CreditCard>()
        {
            new CreditCard() { Balance = 1000, Number = "1111 1111 1111 111"}
        };
    }
}
