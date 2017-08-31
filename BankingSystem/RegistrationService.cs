using System;
using static System.Console;

namespace BankingSystem
{
    public class RegistrationService : IRegistrationService
    {
        void IRegistrationService.registration(User user)
        {
           // Write("Введите ID:");
           // user.Id = Read();

            Write("Введите ИИН:");
            user.IIN = ReadLine();

            Write("Введите имя:");
            user.FirstName = ReadLine();

            Write("Введите фамилию:");
            user.LastName = ReadLine();

            Write("Введите второе имя:");
            user.MiddleName = ReadLine();

            Write("Введите номер удостоверения личности:");
            user.IdCardNumber = ReadLine();

            Write("Введите номер телефона:");
            user.TelephoneNumber = ReadLine();   
        }
    }
}
