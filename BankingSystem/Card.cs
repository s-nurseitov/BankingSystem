using System;
using System.Collections.Generic;

namespace BankingSystem
{
    [Serializable]
    public class Card
    {
        public long Id { get; set; }
        public long NumberCard { get; set; }
        public int CVV { get; set; }
        public List<Account> Accounts { get; set; }

        private static Random cardCounter;

        static Card()
        {
            cardCounter = new Random();
        }

        public Card()
        {
            NumberCard = cardCounter.Next();
            Accounts = new List<Account>();
        }
    }
}