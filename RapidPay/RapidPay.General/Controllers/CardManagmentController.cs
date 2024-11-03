using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.General.Models;
using RapidPay.General.Services;

namespace RapidPay.General.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardManagmentController : ControllerBase
    {
        private ICardManagmentService _cardManagmentService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CardManagmentController(ICardManagmentService cardManagmentService)
        {
            _cardManagmentService = cardManagmentService;
        }

        [HttpPost("CreditCard")]
        public IActionResult CreateCreditCard([FromBody] CreateCreditCardRequest request, CancellationToken token)
        {
            var response = _cardManagmentService.CreateCreditCard(request);
            return CreatedAtAction(nameof(CreateCreditCard), new { id = response.Id });
        }

        [HttpGet("CreditCard/{number}")]
        public async Task<CreditCard?> GetCreditCardBalance(string number, CancellationToken token)
        {
            var response = await _cardManagmentService.GetCreditCardDetails(number);
            return response;
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request, CancellationToken token)
        {
            var response = await _cardManagmentService.CreatePayment(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(CreatePayment), response);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
