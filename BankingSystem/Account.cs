using System;

namespace BankingSystem
{
    [Serializable]
    public class Account
    {
        public long Id { get; set; }
        public long AccountNumber { get; set; }
        public double MoneyOnAccount { get; set; }
        public Сurrency Currency { get; private set; }

        private static Random accountCounter;

        static Account()
        {
            accountCounter = new Random();
        }
        public Account(Сurrency currency)
        {
            AccountNumber = accountCounter.Next();
            Currency = currency;
        }
    }
}