using RapidPay.General.Models;

namespace RapidPay.General.Repositories
{
    public interface ICreditCardRepository
    {
        int Create(CreditCard creditCard);
    }
}
