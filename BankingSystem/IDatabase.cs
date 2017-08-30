using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    interface IDatabase
    {
        void Add(User user, Account account, Card card);
        void Delete(User user);
        void GetUser(long id);
        void GetAccount(long id);
    }
}
