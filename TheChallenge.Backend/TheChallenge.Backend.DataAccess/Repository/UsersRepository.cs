using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TheChallenge.Backend.DataAccess.Model;

namespace TheChallenge.Backend.DataAccess.Repository
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(DatabaseContext mongoDatabase) : base("users", mongoDatabase)
        {
        }
    }
}