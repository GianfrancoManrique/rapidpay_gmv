using Moq;
using RapidPay.General.MockData;
using RapidPay.General.Models;
using RapidPay.General.Repositories;
using RapidPay.General.Services.Implementations;
using RapidPay.General.Services.Interfaces;

namespace RapidPay.Tests
{
    public class CardManagmentServiceTests
    {
        private Mock<IUFEService> _ufeService { get; set; }
        private Mock<ICreditCardRepository> _creditCardRepository { get; set; }
        private CardManagmentService _cardManagmentService;

        [SetUp]
        public void Setup()
        {
            _creditCardRepository = new Mock<ICreditCardRepository>();
            _ufeService = new Mock<IUFEService>();
            _cardManagmentService = new CardManagmentService(_creditCardRepository.Object, _ufeService.Object);
        }

        [Test]
        public void CreateCreditCard_Success()
        {
            var request = MockCardManagmentData.creditCardRequest;
            _creditCardRepository.Setup(cr => cr.Create(It.IsAny<CreditCard>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);

            var response = _cardManagmentService.CreateCreditCard(request);

            Assert.That(response.Result.Success.Equals(true));
            Assert.That(response.Result.Balance.Equals(100m));
        }

        [Test]
        public void CreateCreditCard_Fails()
        {
            var request = MockCardManagmentData.creditCardRequest;

            var response = _cardManagmentService.CreateCreditCard(request);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Error on credit card creation"), Is.True);
        }

        [Test]
        public void GetCreditCardDetails_Success()
        {
            var number = "348899684619369";
            _creditCardRepository.Setup(cr => cr.GetDetails(It.IsAny<string>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);

            var response = _cardManagmentService.GetCreditCardDetails(number);

            Assert.That(response.Result.Success.Equals(true));
        }

        [Test]
        public void GetCreditCardDetails_InvalidNumber()
        {
            var number = string.Empty;

            var response = _cardManagmentService.GetCreditCardDetails(number);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Invalid credit card number"), Is.True);
        }

        [Test]
        public void GetCreditCardDetails_Inexisting()
        {
            var number = "348899684619369";

            var response = _cardManagmentService.GetCreditCardDetails(number);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Inexisting credit card"), Is.True);
        }

        [Test]
        public void CreatePayment_InvalidCreditCardNumber()
        {
            var request = MockCardManagmentData.invalidPaymentRequest;

            var response = _cardManagmentService.CreatePayment(request);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Invalid credit card number"), Is.True);
        }

        [Test]
        public void CreatePayment_InexistingCreditCardNumber()
        {
            var request = MockCardManagmentData.paymentRequestOne;
            var response = _cardManagmentService.CreatePayment(request);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Inexisting credit card number"), Is.True);
        }

        [Test]
        public void CreatePayment_InsufficientBalance()
        {
            var request = MockCardManagmentData.paymentRequestTwo;

            _creditCardRepository.Setup(cr => cr.GetDetails(It.IsAny<string>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);
            _ufeService.Setup(ufe => ufe.GetFee()).Returns(0.65m);

            var response = _cardManagmentService.CreatePayment(request);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Unable to update balance due to insufficient balance"), Is.True);
        }

        [Test]
        public void CreatePayment_FailsUpdateBalance()
        {
            var request = MockCardManagmentData.paymentRequestThree;

            _creditCardRepository.Setup(cr => cr.GetDetails(It.IsAny<string>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);
            _ufeService.Setup(ufe => ufe.GetFee()).Returns(0.65m);

            var response = _cardManagmentService.CreatePayment(request);

            Assert.That(response.Result.Success.Equals(false));
            Assert.That(response.Result?.Message.Contains("Unable to update balance"), Is.True);
        }

        [Test]
        public void CreatePayment_Success()
        {
            var request = MockCardManagmentData.paymentRequestThree;

            _creditCardRepository.Setup(cr => cr.GetDetails(It.IsAny<string>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);
            _creditCardRepository.Setup(cr => cr.UpdateBalance(It.IsAny<CreditCard>(), It.IsAny<decimal>())).ReturnsAsync(MockCardManagmentData.fakeCreditCard);
            _ufeService.Setup(ufe => ufe.GetFee()).Returns(0.65m);

            var response = _cardManagmentService.CreatePayment(request);

            Assert.That(response.Result.Success.Equals(true));
        }
    }
}