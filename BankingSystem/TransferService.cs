
using System;

namespace BankingSystem
{
    public class TransferService : ITransferService
    {
        public long Id { get; set; }
        public bool Transfer(Account account, Account transferAccount, double Money)
        {
            if (account.MoneyOnAccount < Money)
            {
                return false;
            }
            else
            {
                account.MoneyOnAccount -= Money;
                if (transferAccount != null)
                {
                    transferAccount.MoneyOnAccount += Money;
                }
                return true;
            }
        }
    }
}