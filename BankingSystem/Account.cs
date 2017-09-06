using System;

namespace BankingSystem
{
    public class Account
    {
        public long Id { get; set; }
        public long AccountNumber { get; set; }
        public double MoneyOnAccount { get; set; }
        public Сurrency Currency { get; private set; }
        private static long accountCounter;

        static Account()
        {
            accountCounter = 100;
        }
        public Account(Сurrency currency)
        {
            AccountNumber = accountCounter++;
            Currency = currency;
        }
    }
}