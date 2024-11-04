using RapidPay.General.Models;

namespace RapidPay.General.Repositories
{
    public interface ICreditCardRepository
    {
        Task<CreditCard?> Create(CreditCard creditCard);
        Task<CreditCard?> GetDetails(string number);
        Task<CreditCard?> UpdateBalance(CreditCard creditCard, decimal newBalance);
    }
}
