using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class Server
    {
        public long Id { get; set; }
        public delegate void Notify(object obj);
        public event Notify changeAccount;
        private TelegramBot bot;
        private ATM atm;
        private INotificationService notification;
        private IRefillService refill;
        private IWithdrawService withdraw;
        private IRegistrationService registration;
        private ITransferService transfer;
        private IDatabaseManagementSystem dataBase;
        private User activeUser;
        private Card activeCard;
        private Account activeAccount;
        private List<string> result;

        private int indexMenu = 0;
        private bool firstCall = true;
        private bool firstTransferCall = true;
        private int transferSum = 0;
        public Server()
        {
            atm = new ATM(new ATM.Handler(Handler));
            notification = new Notification(this);
            bot = new TelegramBot();
            refill = new RefillService();
            withdraw = new WithdrawService();
            registration = new RegistrationService();
            transfer = new TransferService();
            dataBase = new DatabaseManagementSystem();
            activeUser = new BankingSystem.User();
            activeCard = new Card();
            activeAccount = new Account(0);
            result = new List<string>();
            //bot.Start_Bot();
            
        }

        public List<string> Handler(string command)
        {
            atm.EventHandler -= Input;
            if (firstCall)
            {
                firstCall = false;
                result.Add("1 - RU");
                result.Add("2 - EN");
                result.Add("3 - KZ");
                result.Add("4 - EXIT");
                return result;
            }

            if (command.Contains(Resource.strings.Login))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += Input;
            }

            else if (command.Contains(Resource.strings.SignUp))
            {
                registration.registration(activeUser);
                dataBase.Add(activeUser);
                activeUser = new User();
            }

            else if (command.Contains(Resource.strings.SubmitRequest))
            {
                activeUser.Cards.Add(new Card());
            }

            else if (command.Contains(Resource.strings.Map))
            {
                activeCard = dataBase.GetCards(activeUser)[Int32.Parse(command.Remove(1)) - 1];
            }

            else if (command.Contains(Resource.strings.Delete) && command.Contains(Resource.strings.Account))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += DeleteAccount;
            }

            else if (command.Contains(Resource.strings.Account) && indexMenu == 5)
            {
                if (activeCard.Accounts.Count != 0)
                    activeAccount = activeCard.Accounts[Int32.Parse(command.Remove(1)) - 1];
            }

            else if (command.Contains(Resource.strings.Replenish))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += ReplenishAccount;
            }

            else if (command.Contains(Resource.strings.Withdraw))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += WithdrawAccount;
            }

            else if (command.Contains(Resource.strings.Transfer))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += TransferAccount;
            }

            else if (indexMenu == 5)
            {
                bool findCurrency = false;
                Сurrency currency = 0;
                for (int i = 0; i < 13; i++)
                {
                    string str = " " + (Сurrency)i;
                    if (command.Contains(str))
                    {
                        currency = (Сurrency)i;
                        findCurrency = true;
                    }
                }
                if (findCurrency)
                {
                    activeCard.Accounts.Add(new Account(currency));
                }
            }

            else if (command.Contains("RU") || command.Contains("EN") || command.Contains("KZ"))
            {
                SetLanguage(command);
                return result;
            }
            else if (command.Contains("EXIT"))
            {
                (dataBase as DatabaseManagementSystem).Save();
            }

            FormMenu(command);
            return result;
        }

        private void FormMenu(string command)
        {
            if (command.Contains(Resource.strings.Back))
            {
                Back();
            }
            else if (command.Contains(Resource.strings.Login))
            {
                result.Clear();
                result.Add(Resource.strings.IIN + ": ");
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.SignUp))
            {
                result.Clear();
                result.Add("1 - " + Resource.strings.ViewMaps);
                result.Add("2 - " + Resource.strings.SubmitRequest);
                result.Add("3 - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.ViewMaps))
            {
                result.Clear();
                if (dataBase.GetCards(activeUser).Count != 0)
                {
                    int i = 0;
                    for (; i < dataBase.GetCards(activeUser).Count; i++)
                    {
                        result.Add((i + 1) + " - " + Resource.strings.Map + i);
                    }
                    result.Add((i + 1) + " - " + Resource.strings.Back);
                    indexMenu++;
                }
                else
                {
                    result.Add(Resource.strings.CardsNotFound + "\n1 - " + Resource.strings.ViewMaps);
                    result.Add("2 - " + Resource.strings.SubmitRequest);
                    result.Add("3 - " + Resource.strings.Back);
                }
            }
            else if (command.Contains(Resource.strings.SubmitRequest))
            {
                result.Clear();
                int i = 0;
                for (; i < dataBase.GetCards(activeUser).Count; i++)
                {
                    result.Add((i + 1) + " - " + Resource.strings.Map + i);
                }
                result.Add((i + 1) + " - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.Map))
            {
                result.Clear();
                result.Add("1 - " + Resource.strings.View + " " + Resource.strings.Account);
                result.Add("2 - " + Resource.strings.Add + " " + Resource.strings.Account);
                result.Add("3 - " + Resource.strings.Delete + " " + Resource.strings.Account);
                result.Add("4 - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.View) && command.Contains(Resource.strings.Account))
            {
                result.Clear();
                if (activeCard.Accounts.Count != 0)
                {
                    int i = 0;
                    for (; i < activeCard.Accounts.Count; i++)
                    {
                        result.Add((i + 1) + " - " + Resource.strings.Account + i);
                    }
                    result.Add((i + 1) + " - " + Resource.strings.Back);
                    indexMenu++;
                }
                else
                {
                    result.Add(Resource.strings.AccountsNotFound + "\n1 - " + Resource.strings.View + " " + Resource.strings.Account);
                    result.Add("2 - " + Resource.strings.Add + " " + Resource.strings.Account);
                    result.Add("3 - " + Resource.strings.Delete + " " + Resource.strings.Account);
                    result.Add("4 - " + Resource.strings.Back);
                }
            }
            else if (command.Contains(Resource.strings.Add) && command.Contains(Resource.strings.Account))
            {
                FormCurrency();
                indexMenu++;
            }

            else if (command.Contains(Resource.strings.Delete) && command.Contains(Resource.strings.Account))
            {
                result.Clear();
                result.Add(Resource.strings.AccountNumber + ": ");
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.Account))
            {
                result.Clear();
                result.Add("1 - " + Resource.strings.Replenish);
                result.Add("2 - " + Resource.strings.Withdraw);
                result.Add("3 - " + Resource.strings.Transfer);
                result.Add("4 - " + Resource.strings.Info);
                result.Add("5 - " + Resource.strings.Back);
                indexMenu++;
            }

            else if (command.Contains(Resource.strings.Replenish))
            {
                result.Clear();
                result.Add(Resource.strings.Sum + ": ");
                indexMenu++;
            }

            else if (command.Contains(Resource.strings.Withdraw))
            {
                result.Clear();
                result.Add(Resource.strings.Sum + ": ");
                indexMenu++;
            }

            else if (command.Contains(Resource.strings.Transfer))
            {
                result.Clear();
                result.Add(Resource.strings.Sum + ": ");
                indexMenu++;
            }

            else if (command.Contains(Resource.strings.Info))
            {
                result.Clear();
                result.Add("1 - " + Resource.strings.Back);
                result.Add(Resource.strings.AccountNumber + ": " + activeAccount.AccountNumber);
                result.Add(Resource.strings.Sum + ": " + activeAccount.MoneyOnAccount + " " + activeAccount.Currency);
                indexMenu++;
            }

            else if (indexMenu == 5)
            {
                bool findCurrency = false;
                for (int i = 0; i < 13; i++)
                {
                    string str = " " + (Сurrency)i;
                    if (command.Contains(str))
                    {
                        findCurrency = true;
                    }
                }
                if (findCurrency)
                {
                    Back();
                }
            }
            else if (command.Contains("EXIT"))
            {
                result = null;
            }

        }

        private void FormCurrency()
        {
            result.Clear();
            result.Add("1 - " + Resource.strings.Back);
            for (int i = 1; i < 14; i++)
            {
                result.Add((i + 1) + " - " + (Сurrency)(i - 1));
            }
        }

        public void Start()
        {
            atm.HandlerCommand();
        }

        private List<string> Input(string IIN)
        {
            result.Clear();
            activeUser = dataBase.GetUser(IIN);
            if (activeUser == null)
            {
                result.Add(Resource.strings.IINNotFound);
                result.Add(Resource.strings.IIN + ": ");
                return result;
            }
            result.Add("1 - " + Resource.strings.ViewMaps);
            result.Add("2 - " + Resource.strings.SubmitRequest);
            result.Add("3 - " + Resource.strings.Back);

            atm.EventHandler -= Input;
            atm.EventHandler += Handler;
            return result;
        }

        private List<string> ReplenishAccount(string sum)
        {
            result.Clear();
            int sumAdd = 0;
            if (int.TryParse(sum, out sumAdd))
            {

                if (refill.Refill(activeAccount, sumAdd))
                {
                    DateTime dataTime = DateTime.Now;
                    string str = String.Format("{0}: {1}\n{2}: {3}\n{4}: {5} {6}\n{7}: {8} {9}\n{10}: {11}", Resource.strings.CardNumber,
                        activeCard.NumberCard, Resource.strings.AccountNumber, activeAccount.AccountNumber,
                        Resource.strings.Replenish, sumAdd, activeAccount.Currency,
                        Resource.strings.Available, activeAccount.MoneyOnAccount, activeAccount.Currency,
                        Resource.strings.Date, dataTime);
                    activeUser.lastOperations.Add(str);
                    string subject = "SDPBank";
                    Сontainer container = new Сontainer() { user = activeUser, subject = subject, body = str };
                    Thread threadForNotification = new Thread(new ParameterizedThreadStart(changeAccount));
                    threadForNotification.Start(container);
                    Back();
                    atm.EventHandler -= ReplenishAccount;
                    atm.EventHandler += Handler;
                }
                else
                {
                    result.Add(Resource.strings.Error);
                    result.Add(Resource.strings.Sum + ": ");
                }
            }
            else
            {
                result.Add(Resource.strings.Error);
                result.Add(Resource.strings.Sum + ": ");
            }
            return result;
        }

        private List<string> TransferAccount(string sumOrNumber)
        {
            result.Clear();
            int sum = 0;
            if (firstTransferCall)
            {
                if (int.TryParse(sumOrNumber, out sum))
                {
                    transferSum = sum;
                    result.Add(Resource.strings.Transfer + " " + Resource.strings.ToAccount + ": ");
                    firstTransferCall = false;
                }
                else
                {
                    result.Add(Resource.strings.Error);
                    result.Add(Resource.strings.Sum + ": ");
                }
            }
            else
            {
                long accountNumber;
                if (long.TryParse(sumOrNumber, out accountNumber))
                {
                    Account acceptingAccount = (dataBase as DatabaseManagementSystem).FindAccount(accountNumber);
                    if (transfer.Transfer(activeAccount, acceptingAccount, transferSum))
                    {
                        DateTime dataTime = DateTime.Now;
                        string body = String.Format("{0}: {1}\n{2}: {3}\n{4}: {5} {6}\n{7} {8}: {9}\n{10}: {11} {12}\n{13}: {14}",
                            Resource.strings.CardNumber, activeCard.NumberCard,
                            Resource.strings.AccountNumber, activeAccount.AccountNumber,
                            Resource.strings.Transfer, transferSum, activeAccount.Currency,
                            Resource.strings.Transfer, Resource.strings.ToAccount, accountNumber,
                            Resource.strings.Available, activeAccount.MoneyOnAccount, activeAccount.Currency,
                            Resource.strings.Date, dataTime);
                        activeUser.lastOperations.Add(body);
                        string subject = "SDPBank";
                        Сontainer container = new Сontainer() { user = activeUser, subject = subject, body = body };
                        Thread threadForNotification = new Thread(new ParameterizedThreadStart(changeAccount));
                        threadForNotification.Start(container);
                        Back();
                        atm.EventHandler -= TransferAccount;
                        atm.EventHandler += Handler;
                    }
                    else
                    {
                        result.Add(Resource.strings.Error);
                        result.Add(Resource.strings.Sum + ": ");
                    }
                    firstTransferCall = true;
                }
            }
            return result;
        }

        private List<string> WithdrawAccount(string sum)
        {
            result.Clear();
            int sumWithdraw = 0;
            if (int.TryParse(sum, out sumWithdraw))
            {

                if (withdraw.Withdraw(activeAccount, sumWithdraw))
                {
                    DateTime dataTime = DateTime.Now;
                    string str = String.Format("{0}: {1}\n{2}: {3}\n{4}: {5} {6}\n{7}: {8} {9}\n{10}: {11}", Resource.strings.CardNumber,
                        activeCard.NumberCard, Resource.strings.AccountNumber, activeAccount.AccountNumber,
                        Resource.strings.Withdraw, sumWithdraw, activeAccount.Currency,
                        Resource.strings.Available, activeAccount.MoneyOnAccount, activeAccount.Currency,
                        Resource.strings.Date, dataTime);
                    string subject = "SDPBank";
                    activeUser.lastOperations.Add(str);
                    Сontainer container = new Сontainer() { user = activeUser, subject = subject, body = str };
                    Thread threadForNotification = new Thread(new ParameterizedThreadStart(changeAccount));
                    threadForNotification.Start(container);
                    Back();
                    atm.EventHandler -= WithdrawAccount;
                    atm.EventHandler += Handler;
                }
                else
                {
                    result.Add(Resource.strings.Error);
                    result.Add(Resource.strings.Sum + ": ");
                }
            }
            else
            {
                result.Add(Resource.strings.Error);
                result.Add(Resource.strings.Sum + ": ");
            }
            return result;
        }

        private List<string> DeleteAccount(string accountNubmerStr)
        {
            result.Clear();
            long accountNumber = 0;
            if (long.TryParse(accountNubmerStr, out accountNumber))
            {
                for (int i = 0; i < activeCard.Accounts.Count; i++)
                {
                    if (accountNumber == activeCard.Accounts[i].AccountNumber)
                    {
                        activeCard.Accounts.Remove(activeCard.Accounts[i]);
                        Back();
                        atm.EventHandler -= DeleteAccount;
                        atm.EventHandler += Handler;
                        return result;
                    }
                }
            }
            result.Add(Resource.strings.AccountNumberNotFound);
            result.Add(Resource.strings.IIN + ": ");
            return result;
        }

        private void Back()
        {
            if (indexMenu == 1)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - RU");
                result.Add("2 - EN");
                result.Add("3 - KZ");
                result.Add("4 - EXIT");
            }
            else if (indexMenu == 2)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                result.Add("3 - " + Resource.strings.Back);
            }
            else if (indexMenu == 3)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - " + Resource.strings.ViewMaps);
                result.Add("2 - " + Resource.strings.SubmitRequest);
                result.Add("3 - " + Resource.strings.Back);
            }
            else if (indexMenu == 4)
            {
                indexMenu--;
                result.Clear();
                int i = 0;
                for (; i < dataBase.GetCards(activeUser).Count; i++)
                {
                    result.Add((i + 1) + " - " + Resource.strings.Map + i);
                }
                result.Add((i + 1) + " - " + Resource.strings.Back);
            }
            else if (indexMenu == 5)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - " + Resource.strings.View + " " + Resource.strings.Account);
                result.Add("2 - " + Resource.strings.Add + " " + Resource.strings.Account);
                result.Add("3 - " + Resource.strings.Delete + " " + Resource.strings.Account);
                result.Add("4 - " + Resource.strings.Back);
            }
            else if (indexMenu == 6)
            {
                indexMenu--;
                result.Clear();
                int i = 0;
                for (; i < activeCard.Accounts.Count; i++)
                {
                    result.Add((i + 1) + " - " + Resource.strings.Account + i);
                }
                result.Add((i + 1) + " - " + Resource.strings.Back);
            }
            else if (indexMenu == 7)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - " + Resource.strings.Replenish);
                result.Add("2 - " + Resource.strings.Withdraw);
                result.Add("3 - " + Resource.strings.Transfer);
                result.Add("4 - " + Resource.strings.Info);
                result.Add("5 - " + Resource.strings.Back);
            }
        }

        private void SetLanguage(string language)
        {
            if (language.Contains("RU"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                result.Clear();
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                result.Add("3 - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (language.Contains("EN"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-EN");
                result.Clear();
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                result.Add("3 - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (language.Contains("KZ"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("kk-KZ");
                result.Clear();
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                result.Add("3 - " + Resource.strings.Back);
                indexMenu++;
            }
        }
    }
}
