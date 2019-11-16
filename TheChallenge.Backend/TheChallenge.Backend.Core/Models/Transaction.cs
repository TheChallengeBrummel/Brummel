using System;

namespace TheChallenge.Backend.Core.Models
{
    public class Transaction
    {
        public double Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}