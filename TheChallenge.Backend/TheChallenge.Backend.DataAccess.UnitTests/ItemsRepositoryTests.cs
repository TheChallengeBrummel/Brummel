using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TheChallenge.Backend.DataAccess.Model;
using TheChallenge.Backend.DataAccess.Repository;
using Xunit;

namespace TheChallenge.Backend.DataAccess.UnitTests
{
    public class ItemsRepositoryTests
    {
        private static readonly string connectionString = "mongodb://the-challenge-db:lz9ym9rlLQF49PEhogaSi3OLq96JMajgGXvPwZLEkzJMKfmq6WawyXVPT87ZJBUo2gSHkEDyxJ1Kn7xsEWtEvA==@the-challenge-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@the-challenge-db@&retrywrites=false";
        private IOptions<MongoSettings> options;

        public ItemsRepositoryTests()
        {
            options = Options.Create(new MongoSettings
            {
                ConnectionString = connectionString,
                Database = "the-challenge-db"
            });
        }

        [Fact]
        public async Task Items_GetAllItemsAsync()
        {
            var context = new DatabaseContext(options);
            var itemsRepository = new ItemsRepository(context);
            _ = await itemsRepository.GetAll();
        }

        [Fact]
        public async Task Items_AddNewItemAsync()
        {
            var context = new DatabaseContext(options);
            var itemsRepository = new ItemsRepository(context);
            await itemsRepository.Insert(new Item
            {
                Name = "Unit Test",
                Price = 5.43
            });
        }
    }
}