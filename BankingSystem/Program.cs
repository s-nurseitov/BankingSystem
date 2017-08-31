using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Globalization;
namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Server server = new Server();
            //server.Start();
            Thread.CurrentThread.CurrentUICulture =CultureInfo.GetCultures(); 
            foreach(var culture in CultureInfo.GetCultures())
            {

            }
            Console.WriteLine(strings.Hello);
            Console.ReadLine();
        }
    }
}
