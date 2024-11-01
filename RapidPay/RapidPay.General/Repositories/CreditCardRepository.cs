using RapidPay.General.MockData;
using RapidPay.General.Models;

namespace RapidPay.General.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        public int Create(CreditCard creditCard)
        {
            MockCardManagmentData.CreditCards.Add(creditCard);
            return MockCardManagmentData.CreditCards.Count;
        }
    }
}
