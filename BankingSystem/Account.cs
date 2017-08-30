using System;

namespace BankingSystem
{
    public class Account
    {
        public long Id { get; set; }
        public long AccountNumber { get; set; }
        public double MoneyOnAccount { get; set; }
        public Enum Currency { get; set; }
    }
}