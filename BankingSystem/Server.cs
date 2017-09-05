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
        List<string> result;
        public Server()
        {
            atm = new ATM(new ATM.Handler(PickLanguages));
            notification = new Notification(this);
            refill = new RefillService();
            withdraw = new WithdrawService();
            registration = new RegistrationService();
            transfer = new TransferService();
            dataBase = new DatabaseManagementSystem();
            activeUser = new BankingSystem.User();
            result = new List<string>();
        }

        public List<string> Handler(string command)
        {
            if (command.Contains("RU"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                return result;
            }
            else if (command.Contains("EN"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-EN");
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                return result;
            }
            else if (command.Contains("KZ"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("kk-KZ");
                result.Add("1 - " + Resource.strings.Login);
                result.Add("2 - " + Resource.strings.SignUp);
                return result;
            }
            else if (command.Contains(Resource.strings.Login))
            {
                result.Clear();
                result.Add(Resource.strings.IIN + ": ");
                atm.EventHandler -= Handler;
                atm.EventHandler += Input;
                return result;
            }
            else if (command.Contains(Resource.strings.SignUp))
            {
                result.Clear();
                registration.registration(activeUser);
                dataBase.Add(activeUser);
                result.Add("1 - " + Resource.strings.ViewMaps);
                result.Add("2 - " + Resource.strings.SubmitRequest);
                return result;
            }
            else if (command.Contains(Resource.strings.ViewMaps))
            {
                if (dataBase.GetCards(activeUser).Count == 0)
                {
                    result.Clear();
                    for (int i = 0; i < dataBase.GetCards(activeUser).Count; i++)
                    {
                        result.Add((i + 1) + " - " + Resource.strings.Map + i);
                    }
                }
                result.Add(Resource.strings.CardsNotFound + "\n1 - " + Resource.strings.ViewMaps);
                result.Add("2 - " + Resource.strings.SubmitRequest);
                return result;
            }
            return result;
        }

        public List<string> PickLanguages(string s)
        {
            List<string> str = new List<string> { "1 - RU", "2 - EN", "3 - KZ" };
            atm.EventHandler -= PickLanguages;
            atm.EventHandler += Handler;
            return str;
        }
        public void Start()
        {
            atm.HandlerCommand();
        }

        private List<string> Input(string IIN)
        {
            List<string> result = new List<string>();
            activeUser = dataBase.GetUser(IIN);
            if (activeUser == null)
            {
                result.Add(Resource.strings.IINNotFound);
                result.Add(Resource.strings.IIN + ": ");
                return result;
            }
            result.Add("1 - "+Resource.strings.ViewMaps);
            result.Add("2 - " + Resource.strings.SubmitRequest);
            atm.EventHandler -= Input;
            atm.EventHandler += Handler;
            return result;
        }
    }
}
