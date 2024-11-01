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
    }
}
