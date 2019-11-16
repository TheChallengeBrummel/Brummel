namespace TheChallenge.Backend.Responses
{
    public class BuyItemResponse
    {
        public BuyItemResponse(string description)
        {
            Description = description ?? throw new System.ArgumentNullException(nameof(description));
        }

        public string Description { get; set; }
    }
}
