using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class Database : IDatabase
    {
        public long Id { get; set; }
        List<User> users;
        List<Account> accounts;
        List<Card> cards;
        public void Add(User user, Account account, Card card)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public void GetAccount(long id)
        {
            throw new NotImplementedException();
        }

        public void GetUser(long id)
        {
            throw new NotImplementedException();
        }
    }
}
