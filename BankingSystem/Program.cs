﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("SDPBank.162@gmail.com");
            msg.To.Add("aibek1805@gmail.com");
            msg.Subject = "example";
            msg.Body = "text";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("SDPBank.162@gmail.com", "qwerty123*");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                // запись логов
                //return false;
            }
        }
    }
}
