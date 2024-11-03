using RapidPay.General.Models;

namespace RapidPay.General.Repositories
{
    public interface ICreditCardRepository
    {
        int Create(CreditCard creditCard);
        Task<CreditCard?> GetDetails(string number);
        CreditCard? UpdateBalance(CreditCard creditCard, decimal newBalance);
    }
}
