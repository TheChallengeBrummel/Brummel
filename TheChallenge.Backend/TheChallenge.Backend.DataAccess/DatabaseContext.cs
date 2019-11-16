using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheChallenge.Backend.DataAccess.Model;

namespace TheChallenge.Backend.DataAccess
{
    public class DatabaseContext
    {
        public IMongoDatabase Database { get; set; }

        public DatabaseContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            Database = client.GetDatabase(options.Value.Database);
        }
    }
}