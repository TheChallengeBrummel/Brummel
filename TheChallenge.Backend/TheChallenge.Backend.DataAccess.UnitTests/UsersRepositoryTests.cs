using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheChallenge.Backend.DataAccess.Model;
using TheChallenge.Backend.DataAccess.Repository;
using Xunit;

namespace TheChallenge.Backend.DataAccess.UnitTests
{
    public class UsersRepositoryTests
    {
        private static readonly string connectionString = "mongodb://the-challenge-db:lz9ym9rlLQF49PEhogaSi3OLq96JMajgGXvPwZLEkzJMKfmq6WawyXVPT87ZJBUo2gSHkEDyxJ1Kn7xsEWtEvA==@the-challenge-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@the-challenge-db@&retrywrites=false";
        private IOptions<MongoSettings> options;

        public UsersRepositoryTests()
        {
            options = Options.Create(new MongoSettings
            {
                ConnectionString = connectionString,
                Database = "the-challenge-db"
            });
        }

        [Fact]
        public async Task Insert_AddNewUserAsync()
        {
            var context = new DatabaseContext(options);
            var usersRepository = new UsersRepository(context);
            await usersRepository.Insert(new User
            {
                Name = "UnitTest User",
                Transactions = new List<Transaction>(0)
            });
        }

        [Fact]
        public async Task Update_UpdatesUserTransactions()
        {
            var context = new DatabaseContext(options);
            var usersRepository = new UsersRepository(context);
            var transaction = new Transaction
            {
                Amount = 10
            };
            var user = new User
            {
                Name = "UnitTest User",
                Transactions = new List<Transaction>(0)
            };
            var userId = await usersRepository.Insert(user);

            var insertedUser = await usersRepository.Get(userId);
            insertedUser.Transactions.Add(transaction);
            await usersRepository.Update(insertedUser);

            var updatedUser = await usersRepository.Get(userId);
            updatedUser.Transactions.Should().NotBeEmpty();
            updatedUser.Transactions[0].Amount.Should().Be(10);
        }
    }
}