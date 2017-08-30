using System.Collections.Generic;

namespace BankingSystem
{
    public class User
    {
        public long Id { get; set; }
        public string IIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string IdCardNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Card> Cards { get; set; }
        public string Email { get; set; }
    }
}