using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheChallenge.Backend.CognitiveServices;
using TheChallenge.Backend.Requests;
using TheChallenge.Backend.Responses;
using TheChallenge.Backend.Core;
using TheChallenge.Backend.API.Tagging;
using TheChallenge.Backend.API.Responses;
using TheChallenge.Backend.Core.Models;

namespace TheChallenge.Backend.Handler
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly ILogger<AddMoneyRequestHandler> _logger;
        private readonly UserManager _userManager;

        public GetUserRequestHandler(
            ILogger<AddMoneyRequestHandler> logger,
            UserManager userManager
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.GetUserAsync(request.UserId);

            return new GetUserResponse()
            {
                Name = user.Name,
                Balance = user.Balance,
                Transactions = user.Transactions
            };
        }
    }
}
