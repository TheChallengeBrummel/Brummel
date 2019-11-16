using MediatR;
using System;
using TheChallenge.Backend.Responses;

namespace TheChallenge.Backend.Requests
{
    public class AddMoneyRequest : IRequest<AddMoneyResponse>
    {
        public Guid UserId { get; set; }

        public byte[] ImageData { get; set; }
    }
}