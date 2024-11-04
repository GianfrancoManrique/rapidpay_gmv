using RapidPay.General.Models;

namespace RapidPay.General.Services.Interfaces
{
    public interface ICardManagmentService
    {
        Task<CreateCreditCardResponse> CreateCreditCard(CreateCreditCardRequest request);
        Task<GetCreditCardResponse> GetCreditCardDetails(string number);
        Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
    }
}
