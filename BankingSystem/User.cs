using System.Collections.Generic;

namespace BankingSystem
{
    public class User
    {
        public int Id { get; set; }
        public string IIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string IdCardNumber { get; set; }
        public string TelephoneNumber { get; set; }

        public List<Card> Cards { get; set; }
        public string Email { get; set; }
    }
}