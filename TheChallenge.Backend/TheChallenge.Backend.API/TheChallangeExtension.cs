using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheChallenge.Backend.DataAccess;
using TheChallenge.Backend.CognitiveServices;
using TheChallenge.Backend.DataAccess.Repository;
using TheChallenge.Backend.Core;

namespace TheChallenge.Backend.API
{
    public static class TheChallangeExtension
    {
        public static void AddTheChallangeServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImageServiceSetting>(
                options =>
                {
                    options.ServiceKey = configuration.GetSection("ImageServiceSetting:ServiceKey").Value;
                    options.ServiceUri = configuration.GetSection("ImageServiceSetting:ServiceUri").Value;
                });
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

            services.AddScoped<DatabaseContext>();
            services.AddScoped<ItemsRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<UserManager>();
        }
    }
}