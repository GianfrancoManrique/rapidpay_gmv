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

        [HttpPost]
        public IActionResult CreateCard([FromBody] CreateCreditCardRequest request, CancellationToken token)
        {
            var response = _cardManagmentService.CreateCreditCard(request);
            return CreatedAtAction(nameof(CreateCard), new { id = response.Id });
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
