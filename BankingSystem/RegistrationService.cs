using System;
using static System.Console;

namespace BankingSystem
{
    public class RegistrationService : IRegistrationService
    {
        void IRegistrationService.registration(User user)
        {
            Clear();
            for (;;)
            {
                WriteLine(Resource.strings.IIN + ": ");
                string iin = ReadLine();
                long iinint = 0;
                for (int i = 0; i < iin.Length; ++i)
                {
                    if (iin[i] == ' ')
                    {
                        iin = iin.Remove(i, 1);
                    }
                }
                if (Int64.TryParse(iin, out iinint))
                {
                    if (iin.Length == 12)
                    {
                        user.IIN = iin;
                        break;
                    }
                }
                Clear();
                WriteLine(Resource.strings.Error);
            }

            WriteLine(Resource.strings.FullName + ": ");
            user.FullName = ReadLine();

            WriteLine(Resource.strings.PhoneNumber + ": ");
            user.PhoneNumber = ReadLine();

            WriteLine(Resource.strings.Email + ": ");
            user.Email = ReadLine();
        }
    }
}
