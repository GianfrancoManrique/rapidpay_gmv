using RapidPay.General.MockData;
using RapidPay.General.Models;

namespace RapidPay.General.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        public async Task<CreditCard?> Create(CreditCard creditCard)
        {
            await Task.Delay(1000); // added to simulate multithreading
            MockCardManagmentData.CreditCards.Add(creditCard);
            return creditCard;
        }

        public async Task<CreditCard?> GetDetails(string number)
        {
            await Task.Delay(1000); // added to simulate multithreading
            return MockCardManagmentData.CreditCards.FirstOrDefault(card => card.Number == number);
        }

        public async Task<CreditCard?> UpdateBalance(CreditCard creditCard, decimal newBalance)
        {
            await Task.Delay(1000); // added to simulate multithreading
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
