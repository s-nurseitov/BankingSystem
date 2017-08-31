using System.Collections.Generic;

namespace BankingSystem
{
    public class Card
    {
        public long Id { get; set; }
        public long NumberCard { get; set; }
        public int CVV { get; set; }
        public List<Account> Accounts { get; set; }

    }
}