using System.Collections.Generic;

namespace TheChallenge.Backend.DataAccess.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}