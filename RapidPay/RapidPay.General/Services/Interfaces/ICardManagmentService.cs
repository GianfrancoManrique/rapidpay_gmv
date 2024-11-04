using RapidPay.General.Models;

namespace RapidPay.General.Services.Interfaces
{
    public interface ICardManagmentService
    {
        Task<CreateCreditCardResponse> CreateCreditCard(CreateCreditCardRequest request);
        Task<CreditCard?> GetCreditCardDetails(string number);
        Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
    }
}
