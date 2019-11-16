using System.Collections.Generic;
using TheChallenge.Backend.Core.Models;

namespace TheChallenge.Backend.API.Responses
{
    public class GetUserResponse
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}