using System;

namespace BankingSystem
{
    public class WithdrawService : IWithdrawService
    {
        public long Id { get; set; }

        public bool Withdraw(Account account, int sum)
        {
            // Тут может быть своя политики банка (коммисия).
            if (account.MoneyOnAccount < sum)
            {
                return false;
            }
            account.MoneyOnAccount -= sum;
            return true;
        }
    }
}