using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();

            Console.ReadLine();
        }
    }
}
