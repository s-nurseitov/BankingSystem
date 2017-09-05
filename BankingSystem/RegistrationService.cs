using System;
using static System.Console;

namespace BankingSystem
{
    public class RegistrationService : IRegistrationService
    {
        void IRegistrationService.registration(User user)
        {
            Clear();

            Write(Resource.strings.IIN+": ");
            user.IIN = ReadLine();

            Write(Resource.strings.FullName+": ");
            user.FullName = ReadLine();

            Write(Resource.strings.PhoneNumber + ": ");
            user.PhoneNumber = ReadLine();

            Write(Resource.strings.Email + ": ");
            user.Email = ReadLine();
        }
    }
}
