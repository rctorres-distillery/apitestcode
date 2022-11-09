using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Models;
using RapidPayApi.Services;

namespace RapidPayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        CardManagementService _cardMngService;
        public CardManagementController(CardManagementService cardManagementService)
        {
            _cardMngService = cardManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCard newCard)
        {
            var result = await _cardMngService.CreateCard(newCard);
            return Ok(result);
        }

        [HttpPost, Route("pay")]
        public async Task<IActionResult> Pay(PaymentInfo payment)
        {
            var result = await _cardMngService.PayWithCard(payment);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Balance(string cardNumber)
        {
            var result = await _cardMngService.GetCardBalance(cardNumber);
            return Ok(result);
        }
    }
}
