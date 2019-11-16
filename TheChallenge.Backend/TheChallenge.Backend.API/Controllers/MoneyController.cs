using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheChallenge.Backend.Requests;
using TheChallenge.Backend.Responses;

namespace TheChallenge.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoneyController : Controller
    {
        private readonly ILogger<MoneyController> _logger;
        private readonly IMediator _mediator;


        public MoneyController(IMediator mediator, ILogger<MoneyController> logger)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [HttpPost("add")]
        [Produces(typeof(AddMoneyResponse))]
        public async Task<IActionResult> Add([FromBody] AddMoneyRequest request)
        {
            var mappingResult = await _mediator.Send(request);
            return Json(mappingResult);
        }
    }
}
