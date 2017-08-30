
using System;

namespace BankingSystem
{
    public class TransferService : ITransferService
    {
        public long Id { get; set; }
        public bool Transfer(Account account, Account transferaccount, double Money)
        {
            if (account.MoneyOnAccount < Money)
            {
                return false;
            }
            else
            {
                transferaccount.MoneyOnAccount += Money;
                account.MoneyOnAccount -= Money;
                return true;
            }
        }
    }
}