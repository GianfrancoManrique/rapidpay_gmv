using RapidPay.General.MockData;
using RapidPay.General.Models;
using RapidPay.General.Repositories;
using RapidPay.General.Services.Interfaces;

namespace RapidPay.General.Services.Implementations
{
    public class CardManagmentService : ICardManagmentService
    {
        private IUFEService _ufeService;
        private ICreditCardRepository _creditCardRepository;

        public CardManagmentService(ICreditCardRepository creditCardRepository, IUFEService ufeService)
        {
            _creditCardRepository = creditCardRepository;
            _ufeService = ufeService;
        }

        public async Task<CreateCreditCardResponse> CreateCreditCard(CreateCreditCardRequest request)
        {
            var response = new CreateCreditCardResponse(true, "Successfull credit card creation");

            var creditCard = new CreditCard
            {
                Number = GetNumber(),
                Balance = request?.Balance ?? Constants.DEFAULT_BALANCE
            };

            var creditCardCreated = await _creditCardRepository.Create(creditCard);
            if (creditCardCreated == null)
            {
                response = new CreateCreditCardResponse(false, "Error on credit card creation");
                return response;
            }

            response.Number = creditCardCreated?.Number;
            response.Balance = creditCardCreated?.Balance;
            return response;
        }

        private string GetNumber()
        {
            string[] prefixes = { "34", "37" };
            Random random = new Random();

            // Select a random prefix
            string prefix = prefixes[random.Next(0, prefixes.Length)];

            // Generate the remaining 13 random digits
            for (int i = 0; i < 13; i++)
            {
                prefix += random.Next(0, 10);
            }

            return prefix;
        }

        public async Task<GetCreditCardResponse> GetCreditCardDetails(string number)
        {
            var response = new GetCreditCardResponse(true, "Success getting credit card details");

            if (string.IsNullOrEmpty(number))
            {
                response = new GetCreditCardResponse(false, "Invalid credit card number");
                response.CreditCard.Number = string.Empty;
                return response;
            }

            var creditCard = await _creditCardRepository.GetDetails(number);
            if (creditCard == null)
            {
                response = new GetCreditCardResponse(false, "Inexisting credit card");
                response.CreditCard.Number = number;
                return response;
            }

            response.CreditCard = creditCard;
            return response;
        }

        public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
        {
            var response = new CreatePaymentResponse(request.Number, true, "Successfull payment creation");

            if (string.IsNullOrEmpty(request.Number))
            {
                response = new CreatePaymentResponse(string.Empty, false, "Invalid credit card number");
                return response;
            }

            var existingCreditCard = await _creditCardRepository.GetDetails(request.Number);
            if (existingCreditCard == null)
            {
                response = new CreatePaymentResponse(request.Number, false, "Inexisting credit card number");
                return response;
            }

            var fee = _ufeService.GetFee();
            decimal newBalance = existingCreditCard.Balance - request.Amount - request.Amount * fee;

            if (newBalance < 0)
            {
                response = new CreatePaymentResponse(request.Number,false, "Unable to update balance due to insufficient balance");
                return response;
            }

            var updatedCreditCard = await _creditCardRepository.UpdateBalance(existingCreditCard, newBalance);
            if (updatedCreditCard == null)
            {
                response = new CreatePaymentResponse(request.Number, false, "Unable to update balance");
                return response;
            }

            response.NewBalance = updatedCreditCard.Balance;
            response.PaymentFee = fee;
            return response;
        }
    }
}
