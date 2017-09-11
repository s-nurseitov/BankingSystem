using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem
{
    public class DatabaseManagementSystem : IDatabaseManagementSystem
    {
        public long Id { get; set; }
        List<User> users;
        public DatabaseManagementSystem()
        {
            users = new List<User>();
            if (File.Exists("BankDatabase.json"))
            {
                using (FileStream fileStream = new FileStream("BankDatabase.json", FileMode.Open))
                {
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    string str = Encoding.Default.GetString(bytes);
                    if (JsonConvert.DeserializeAnonymousType(str,users)!=null)
                    {
                        users = JsonConvert.DeserializeAnonymousType(str, users);
                    }
                }  
            }      
        }

        public void Save()
        {
            using (FileStream fileStream = new FileStream("BankDatabase.json", FileMode.OpenOrCreate))
            {
                string jsonusers="";
                jsonusers=JsonConvert.SerializeObject(users);
                byte[] bytes = Encoding.Default.GetBytes(jsonusers);
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

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


        public User GetUser(string iin)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].IIN == iin)
                    return users[i];
            }
            return null;
        }

        public List<Card> GetCards(User user)
        {
            return user.Cards;
        }

        public Account FindAccount(long accountNumber)
        {
            foreach (User user in users)
            {
                foreach (Card card in user.Cards)
                {
                    foreach (Account account in card.Accounts)
                    {
                        if (accountNumber == account.AccountNumber)
                        {
                            return account;
                        }
                    }
                }
            }
            return null;
        }
        public User FindPhoneNumber(string phoneNumber)
        {
            foreach (User user in users)
            {
                if (user.PhoneNumber == phoneNumber)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
