using MediatR;
using TheChallenge.Backend.API.Enums;
using TheChallenge.Backend.Responses;

namespace TheChallenge.Backend.Requests
{
    public class BuyItemRequest : IRequest<BuyItemResponse>
    {
        public double Amount { get; set; }

        public string ItemType { get; set; }

        public string UserId { get; set; }
    }
}
