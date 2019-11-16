using MongoDB.Driver;
using TheChallenge.Backend.DataAccess.Model;

namespace TheChallenge.Backend.DataAccess.Repository
{
    public class ItemsRepository : BaseRepository<Item>
    {
        public ItemsRepository(DatabaseContext mongoDatabase) : base("items", mongoDatabase)
        {
        }
    }
}