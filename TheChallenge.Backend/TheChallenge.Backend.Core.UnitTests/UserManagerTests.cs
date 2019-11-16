using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using TheChallenge.Backend.API.Enums;
using TheChallenge.Backend.DataAccess;
using TheChallenge.Backend.DataAccess.Model;
using TheChallenge.Backend.DataAccess.Repository;
using Xunit;

namespace TheChallenge.Backend.Core.UnitTests
{
    public class UserManagerTests
    {
        private static readonly string connectionString = "mongodb://the-challenge-db:lz9ym9rlLQF49PEhogaSi3OLq96JMajgGXvPwZLEkzJMKfmq6WawyXVPT87ZJBUo2gSHkEDyxJ1Kn7xsEWtEvA==@the-challenge-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@the-challenge-db@&retrywrites=false";
        private IOptions<MongoSettings> options;

        public UserManagerTests()
        {
            options = Options.Create(new MongoSettings
            {
                ConnectionString = connectionString,
                Database = "the-challenge-db"
            });
        }

        [Fact]
        public async Task AddMoneyToUser_AddsMoneyAsync()
        {
            var databaseContext = new DatabaseContext(options);
            var usersRepository = new UsersRepository(databaseContext);
            var userManager = new UserManager(usersRepository);
            var newUser = new User
            {
                Name = "UnitTest_AddMoneyToUser_AddsMoney",
                Transactions = new List<Transaction>(0)
            };
            var userId = await usersRepository.Insert(newUser);

            await userManager.AddMoneyToUser(userId, 123, "Reason: Unit Test");

            var resultingUser = await usersRepository.Get(userId);

            resultingUser.Transactions[0].Amount.Should().Be(123);
        }

        [Fact]
        public async Task GetUser_UserDoesNotExist_ReturnsNewUser()
        {
            var userId = Guid.NewGuid();
            var databaseContext = new DatabaseContext(options);
            var usersRepository = new UsersRepository(databaseContext);
            var userManager = new UserManager(usersRepository);

            var user = await userManager.GetUserAsync(userId.ToString());

            user.Id.Should().Be(userId);
            user.Transactions.Should().BeEmpty();
        }
    }
}