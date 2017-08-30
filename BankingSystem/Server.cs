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

        public Server()
        {
            atm = new ATM(new ATM.Handler(Handler));
            INotificationService notification = new Notification(this);
            IRefillService refill = new RefillService();
            IWithdrawService withdraw = new WithdrawService();
            IRegistrationService registration = new RegistrationService();
            ITransferService transfer = new TransferService();
        }

        public List<string> Handler(int command)
        {
            return null;
        }
    }
}
