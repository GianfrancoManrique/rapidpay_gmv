using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.General.Models;
using RapidPay.General.Services.Interfaces;

namespace RapidPay.General.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CardManagmentController : ControllerBase
    {
        private ICardManagmentService _cardManagmentService;

        public CardManagmentController(ICardManagmentService cardManagmentService)
        {
            _cardManagmentService = cardManagmentService;
        }

        [HttpPost("CreditCard")]
        public async Task<IActionResult> CreateCreditCard([FromBody] CreateCreditCardRequest request, CancellationToken token)
        {
            var response = await _cardManagmentService.CreateCreditCard(request);
            if (response.Success)
            {
                return CreatedAtAction(nameof(CreateCreditCard), new { id = response.Number });
            }
            return BadRequest(response);
        }

        [HttpGet("CreditCard/{number}")]
        public async Task<IActionResult> GetCreditCardBalance(string number, CancellationToken token)
        {
            var response = await _cardManagmentService.GetCreditCardDetails(number);
            if (response.Success)
            {
                return CreatedAtAction(nameof(CreateCreditCard), response);
            }
            return BadRequest(response);
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request, CancellationToken token)
        {
            var response = await _cardManagmentService.CreatePayment(request);
            if (response.Success)
            {
                return CreatedAtAction(nameof(CreatePayment), response);
            }
            return BadRequest(response);
        }
    }
}
