using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class DatabaseManagementSystem : IDatabaseManagementSystem
    {
        public long Id { get; set; }
        List<User> users;
        public void Add(User user)
        {
            this.users.Add(user);
        }

        public void Delete(User user)
        {
            bool isAppear = users.Contains(user);
            int index;
            if (isAppear)
            {
                index = users.IndexOf(user);
                users.RemoveAt(index);
            }
        }


        public User GetUser(long iin)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].IIN == iin)
                    return users[i];
            }
            return null;
        }
    }
}
