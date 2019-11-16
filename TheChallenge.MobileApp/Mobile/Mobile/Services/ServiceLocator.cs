using System;
using System.Net.Http;
using Xamarin.Forms;

namespace Mobile.Services
{
    public static class ServiceLocator
    {
        private const string BaseUrl = "https://the-challenge-webapp-develop.azurewebsites.net/";

        public static ItemTypes? LastBoughtType { get; set; }
        public static double LastBalanceValue { get; set; }
        public static HttpClient HttpClient { get; } = new HttpClient();
        public static Client Client { get; } = new Client(BaseUrl, HttpClient);
        public static Guid UserId
        {
            get
            {
                var userId = Application.Current.Properties.TryGetValue("userId", out var value) ? value as Guid? : null;
                if (userId == null)
                {
                    userId = Guid.NewGuid();
                    Application.Current.Properties["userId"] = userId;
                }
                //return userId.Value;

                return Guid.Parse("8D00D354-1BDD-4E30-BA4A-779B0B646B14");
            }
        }
    }
}