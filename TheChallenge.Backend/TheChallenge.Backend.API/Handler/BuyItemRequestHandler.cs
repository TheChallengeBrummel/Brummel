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
using TheChallenge.Backend.API.Enums;

namespace TheChallenge.Backend.Handler
{
    public class BuyItemRequestHandler : IRequestHandler<BuyItemRequest, BuyItemResponse>
    {
        private readonly ImageServiceSetting _imageServiceSetting;
        private readonly ILogger<AddMoneyRequestHandler> _logger;
        private readonly UserManager _userManager;

        public BuyItemRequestHandler(
            ILogger<AddMoneyRequestHandler> logger,
            IOptions<ImageServiceSetting> imageServiceSetting,
            UserManager userManager
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ImageServiceSetting = imageServiceSetting;
            _imageServiceSetting = imageServiceSetting.Value;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IOptions<ImageServiceSetting> ImageServiceSetting { get; }

        public async Task<BuyItemResponse> Handle(BuyItemRequest request, CancellationToken cancellationToken)
        {
            var itemType = ItemTypesExtension.GetItemTypeBySting(request.ItemType);
            var map = ItemTagMaps.GetMapByTag(itemType.ToString());
            await _userManager.BuyItem(new Guid(request.UserId), request.Amount, map.Description);
            return new BuyItemResponse(map.Description);
        }
    }
}
