using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public interface IDatabaseManagementSystem
    {
        void Add(User user);
        void Delete(User user);
        User GetUser(string id);
    }
}
