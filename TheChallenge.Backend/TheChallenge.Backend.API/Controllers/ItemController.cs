using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheChallenge.Backend.Core;
using TheChallenge.Backend.Requests;
using TheChallenge.Backend.Responses;

namespace TheChallenge.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly ILogger<MoneyController> _logger;
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator, ILogger<MoneyController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("buy")]
        [Produces(typeof(BuyItemResponse))]
        public async Task<IActionResult> Buy([FromBody] BuyItemRequest request)
        {
            var mappingResult = await _mediator.Send(request);
            return Json(mappingResult);
        }
    }
}
