using System;

namespace TheChallenge.Backend.DataAccess.Model
{
    public class Transaction
    {
        public double Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}