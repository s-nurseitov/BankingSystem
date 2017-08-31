using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Languages languages;
        private List<List<string>> activelanguage;
        public Server()
        {
            atm = new ATM(new ATM.Handler(PickLanguages));
            languages = new Languages();
            INotificationService notification = new Notification(this);
            IRefillService refill = new RefillService();
            IWithdrawService withdraw = new WithdrawService();
            IRegistrationService registration = new RegistrationService();
            ITransferService transfer = new TransferService();
        }

        public List<string> Handler(int command)
        {
            activelanguage = languages.GetActiveLanguage(command);

            return activelanguage[0];
        }

        public List<string> PickLanguages(int i)
        {
            List<string> str = new List<string> { "RU", "EN", "KZ" };
            atm.EventHandler -= PickLanguages;
            atm.EventHandler += Handler;
            return str;
        }
        public void Start()
        {
            atm.HandlerCommand();
        }
    }
}
