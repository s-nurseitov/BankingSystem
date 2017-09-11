using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    //Repository pattern or Gateway
    public interface IDatabaseManagementSystem
    {
        void Add(User user);
        void Delete(User user);
        User GetUser(string id);
        List<Card> GetCards(User user);
    }
}
