using System;
using System.Collections.Generic;
using System.Linq;
using Mobile.Services;

namespace Mobile.Views
{
    public class TransactionsProvider
    {
        private readonly GetUserResponse response;

        public TransactionsProvider(GetUserResponse response)
        {
            this.response = response;
        }

        public List<Transaction> GetTransactions()
        {
            return response.Transactions.ToList();
        }
    }
}