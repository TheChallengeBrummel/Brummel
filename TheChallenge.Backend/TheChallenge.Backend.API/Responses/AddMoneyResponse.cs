namespace TheChallenge.Backend.Responses
{
    public class AddMoneyResponse
    {
        public AddMoneyResponse(string description, double amount)
        {
            Description = description ?? throw new System.ArgumentNullException(nameof(description));
            Amount = amount;
        }

        public string Description { get; set; }
        public double Amount { get; set; }
    }
}
