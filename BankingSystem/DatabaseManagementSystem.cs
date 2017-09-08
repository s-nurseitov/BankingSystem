using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace BankingSystem
{
    public class DatabaseManagementSystem : IDatabaseManagementSystem
    {
        public long Id { get; set; }
        List<User> users;
        public DatabaseManagementSystem()
        {
            users = new List<User>();

            XmlDocument doc = new XmlDocument();
            doc.Load("Database.xml");
            XmlElement element = doc.DocumentElement;

            foreach (XmlNode nodeUser in element)
            {
                User user = new User();
                user.Id = long.Parse(nodeUser["Id"].InnerText);
                user.IIN = nodeUser["IIN"].InnerText;
                user.PhoneNumber = nodeUser["Number"].InnerText;
                user.FullName = nodeUser["Name"].InnerText;
                user.Email = nodeUser["Email"].InnerText;
                foreach (XmlNode nodeCard in nodeUser["Cards"].ChildNodes)
                {
                    Card card = new Card();
                    card.Id = long.Parse(nodeCard["Id"].InnerText);
                    card.NumberCard = long.Parse(nodeCard["Number"].InnerText);
                    card.CVV = int.Parse(nodeCard["CVV"].InnerText);
                    foreach (XmlNode nodeAccount in nodeCard["Accounts"].ChildNodes)
                    {
                        Сurrency currency = (Сurrency)Int32.Parse(nodeAccount["Currency"].InnerText);
                        Account account = new Account(currency);
                        account.AccountNumber = long.Parse(nodeAccount["Number"].InnerText);
                        account.Id = long.Parse(nodeAccount["Id"].InnerText);
                        account.MoneyOnAccount = double.Parse(nodeAccount["Money"].InnerText);
                        card.Accounts.Add(account);
                    }
                    user.Cards.Add(card);
                }
                users.Add(user);
            }
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Database.xml");
            XmlElement element = doc.DocumentElement;
            element.RemoveAll();

            for (int i = 0; i < users.Count; i++)
            {
                XmlElement user = doc.CreateElement("User");

                XmlElement name = doc.CreateElement("Name");
                XmlElement id = doc.CreateElement("Id");
                XmlElement number = doc.CreateElement("Number");
                XmlElement email = doc.CreateElement("Email");
                XmlElement iin = doc.CreateElement("IIN");

                XmlText nameText = doc.CreateTextNode(users[i].FullName);
                XmlText idText = doc.CreateTextNode(users[i].Id.ToString());
                XmlText numberText = doc.CreateTextNode(users[i].PhoneNumber);
                XmlText emailText = doc.CreateTextNode(users[i].Email);
                XmlText iinText = doc.CreateTextNode(users[i].IIN);

                XmlElement cards = doc.CreateElement("Cards");
                for(int j = 0; j < users[i].Cards.Count; j++)
                {
                    XmlElement card = doc.CreateElement("Card");

                    XmlElement idCard = doc.CreateElement("Id");
                    XmlElement numberCard = doc.CreateElement("Number");
                    XmlElement cvv = doc.CreateElement("CVV");

                    XmlText idCardText = doc.CreateTextNode(users[i].Cards[j].Id.ToString());
                    XmlText numberCardText = doc.CreateTextNode(users[i].Cards[j].NumberCard.ToString());
                    XmlText cvvText = doc.CreateTextNode(users[i].Cards[j].CVV.ToString());

                    XmlElement accounts = doc.CreateElement("Accounts"); 
                    for(int k = 0; k < users[i].Cards[j].Accounts.Count; k++)
                    {
                        XmlElement account = doc.CreateElement("Account");

                        XmlElement idAccount = doc.CreateElement("Id");
                        XmlElement accountNumber = doc.CreateElement("Number");
                        XmlElement money = doc.CreateElement("Money");
                        XmlElement currency = doc.CreateElement("Currency");

                        XmlText idAccountText = doc.CreateTextNode(users[i].Cards[j].Accounts[k].Id.ToString());
                        XmlText accountNumberText = doc.CreateTextNode(users[i].Cards[j].Accounts[k].AccountNumber.ToString());
                        XmlText moneyText = doc.CreateTextNode(users[i].Cards[j].Accounts[k].MoneyOnAccount.ToString());
                        XmlText currencyText = doc.CreateTextNode(users[i].Cards[j].Accounts[k].Currency.ToString());

                        idAccount.AppendChild(idAccountText);
                        accountNumber.AppendChild(accountNumberText);
                        money.AppendChild(moneyText);
                        currency.AppendChild(currencyText);

                        account.AppendChild(idAccount);
                        account.AppendChild(accountNumber);
                        account.AppendChild(money);
                        account.AppendChild(currency);

                        accounts.AppendChild(account);
                    }
                    idCard.AppendChild(idCardText);
                    numberCard.AppendChild(numberCardText);
                    cvv.AppendChild(cvvText);

                    card.AppendChild(idCard);
                    card.AppendChild(numberCard);
                    card.AppendChild(cvv);
                    card.AppendChild(accounts);

                    cards.AppendChild(card);
                }

                name.AppendChild(nameText);
                id.AppendChild(idText);
                number.AppendChild(numberText);
                email.AppendChild(emailText);
                iin.AppendChild(iinText);

                user.AppendChild(name);
                user.AppendChild(id);
                user.AppendChild(number);
                user.AppendChild(email);
                user.AppendChild(iin);
                user.AppendChild(cards);

                element.AppendChild(user);
            }
            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString().Remove(AppDomain.CurrentDomain.BaseDirectory.Length-10) + "Database.xml");
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
