using System;

namespace BankingSystem
{
    public class Account
    {
        public long Id { get; set; }
        public long AccountNumber { get; set; }
        public long MoneyOnAccount { get; set; }
        public Enum Currency { get; set; }
    }
}