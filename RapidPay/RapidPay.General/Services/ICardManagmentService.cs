using RapidPay.General.Models;

namespace RapidPay.General.Services
{
    public interface ICardManagmentService
    {
        CreateCreditCardResponse CreateCreditCard(CreateCreditCardRequest request);
    }
}
