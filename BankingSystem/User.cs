using System;
using System.Collections.Generic;

namespace BankingSystem
{
    [Serializable]
    public class User
    {
        public long Id { get; set; }
        public string IIN { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Card> Cards { get; set; }
        public string Email { get; set; }

        public List<string> lastOperations;
        public User()
        {
            IIN ="";
            FullName = "";
            PhoneNumber = "";
            Cards = new List<Card>();
            Email = "";
            lastOperations = new List<string>();
        }
    }
}