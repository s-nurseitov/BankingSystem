﻿using System;
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

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            //Thread.CurrentThread.CurrentUICulture=CultureInfo.GetCultureInfo("kk-KZ");

            //Console.WriteLine(Resource.strings.Hello);
            Console.ReadLine();
        }
    }
}
