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

        public async Task<CreditCard?> GetDetails(string number)
        {
            await Task.Delay(1000);
            return MockCardManagmentData.CreditCards.FirstOrDefault(card => card.Number == number);
        }

        public CreditCard? UpdateBalance(CreditCard creditCard, decimal newBalance)
        {
            var existingCreditCard = MockCardManagmentData.CreditCards.FirstOrDefault(card => card.Number == creditCard?.Number);
            if (existingCreditCard == null)
            {
                return null;
            }
            existingCreditCard.Balance = newBalance;
            return existingCreditCard;
        }
    }
}
