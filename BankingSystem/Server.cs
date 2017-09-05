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

        public delegate bool Notify(User user, string subject, string body);
        public event Notify changeAccount;
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
        public Server()
        {
            atm = new ATM(new ATM.Handler(Handler));
            notification = new Notification(this);
            refill = new RefillService();
            withdraw = new WithdrawService();
            registration = new RegistrationService();
            transfer = new TransferService();
            dataBase = new DatabaseManagementSystem();
            activeUser = new BankingSystem.User();
            activeCard = new Card();
            activeAccount = new Account();
            result = new List<string>();
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
                return result;
            }
            if (command.Contains("RU") || command.Contains("EN") || command.Contains("KZ"))
            {
                SetLanguage(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Login))
            {
                atm.EventHandler -= Handler;
                atm.EventHandler += Input;
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.SignUp))
            {
                activeUser = new User();
                registration.registration(activeUser);
                dataBase.Add(activeUser);
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.ViewMaps))
            {
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.SubmitRequest))
            {
                activeUser.Cards.Add(new Card());
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Map))
            {
                activeCard = dataBase.GetCards(activeUser)[Int32.Parse(command.Remove(1)) - 1];
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.View) && command.Contains(Resource.strings.Account))
            {
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Add) && command.Contains(Resource.strings.Account))
            {
                activeCard.Accounts.Add(new Account());
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Delete) && command.Contains(Resource.strings.Account))
            {
                activeCard.Accounts.Add(new Account());
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Account))
            {
                activeAccount = activeCard.Accounts[Int32.Parse(command.Remove(1)) - 1];
                FormMenu(command);
                return result;
            }
            else if (command.Contains(Resource.strings.Replenish))
            {

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
                result.Clear();
                int i = 0;
                for (; i < activeCard.Accounts.Count; i++)
                {
                    result.Add((i + 1) + " - " + Resource.strings.Account + i);
                }
                result.Add((i + 1) + " - " + Resource.strings.Back);
                indexMenu++;
            }
            else if (command.Contains(Resource.strings.Delete) && command.Contains(Resource.strings.Account))
            {
                result.Clear();
                int i = 0;
                for (; i < activeCard.Accounts.Count; i++)
                {
                    result.Add((i + 1) + " - " + Resource.strings.Account + i);
                }
                result.Add(i + " - " + Resource.strings.Back);
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

        private void Back()
        {
            if (indexMenu == 1)
            {
                indexMenu--;
                result.Clear();
                result.Add("1 - RU");
                result.Add("2 - EN");
                result.Add("3 - KZ");
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
