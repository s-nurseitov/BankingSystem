using System.Collections.Generic;

namespace BankingSystem
{
    public class Card
    {
        public long Id { get; set; }
        public List<Account> Accounts { get; set; }

    }
}