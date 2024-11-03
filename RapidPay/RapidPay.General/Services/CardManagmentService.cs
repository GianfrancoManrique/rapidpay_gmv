using RapidPay.General.Models;
using RapidPay.General.Repositories;

namespace RapidPay.General.Services
{
    public class CardManagmentService : ICardManagmentService
    {
        private ICreditCardRepository _creditCardRepository;

        public CardManagmentService(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public CreateCreditCardResponse CreateCreditCard(CreateCreditCardRequest request)
        {
            var response = new CreateCreditCardResponse();
            var creditCard = new CreditCard
            {
                Number = request.Number,
                Balance = request.Balance
            };
            var creditCardId = _creditCardRepository.Create(creditCard);
            response.Id = creditCardId;
            return response;
        }

        public async Task<CreditCard?> GetCreditCardDetails(string number)
        {
            CreditCard? creditCard = await _creditCardRepository.GetDetails(number);
            return creditCard;
        }

        public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
        {
            var response = new CreatePaymentResponse();
            if (request.Number == null)
            {
                response.Success = false;
                response.Message = "Invalid credit card number";
                return response;
            }

            var existingCreditCard = await _creditCardRepository.GetDetails(request.Number);
            if (existingCreditCard == null)
            {
                response.Success = false;
                response.Message = "Inexisting credit card number";
                return response;
            }

            var fee = UFEService.Instance.GetFee();
            decimal newBalance = existingCreditCard.Balance - request.Amount - request.Amount*fee;

            var updatedCreditCard = _creditCardRepository.UpdateBalance(existingCreditCard, newBalance);

            if (updatedCreditCard == null)
            {
                response.Success = false;
                response.Message = "Not able to update balance";
                return response;
            }
            response.Success = true;
            response.CurrentBalance = updatedCreditCard.Balance;
            response.Fee = fee;
            response.Message = "Successfull payment creation";
            return response;
        }
    }
}
