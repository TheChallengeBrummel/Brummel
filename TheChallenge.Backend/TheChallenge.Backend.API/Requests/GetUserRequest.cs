using MediatR;
using TheChallenge.Backend.API.Responses;

namespace TheChallenge.Backend.Requests
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public string UserId { get; set; }
    }
}
