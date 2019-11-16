using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheChallenge.Backend.Core.Models;
using TheChallenge.Backend.DataAccess.Repository;

namespace TheChallenge.Backend.Core
{
    public class UserManager
    {
        private readonly UsersRepository usersRepository;

        public UserManager(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task AddMoneyToUser(Guid userId, double amount, string reason)
        {
            var user = await usersRepository.Get(userId);
            user.Transactions.Add(new DataAccess.Model.Transaction
            {
                Amount = amount,
                Description = reason,
                CreatedOn = DateTime.Now
            });

            await usersRepository.Update(user);
        }

        public async Task BuyItem(Guid userId, double amount, string reason)
        {
            var user = await usersRepository.Get(userId);
            user.Transactions.Add(new DataAccess.Model.Transaction
            {
                Amount = amount < 0 ? amount : amount * -1,
                Description = reason,
                CreatedOn = DateTime.Now
            });

            await usersRepository.Update(user);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var dbUser = await usersRepository.Get(new Guid(userId));
            if (dbUser == null)
            {
                dbUser = new DataAccess.Model.User
                {
                    Id = Guid.Parse(userId),
                    Name = "Automatic User",
                    Transactions = new List<DataAccess.Model.Transaction>(0)
                };
                await usersRepository.Insert(dbUser);
            }
            var coreTransactions = MapTransactions(dbUser.Transactions);
            return new User
            {
                Id = dbUser.Id.Value,
                Name = dbUser.Name,
                Transactions = coreTransactions,
                Balance = CalculateBalance(coreTransactions)
            };
        }

        public async Task GenerateDemoData()
        {
            var id = Guid.Parse("8D00D354-1BDD-4E30-BA4A-779B0B646B14");
            var newUser = new DataAccess.Model.User
            {
                Id = id,
                Name = "Max Muster",
                Transactions = new List<DataAccess.Model.Transaction>
                {
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 1, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 2, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 3, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 4, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 5, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 6, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 7, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 8, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 9, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 10, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 11, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount = 25, CreatedOn = new DateTime(2019, 12, 1), Description="Taschengeld"},
                    new DataAccess.Model.Transaction { Amount=-10, CreatedOn =new DateTime(2019, 1, 3), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-12.5, CreatedOn =new DateTime(2019, 1, 15), Description=ItemTypes.Food.ToString() }, // January = 22.5
                    new DataAccess.Model.Transaction { Amount=-5.5, CreatedOn =new DateTime(2019, 2, 3), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-3.0, CreatedOn = new DateTime(2019, 2, 7) , Description=ItemTypes.Food.ToString() }, // February = 8.5
                    new DataAccess.Model.Transaction { Amount=-13.0, CreatedOn = new DateTime(2019, 3, 3), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-23.0, CreatedOn = new DateTime(2019, 3, 13), Description=ItemTypes.Cloths.ToString() }, // March = 36
                    new DataAccess.Model.Transaction { Amount=-50.0, CreatedOn = new DateTime(2019, 4, 13), Description=ItemTypes.Toys.ToString() }, // April = 50
                    new DataAccess.Model.Transaction { Amount=-4.5, CreatedOn = new DateTime(2019, 5, 2) , Description=ItemTypes.Food.ToString() },
                    new DataAccess.Model.Transaction { Amount=-3.5, CreatedOn = new DateTime(2019, 5, 3) , Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-7.0, CreatedOn = new DateTime(2019, 5, 5), Description=ItemTypes.Food.ToString() }, // May = 16
                    new DataAccess.Model.Transaction { Amount=-5.5, CreatedOn = new DateTime(2019, 6, 9) , Description=ItemTypes.Food.ToString() },
                    new DataAccess.Model.Transaction { Amount=-16.0, CreatedOn = new DateTime(2019, 6, 5) , Description=ItemTypes.Toys.ToString() }, // June = 22.5
                    new DataAccess.Model.Transaction { Amount=-5.5, CreatedOn = new DateTime(2019, 7, 23), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-7.5, CreatedOn = new DateTime(2019, 7, 8) , Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-14.0, CreatedOn = new DateTime(2019, 7, 1) , Description=ItemTypes.Toys.ToString() }, // July = 27
                    new DataAccess.Model.Transaction { Amount=-22.5, CreatedOn = new DateTime(2019, 8, 1), Description=ItemTypes.Cloths.ToString() }, // August 22.5
                    new DataAccess.Model.Transaction { Amount=-10.5, CreatedOn = new DateTime(2019, 9, 5) , Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-10.0, CreatedOn = new DateTime(2019, 9, 13), Description=ItemTypes.PrintMedia.ToString() }, // September 20.5
                    new DataAccess.Model.Transaction { Amount=-10.0, CreatedOn = new DateTime(2019, 10, 12), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-10.0, CreatedOn = new DateTime(2019, 10, 12), Description=ItemTypes.Candy.ToString() },
                    new DataAccess.Model.Transaction { Amount=-6.0, CreatedOn = new DateTime(2019, 10, 23), Description=ItemTypes.Candy.ToString() }, // October = 26
                    new DataAccess.Model.Transaction { Amount=-4.5, CreatedOn = new DateTime(2019, 11, 17), Description=ItemTypes.Food.ToString() },
                    new DataAccess.Model.Transaction { Amount=-4.5, CreatedOn = new DateTime(2019, 11, 17), Description=ItemTypes.Food.ToString() },
                    new DataAccess.Model.Transaction { Amount=-4.5, CreatedOn = new DateTime(2019, 11, 17), Description=ItemTypes.Food.ToString() },
                    new DataAccess.Model.Transaction { Amount=-4.5, CreatedOn = new DateTime(2019, 11, 17), Description=ItemTypes.Food.ToString() }, // November = 18
                }
            };
            try
            {
                await usersRepository.Insert(newUser);
            }
            catch (Exception)
            {
                await usersRepository.Update(newUser);
            }
        }

        private double CalculateBalance(List<Transaction> transactions)
        {
            return transactions.Sum(transaction => transaction.Amount);
        }

        private List<Transaction> MapTransactions(List<DataAccess.Model.Transaction> transactions)
        {
            return transactions.Select(t => new Transaction
            {
                Amount = t.Amount,
                CreatedOn = t.CreatedOn,
                Description = t.Description
            }).ToList();
        }
    }
}