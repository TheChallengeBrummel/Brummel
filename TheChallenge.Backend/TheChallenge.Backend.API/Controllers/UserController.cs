using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheChallenge.Backend.API.Responses;
using TheChallenge.Backend.Core;
using TheChallenge.Backend.Requests;

namespace TheChallenge.Backend.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager userManager;

        public UserController(IMediator mediator, UserManager userManager)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.userManager = userManager;
        }

        [HttpGet("{userId}")]
        [Produces(typeof(GetUserResponse))]
        public async Task<IActionResult> GetUserAsync(string userId)
        {
            var mappingResult = await _mediator.Send(new GetUserRequest() { UserId = userId });
            return Json(mappingResult);
        }

        [HttpGet("/reset")]
        public async Task GenerateDemoDataAsync()
        {
            await userManager.GenerateDemoData();
        }
    }
}